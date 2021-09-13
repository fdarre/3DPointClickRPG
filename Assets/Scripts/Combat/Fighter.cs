using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        #region Public

        public void Attack(CombatTarget target)
        {
            Debug.Log("attacking" + target.gameObject.name);      
        }
        
        #endregion
    }
}