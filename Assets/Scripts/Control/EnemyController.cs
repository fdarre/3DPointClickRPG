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
        
        [SerializeField] private float waypointDistanceTolerance = 1f;
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float suspicionStateDuration = 8f;
        [SerializeField] private float waypointWaitTime = 2f;
        [SerializeField] private PatrolPath patrolPath;

                
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
                AttackingState();
            }
            else if (_timeSinceSawPlayer < suspicionStateDuration)
            {
                SuspicionState();
            }
            else
            {
                PatrollingState();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceSawPlayer += Time.deltaTime;
            _timeSinceLastWaypoint += Time.deltaTime;
        }

        private void PatrollingState()
        {
            Vector3 nextPosition = _guardOriginalPosition;
            
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypointPosition();
            }
            
            if (_timeSinceLastWaypoint > waypointWaitTime)
            {
                _mover.StartMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWaypointPosition()
        {
            return patrolPath.GetWaypointPosition(_currentWaypointIndex);
        }
        
        private void CycleWaypoint()
        {
            _timeSinceLastWaypoint = 0f;
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }
        

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypointPosition());
            return distanceToWaypoint < waypointDistanceTolerance;
        }

        private void AttackingState()
        {
            _timeSinceSawPlayer = 0f;
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
        private float _timeSinceLastWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;

        #endregion

    }
}
