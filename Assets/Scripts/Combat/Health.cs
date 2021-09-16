using Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        #region Expose in inspector
        
        [SerializeField] private float health = 30f;
        
        #endregion

        #region Properties

        public bool IsDead { get; set; }

        #endregion
        
        #region Init

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
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
            _animator.SetTrigger(_die);
            IsDead = true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
        private Animator _animator;
        private static readonly int _die = Animator.StringToHash("die");

        #endregion
    }
}
