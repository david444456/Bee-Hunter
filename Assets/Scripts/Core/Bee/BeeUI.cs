using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core {
    [RequireComponent(typeof(Animator))]
    public class BeeUI : MonoBehaviour
    {
        [SerializeField] string _nameAnimationRecolectingPollen = "Recolect";
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ChangeAnimationToRecolectingFood() {
            ChangeAnimationByString(_nameAnimationRecolectingPollen);
        }

        private void ChangeAnimationByString(string nameNewAnimation) {
            animator.Play(nameNewAnimation, -1);
        }
    }
}
