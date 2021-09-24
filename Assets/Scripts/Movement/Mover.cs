using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Combat;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        #region Init

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _scheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        #endregion

        #region Public

        public void StartMoveAction(Vector3 destination)
        {
            _scheduler.StartAction(this);
            MoveTo(destination);               
        }

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
            _navMeshAgent.isStopped = false;
        }
        
        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
        
        #endregion

        #region Update

        private void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead;
        }

        #endregion


        #region Private
        
        private NavMeshAgent _navMeshAgent;
        private ActionScheduler _scheduler;
        private Health _health;

        #endregion
        
    }
}
