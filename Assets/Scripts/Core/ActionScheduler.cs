using UnityEngine;

namespace Core
{
    public class ActionScheduler : MonoBehaviour
    {
  
        #region Public
  
        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            
            _currentAction?.Cancel();
            
            _currentAction = action;
        }
  
        #endregion
  
        #region Private
  
        private IAction _currentAction;
  
        #endregion
    }
}
