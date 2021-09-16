using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using RPG.Combat;
using UnityEngine;

namespace RPG.Combat
{
    public class HitEvent : MonoBehaviour
    {
        #region Init

        private void Awake()
        {
            _health = GetComponent<Health>();
            _fighter = GetComponentInParent<Fighter>();
        }

        #endregion
        
        #region Private
        
        //Animation Event
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private void Hit()
        {
            _fighter.Hit();
        }
        
        private Health _health;
        private Fighter _fighter;

        #endregion
    }
}
