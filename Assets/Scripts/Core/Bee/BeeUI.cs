using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeHunter.Core {
    [RequireComponent(typeof(Animator))]
    public class BeeUI : MonoBehaviour
    {
        [SerializeField] string _nameAnimationRecolectingPollen = "Recolect";
        [SerializeField] string _nameAnimationWorkWithDiaper = "WorkDiaper";

        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ChangeAnimationToRecolectingFood() {
            ChangeAnimationByString(_nameAnimationRecolectingPollen);
        }

        public void ChangeAnimationToWorkWithDiaper() {
            ChangeAnimationByString(_nameAnimationWorkWithDiaper);
        }

        private void ChangeAnimationByString(string nameNewAnimation) {
            animator.Play(nameNewAnimation, -1);
        }
    }
}
