using System;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        
        #region Public
        
        public int GetNextIndex(int i)
        {
            if (i < transform.childCount - 1)
            {
                return i + 1;
            }

            return 0;
        }

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }
        
        #endregion
        
        #region Private

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++) //donne le nombre de children que le gameobject a !!!
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypointPosition(i), _WaypointGizmosRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }

        private const float _WaypointGizmosRadius = 0.2f; //check naming convention 

        #endregion
    }
}
