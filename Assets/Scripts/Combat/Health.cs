using UnityEngine;

using RPG.Core;
using RPG.Animation;


namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        #region Expose in inspector
        
        [SerializeField] private float health = 30f;
        
        #endregion

        #region Properties

        public bool IsDead { get; private set; }

        #endregion
        
        #region Init

        private void Awake()
        {
            _animationController = GetComponent<CharacterAnimationController>();
        }

        #endregion
        
        #region Public

        public void TakeDamage(float damageAmount)
        {
            health = Mathf.Max(health - damageAmount, 0);
            Debug.Log(health);
            if (health <= Mathf.Epsilon && !IsDead)
            {
                Die();
            }
        }

        #endregion
        
        #region Private
        
        private void Die()
        {
            _animationController.TriggerDeath();
            IsDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        private CharacterAnimationController _animationController;
        
        #endregion
    }
}
