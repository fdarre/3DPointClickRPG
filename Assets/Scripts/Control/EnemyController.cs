using System;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class EnemyController : MonoBehaviour
    {
        #region Expose in Inspector
        
        [SerializeField] private float chaseDistance = 5f;
                
        #endregion
        
        #region Init

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player"); //Start ?
            _fighter = GetComponent<Fighter>(); //start ?
        }

        #endregion
        
        #region Update

        private void Update()
        {
            if (IsPlayerInRange())
            {
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel(); //@todo: optimize
            }
        }

        private bool IsPlayerInRange()
        {
            float distanceToPlayer = Vector3.Distance(_player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        #endregion   
        
        #region Private

        private GameObject _player;
        private Fighter _fighter;

        #endregion
        
    }
}
