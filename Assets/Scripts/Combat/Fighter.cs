using System;

using UnityEngine;
using UnityEngine.Serialization;

using RPG.Core;
using RPG.Animation;
using RPG.Movement;


namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        #region Expose in Inspector

        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private float delayBetweenAttacks = 1f;
        
        #endregion
        
        #region Init

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _scheduler = GetComponent<ActionScheduler>();
            _animationController = GetComponent<CharacterAnimationController>();
        }
        
        #endregion
        
        
        #region Public
        
        public bool GetIsValidTarget(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            _scheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>(); //optimize ?
        }

        public void Cancel()
        {
            _animationController.TriggerStopAttack();
            _target = null;
        }

        public void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage);
        }
        
        #endregion
        
        
        #region Update

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_target == null || _target.IsDead ) { return; }
            
            //@todo: Use stopping distance instead ?
            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }
        
        private void AttackBehaviour()
        {
            if (_timeSinceLastAttack <= delayBetweenAttacks) return;
            
            _animationController.TriggerAttack();
            _timeSinceLastAttack = 0f;
        }

        
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        #endregion
        
        
        #region Private
        
        private float _timeSinceLastAttack = 100f;
        private Health _target;
        private Mover _mover;
        private ActionScheduler _scheduler;
        private CharacterAnimationController _animationController;

        #endregion
    }
}