using System;

using UnityEngine;
using UnityEngine.AI;

namespace RPG.Animation
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterAnimationController : MonoBehaviour
    {
        #region Init

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        #endregion

        #region Update

        private void Update()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            _animator.SetFloat(_forwardSpeed, localVelocity.z);
        }

        #endregion

        #region Public

        public void TriggerAttack()
        {
            _animator.ResetTrigger(_stopAttack);
            _animator.SetTrigger(_attack);
        } 
        
        public void TriggerDeath()
        {
            _animator.SetTrigger(_die);
        }

        public void TriggerStopAttack()
        {
            _animator.SetTrigger(_stopAttack);
        }
        

        #endregion


        #region Private Fields

        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private int _forwardSpeed = Animator.StringToHash("ForwardSpeed");
        private int _attack = Animator.StringToHash("Attack");
        private int _stopAttack = Animator.StringToHash("StopAttack");
        private int _die = Animator.StringToHash("Die");

        #endregion
    }
}