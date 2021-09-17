using System;
using Core;
using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        #region Expose in Inspector
        
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionStateDuration = 8f;
                
        #endregion
        
        #region Init

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player"); //Start ?
            _fighter = GetComponent<Fighter>(); //start ?
            _health = GetComponent<Health>(); 
            _mover = GetComponent<Mover>(); 
        }

        private void Start()
        {
            _guardOriginalPosition = transform.position;
        }

        #endregion
        
        #region Update

        private void Update()
        {
            //@todo: State machine instead
            if(_health.IsDead) return;
            
            if (IsPlayerInRange())
            {
                _timeSinceSawPlayer = 0f;
                AttackState();
            }
            else if (_timeSinceSawPlayer < suspicionStateDuration)
            {
                SuspicionState();
            }
            else
            {
                GuardState();
            }

            _timeSinceSawPlayer += Time.deltaTime;
        }

        private void GuardState()
        {
            _mover.StartMoveAction(_guardOriginalPosition); //@todo: optimize ? Called at each frame.
        }

        private void AttackState()
        {
            _fighter.Attack(_player);
        }

        private void SuspicionState()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private bool IsPlayerInRange()
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        #endregion   
        
        #region Private

        private GameObject _player;
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;
        private Vector3 _guardOriginalPosition;
        private float _timeSinceSawPlayer = Mathf.Infinity;

        #endregion

    }
}
