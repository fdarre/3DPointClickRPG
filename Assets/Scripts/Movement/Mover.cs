using Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        #region Init

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
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
            UpdateAnimator();
        }

        #endregion


        #region Private

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            _animator.SetFloat(_forwardSpeed, speed);

        }

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private ActionScheduler _scheduler;
        private static readonly int _forwardSpeed = Animator.StringToHash("forwardSpeed");

        #endregion
        
    }
}
