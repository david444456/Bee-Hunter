using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeeHunter.Attributes;
using BeeHunter.Core;
using BeeHunter.Slots;
using System;

namespace BeeHunter.Control
{
    public class AIController : MonoBehaviour, IDesactiveItemObject
    {
        [SerializeField] Transform[] _limitsBeeInNature;
        [SerializeField] float _speedFractionXZ = 0.2f;
        [SerializeField] float _speedFractionY = 0.05f;
        [SerializeField] float _mixSpeedVelocityMoveY = 0.3f;
        [SerializeField] Collider _colliderMesh;

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

        [SerializeField] private StateBee _actualStateBee;
        private Vector3 _lastMoveDirectionXZ = Vector3.zero;
        private Vector3 _lastMoveDirectionY = Vector3.zero;
        private GameObject _GOLastFlower = null;
        private GameObject _GOLastDiaper = null;


        void Start()
        {
            controlInfo = GetComponent<ControlInfoBee>();
            move = GetComponent<BeeMove>();
            beeUI = GetComponent<BeeUI>();

            controlInfo.ChangeStateBeeInContainerEvent += ChangeBeeInContainer;
            GetComponent<ControlItemObject>().DesactiveObjectEvent += DesactiveObjectInformToMainContainer;
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
                if (_GOLastFlower == null) { 
                    //get other flower
                    GameObject newFlower = container.GetFlowerGameObject();
                    _GOLastFlower = newFlower;

                    //distance
                    if (_GOLastFlower == null)
                    {
                        ChangeStateBee(StateBee.Normal);
                    }
                    else {
                        print("Change ignore collision " + _colliderMesh.name);
                        Physics.IgnoreCollision(_colliderMesh, _GOLastFlower.GetComponentInParent<CapsuleCollider>());
                    }
                }

                //distance
                if (_GOLastFlower == null) return ; 

                //ai
                if (GetIsDistanceLessThanStop(_GOLastFlower))
                {
                    //states
                    ChangeStateBee(StateBee.Waiting);
                    
                    //counts flower and unrequested
                    container.FinishJobWithFlower(_GOLastFlower);

                    //animation
                    beeUI.ChangeAnimationToRecolectingFood();
                }

                AIMovementBee(_GOLastFlower, _GOLastFlower.transform.position);

                StartMoveUpDirection();

            }
            else if (_actualStateBee == StateBee.Pollen) {
                if (_GOLastDiaper == null) _GOLastDiaper = container.GetActualDiaper();

                //distance
                if (_GOLastDiaper == null) return;

                //ai
                if (GetIsDistanceLessThanStop(_GOLastDiaper))
                {
                    ChangeStateBee(StateBee.Waiting);
                    beeUI.ChangeAnimationToWorkWithDiaper();
                }

                //ai
                AIMovementBee(_GOLastDiaper, _GOLastDiaper.transform.position);
                StartMoveUpDirection();
            }

            //time values
            timeSinceLastFood += Time.deltaTime;
            timeSinceChangeMovementNormal += Time.deltaTime;
        }

        public void DesactiveObjectInformToMainContainer()
        {
            if(_GOLastFlower != null)
                container.ReturnFlowerToUnrequested(_GOLastFlower);
        }

        private void AnimationFinishWorkWithDiaper() {
            ChangeStateBee(StateBee.Normal);
            container.InstantiateNewHoney(_GOLastDiaper);
        }

        private void AIMovementBee(GameObject GOvalue, Vector3 positionToMove)
        {

            //set movement to this direction
            _lastMoveDirectionXZ = positionToMove;

            CallToStartMoveAction(positionToMove);

            //up down direction
            _lastMoveDirectionY = new Vector3(0, GOvalue.transform.position.y, 0);
        }

        private Vector3 GetVectorConvertedWithZeroY(Vector3 newVector) => new Vector3(newVector.x, 0, newVector.z);

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

        private bool GetIsDistanceLessThanStop(GameObject GOValue)
            => Vector3.Distance(transform.position, GOValue.transform.position) < _distanceStop;

        private void ChangeMovementRandomBee() {
            Transform[] limits = controlInfo.GetLimitsTransform();
            if (_beeInContainer)
            {
                MovementRandomBeeXZInContainer(limits);
                MovementRandomBeY();
            }
            else {
                MovementRandomBeeXZInContainer(_limitsBeeInNature);
                MovementRandomBeY();
                print("it isnt in container");
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
            CallToStartMoveAction(newDirection);
        }

        private void CallToStartMoveAction(Vector3 positionToMove)
                                             => move.StartMoveAction(GetVectorConvertedWithZeroY(positionToMove), _speedFractionXZ);

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
            float timeMoveUP = GetTimeInSecondsMovementHorizontal();
            float timeInSeconds = Mathf.Max(timeMoveUP, _mixSpeedVelocityMoveY);
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
