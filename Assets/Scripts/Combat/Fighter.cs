using System;
using Combat;
using Core;
using UnityEngine;
using RPG.Movement;
using UnityEngine.Serialization;

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
            _animator = GetComponentInChildren<Animator>();
        }
        
        #endregion
        
        
        #region Public
        
        public bool GetIsValidTarget(CombatTarget combatTarget)
        {
            if (combatTarget == null) { return false; }
            
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _scheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>(); //optimize ?
        }

        public void Cancel()
        {
            _animator.SetTrigger(_stopAttacking);
            _target = null;
        }

        public void Hit()
        {
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
            
            _animator.SetTrigger(_attack);
            _timeSinceLastAttack = 0f;
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        #endregion
        
        
        #region Private
        
        private Health _target;
        private Animator _animator;
        private Mover _mover;
        private ActionScheduler _scheduler;
        private float _timeSinceLastAttack = 0f;
        private static readonly int _attack = Animator.StringToHash("attack");
        private static readonly int _stopAttacking = Animator.StringToHash("stopAttacking");

        #endregion
    }
}