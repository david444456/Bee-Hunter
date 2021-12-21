using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BeeHunter.Attributes
{
    public abstract class Health : MonoBehaviour
    {

        [SerializeField] public UnityEvent OnDie;
        [SerializeField] protected int damageToReceiveHit = 20;
        [SerializeField] protected int initialHealthPoints = 100;
        [SerializeField] protected bool isPlayer = false;

        [HideInInspector] protected Animator animator;
        [HideInInspector] protected bool isDead = false;

        private int _healthPoints;

        protected virtual void Awake()
        {
            _healthPoints = initialHealthPoints;
        }

        /// <summary>
        /// Take damage in the health gameobject
        /// </summary>
        /// <param name="instigator"> GameObject who does the damage</param>
        /// <param name="damage">damage value (float) </param>
        public virtual void TakeDamage(GameObject instigator, float damage)
        {
            //dont continue when the player dies
            if (isDead) return;

            //take damage
            _healthPoints = (int)Mathf.Max(_healthPoints - damage, 0);
            if (_healthPoints <= 0) //die general
            {
                OnDie.Invoke();
            }
        }

        /// <summary>
        /// Augment the health with heal parameter
        /// </summary>
        /// <param name="heal"></param>
        public void AugmentHealth(float heal) => _healthPoints = (int)Mathf.Min(_healthPoints + heal, GetMaxHealthPoints());

        public float GetInitialHealth() => initialHealthPoints;

        /// <returns>Return health points</returns>
        public float GetHealthPoints() => _healthPoints;

        /// <returns>Return health max points</returns>
        public float GetMaxHealthPoints() => initialHealthPoints;

        /// <returns>If is dead</returns>
        public bool IsDead() => isDead;

        public virtual void Die(string animatorStringDeath)
        {
            //dead
            if (isDead) return;
            isDead = true;

            //animator
            /*Animator animator = GetComponent<Animator>();
            animator.Play(animatorStringDeath, -1);*/
        }

        /*
        //captureState save the different variables
        public object CaptureState()
        {
            return _healthPoints;
        }

        public void RestoreState(object state)
        {
            _healthPoints = (float)state;
            if (_healthPoints <= 0)
            {
                Die("DeathSave");
            }
        }*/

    }
}
