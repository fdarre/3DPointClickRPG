using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        #region Expose in inspector
        
        [SerializeField] private float health = 30f;
        
        #endregion
        
        #region Public
        
        public void Takedamage(float damageAmount)
        {
            health = Mathf.Max(health -= damageAmount, 0);
            Debug.Log(health);
        }
        
        #endregion
    }
}
