using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Playables;

using RPG.Movement;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        
        #region Expose in Inspector
        
        [SerializeField] private GameObject player;
                
        #endregion
        
        #region Collisions
        
        private void OnTriggerEnter(Collider other)
        {
            if (_hasBeenTriggered || !other.CompareTag("Player")) return;
            
            player.GetComponent<Mover>().Cancel();
            GetComponent<PlayableDirector>().Play();
            _hasBeenTriggered = true;
        }
        
        #endregion
        
        #region Private

        private bool _hasBeenTriggered = false;

        #endregion

    }
}
