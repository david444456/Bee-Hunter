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

        List<ControlInfoBee> _actualTotalBee = new List<ControlInfoBee>();
        List<GameObject> _actualTotalFlowers = new List<GameObject>();
        List<bool> _actualRequestedFlowers = new List<bool>();
        TypeBee _typeBeeActualContainter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Bee") {
                ControlInfoBee newInfoBee = other.GetComponentInParent<ControlInfoBee>();
                BeeItem newBee = newInfoBee.GetActualBeeItem();

                print("New detecting");
                //add bee
                if (_actualTotalBee.Count <= 0) 
                    CreateNewTypeOfBee(newBee, newInfoBee);
                else
                {
                    AddBeeToActualBees(newBee, newInfoBee);
                }

                newInfoBee.ReceiveInformationFromContainer(this, _limitsToContainer, _diaperGO);
                //communicate with bee to informate that she is in a container, the dimension
                //comunnicate info about diaper
            }
        }

        public void RemoveObjectFromList(ControlInfoBee controlInfoBee) {
            print("Remove object!");
            _actualTotalBee.Remove(controlInfoBee);
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

        public bool GetIfThereAreOneFlowerWaiting() {
            for (int i = 0; i < GetTotalFlowers(); i++)
            {
                if (!_actualRequestedFlowers[i]) return true;
            }
            return false;
        }

        public int GetTotalFlowers() => _actualTotalFlowers.Count;

        private void AddBeeToActualBees(BeeItem newBee, ControlInfoBee controlInfoBee)
        {
            if (newBee.GetTypeBee() == _typeBeeActualContainter)
            {
                _actualTotalBee.Add(controlInfoBee);
            }
            else
            {
                print("Different types!");
            }
        }

        private void CreateNewTypeOfBee(BeeItem newBee, ControlInfoBee controlInfoBee)
        {
            _typeBeeActualContainter = newBee.GetTypeBee();
            _actualTotalBee.Add(controlInfoBee);
        }
    }
}
