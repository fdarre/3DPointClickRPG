using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        #region Init

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();
        }

        #endregion

        #region Public

        public void MoveTo(Vector3 destination)
        {
            _navMeshAgent.SetDestination(destination);
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
        private static readonly int _forwardSpeed = Animator.StringToHash("forwardSpeed");

        #endregion
    }
}
