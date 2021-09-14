using System;
using Core;
using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        #region Expose in Inspector

        [SerializeField] private float weaponRange = 3f;
        
        #endregion
        
        #region Init

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _scheduler = GetComponent<ActionScheduler>();
        }
        
        #endregion
        
        
        #region Public

        public void Attack(CombatTarget combatTarget)
        {
            _scheduler.StartAction(this);
            _target = combatTarget.transform;
        }

        public void Cancel()
        {
            _target = null;
        }
        
        #endregion
        
        
        #region Update

        private void Update()
        {
            
            if (_target == null) { return; }
            
            //@todo: Use stopping distance instead ?
            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.Cancel();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        #endregion
        
        
        #region Private
        
        private Transform _target;
        private Mover _mover;
        private ActionScheduler _scheduler;

        #endregion
    }
}