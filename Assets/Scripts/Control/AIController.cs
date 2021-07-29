using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeeHunter.Attributes;
using BeeHunter.Core;
using BeeHunter.Slots;
using System;

namespace BeeHunter.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float _speedFractionXZ = 0.2f;
        [SerializeField] float _speedFractionY = 0.05f;

        [Header("AI")]
        [SerializeField] float _heightToUp = 5;
        [SerializeField] float _maxTimeBetweenGetFood = 5;
        [SerializeField] float _maxTimeToChangeMoveNormal = 4;
        [SerializeField] float _distanceStop = 3;

        ControlInfoBee controlInfo;
        BeeMove move;
        Container container;
        BeeUI beeUI;

        private float timeSinceLastFood = Mathf.Infinity;
        private float timeSinceChangeMovementNormal = Mathf.Infinity;
        //private float timeSinceAggrevated = Mathf.Infinity;
        private bool _beeInContainer = false;

        private StateBee _actualStateBee;
        private Vector3 _lastMoveDirectionXZ = Vector3.zero;
        private Vector3 _lastMoveDirectionY = Vector3.zero;
        private Vector3 _lastPositionActiveFlower = Vector3.zero;


        void Start()
        {
            controlInfo = GetComponent<ControlInfoBee>();
            move = GetComponent<BeeMove>();
            beeUI = GetComponent<BeeUI>();

            controlInfo.ChangeStateBeeInContainerEvent += ChangeBeeInContainer;
        }

        private void Update()
        {
            if (_actualStateBee == StateBee.Waiting) return;

            if (_actualStateBee == StateBee.Normal)
            {
                if (_maxTimeBetweenGetFood < timeSinceLastFood && VerificatedIfThereAreFoodInTheArea())
                {

                    //if there are food in the limits
                    ChangeStateBee(StateBee.Food);
                    timeSinceLastFood = 0;
                }
                else
                {
                    if (_maxTimeToChangeMoveNormal < timeSinceChangeMovementNormal)
                    {
                        //change movement random
                        ChangeMovementRandomBee();
                        timeSinceChangeMovementNormal = 0;
                    }
                }
            }
            else if (_actualStateBee == StateBee.Food)
            {

                //get position of the food
                GameObject newFlower = container.GetFlowerGameObject();

                //distance
                if (Vector3.Distance(transform.position, _lastPositionActiveFlower) < _distanceStop)
                {
                    print("Distance!");
                    ChangeStateBee(StateBee.Waiting);
                    beeUI.ChangeAnimationToRecolectingFood();
                }

                //null
                if (newFlower == null) return;

                //set movement to this direction
                _lastPositionActiveFlower = newFlower.transform.position;
                _lastMoveDirectionXZ = _lastPositionActiveFlower;
                move.StartMoveAction(_lastPositionActiveFlower, _speedFractionXZ);

                //up down direction
                _lastMoveDirectionY = new Vector3(0, newFlower.transform.position.y, 0);
                print(_lastMoveDirectionY);
                StartMoveUpDirection();

            }
            else if (_actualStateBee == StateBee.Pollen) print("I am polling");

            //movement

            timeSinceLastFood += Time.deltaTime;
            timeSinceChangeMovementNormal += Time.deltaTime;
        }

        private bool VerificatedIfThereAreFoodInTheArea() {
            if (_beeInContainer)
            {
                container = controlInfo.GetActualContainer();
                return container.GetIfThereAreOneFlowerWaiting();
            }
            else {
                //verificated if there are in the limits around the world
                return false;
            }
        }

        private bool BePositionInLimits(Vector3 position, Transform[] limits)
        {
            bool res = true;
            float maxDistance = 0;

            //search for the max distance
            for (int i = 0; i < limits.Length-1; i++)
            {
                float distance = Vector3.Distance(limits[i].transform.position, limits[i+1].transform.position);
                if (distance > maxDistance) maxDistance = distance;
            }

            //in limits
            for (int i = 0; i < limits.Length; i++) {
                if (Vector3.Distance(limits[i].position, transform.position) > maxDistance) res = false; 
            }

            return res;
        }

        private void ChangeMovementRandomBee() {
            Transform[] limits = controlInfo.GetLimitsTransform();
            if (_beeInContainer)
            {
                MovementRandomBeeXZInContainer(limits);
                MovementRandomBeY();
            }
            else { 
                //move around the world
            }
        }

        private void MovementRandomBeeXZInContainer(Transform[] limits)
        {
            //get values
            float minX = limits[0].position.x;
            float minZ = limits[0].position.z;
            float maxX = limits[0].position.x;
            float maxZ = limits[0].position.z;

            for (int i = 1; i < limits.Length - 1; i++)
            {
                minX = Mathf.Min(minX, limits[i + 1].position.x);
                minZ = Mathf.Min(minZ, limits[i + 1].position.z);
                maxX = Mathf.Max(maxX, limits[i + 1].position.x);
                maxZ = Mathf.Max(maxZ, limits[i + 1].position.z);
            }

            //make the direction
            Vector3 newDirection = new Vector3(UnityEngine.Random.Range(minX, maxX), 0, UnityEngine.Random.Range(minZ, maxZ));

            //information
            if (!BePositionInLimits(newDirection, limits)) Debug.LogError("The limits are wrong! : " + newDirection);

            //change movement
            _lastMoveDirectionXZ = newDirection;
            move.StartMoveAction(newDirection, _speedFractionXZ);
        }

        private void MovementRandomBeY()
        {
            float moveY = UnityEngine.Random.Range(0, _heightToUp);
            _lastMoveDirectionY = new Vector3(0, moveY, 0);

            StartMoveUpDirection();
        }

        private float GetTimeInSecondsMovementHorizontal() {
            float distanceX = Vector3.Distance(_lastMoveDirectionXZ, transform.position);
            return distanceX / move.GetActualSpeed();
        }

        private void StartMoveUpDirection()
        {
            float timeInSeconds = GetTimeInSecondsMovementHorizontal();
            move.StartMoveActionUpDirection(_lastMoveDirectionY, timeInSeconds);
        }

        private void ChangeStateBee(StateBee newState) => _actualStateBee = newState;

        private void ChangeBeeInContainer(bool newState) => _beeInContainer = newState;
    }

    public enum StateBee { 
        Normal,
        Food,
        Pollen,
        Waiting
    }
}
