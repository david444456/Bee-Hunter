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

                newInfoBee.ReceiveInformationFromContainer(this);
                //communicate with bee to informate that she is in a container, the dimension
                //comunnicate info about diaper
            }
        }

        public void RemoveObjectFromList(ControlInfoBee controlInfoBee) {
            print("Remove object!");
            _actualTotalBee.Remove(controlInfoBee);
        }

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
