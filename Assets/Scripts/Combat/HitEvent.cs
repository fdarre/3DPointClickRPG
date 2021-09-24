using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using UnityEngine;

using RPG.Combat;

namespace RPG.Combat
{
    public class HitEvent : MonoBehaviour
    {
        #region Init

        private void Awake()
        {
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
        
        private Fighter _fighter;

        #endregion
    }
}
