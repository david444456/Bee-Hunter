using BeeHunter.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Slots
{
    public class Container : MonoBehaviour
    {
        [SerializeField] Transform[] _limitsToContainer;
        [SerializeField] GameObject _diaperGO;
        [SerializeField] GameObject _honeyGO;
        [SerializeField] float _forceSpawn = 250f;

        List<ControlInfoBee> _actualTotalBee = new List<ControlInfoBee>();
        List<GameObject> _actualTotalFlowers = new List<GameObject>();
        List<bool> _actualRequestedFlowers = new List<bool>();
        TypeBee _typeBeeActualContainter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Bee")
            {
                ControlInfoBee newInfoBee = other.GetComponentInParent<ControlInfoBee>();
                BeeItem newBee = newInfoBee.GetActualBeeItem();

                //add bee
                if (_actualTotalBee.Count <= 0)
                {
                    CreateNewTypeOfBee(newBee, newInfoBee);
                }
                else
                {
                    if (!AddBeeToActualBees(newBee, newInfoBee)) return; //for no the same types of bee
                }

                //communicate with bee to informate that she is in a container, the dimension
                //comunnicate info about diaper
                newInfoBee.ReceiveInformationFromContainer(this, _limitsToContainer, _diaperGO);
                _honeyGO = newBee.GetGOHoney();

            }
            else if (other.tag == "Flower") {
                _actualTotalFlowers.Add(other.gameObject);
                _actualRequestedFlowers.Add(false);
            }
        }

        public void RemoveBeeObjectFromList(ControlInfoBee controlInfoBee) {
            print("Remove object!");
            _actualTotalBee.Remove(controlInfoBee);
        }

        public void RemoveFlowerObjectFromList(GameObject flowerGO) {
            if (!_actualTotalFlowers.Exists(element => flowerGO)) return ;
            int index = _actualTotalFlowers.IndexOf(flowerGO);
            _actualTotalFlowers.Remove(flowerGO);
            _actualRequestedFlowers.RemoveAt(index);
        }

        public void ReturnFlowerToUnrequested(GameObject flower) {
            int index = _actualTotalFlowers.IndexOf(flower);
            _actualRequestedFlowers[index] = false;
        }

        public void InstantiateNewHoney(GameObject GOactualDiaper) {
            GameObject honey = Instantiate(_honeyGO, GOactualDiaper.transform.position, Quaternion.identity);

            //ramdon force
            float x = UnityEngine.Random.Range(0, 1.0f);
            float y = UnityEngine.Random.Range(0, 1.0f);
            float z = UnityEngine.Random.Range(0, 1.0f);
            Vector3 newRandomVector = 
                new Vector3(x, y, z);

            honey.GetComponent<Rigidbody>().AddForce(newRandomVector * _forceSpawn, ForceMode.Impulse);
        }

        public void FinishJobWithFlower(GameObject GOflower) {
            bool res = GOflower.GetComponentInParent<ControlFlower>().NewJobFinishedWithThisFlower_ReturnIfFlowerDie();
            if (!res)
                ReturnFlowerToUnrequested(GOflower);
            else
                RemoveFlowerObjectFromList(GOflower);
        }

        public GameObject GetFlowerGameObject() {

            for (int i = 0; i < GetTotalFlowers(); i++) {
                if (!_actualRequestedFlowers[i]) {
                    _actualRequestedFlowers[i] = true;
                    return _actualTotalFlowers[i];
                }
            }

            return null;
        }

        public GameObject GetActualDiaper() => _diaperGO;

        public bool GetIfThereAreOneFlowerWaiting() {
            for (int i = 0; i < GetTotalFlowers(); i++)
            {
                if (!_actualRequestedFlowers[i]) return true;
            }
            return false;
        }

        public int GetTotalFlowers() => _actualTotalFlowers.Count;

        private bool AddBeeToActualBees(BeeItem newBee, ControlInfoBee controlInfoBee)
        {
            if (newBee.GetTypeBee() == _typeBeeActualContainter)
            {
                _actualTotalBee.Add(controlInfoBee);
                return true;
            }
            else
            {
                print("Different types!");
            }
            return false;
        }

        private void CreateNewTypeOfBee(BeeItem newBee, ControlInfoBee controlInfoBee)
        {
            _typeBeeActualContainter = newBee.GetTypeBee();
            _actualTotalBee.Add(controlInfoBee);
        }
    }
}
