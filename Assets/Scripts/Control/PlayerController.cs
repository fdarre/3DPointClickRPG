using UnityEngine;

using RPG.Combat;
using RPG.Movement;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
            if(_health.IsDead) return;
            if(CombatInteraction()) return;
            if(MovementInteraction()) return;
        }

        private bool CombatInteraction()
        {
            _lastRay = GetRayFromMousePosition(); //@todo
            
            RaycastHit[] hits = Physics.RaycastAll(_lastRay);
            
                foreach (var hit in hits)
                {
                    CombatTarget target = hit.collider.GetComponentInParent<CombatTarget>();  //replace by tags
                    
                    if(target == null) {continue;}

                    if (!GetComponent<Fighter>().GetIsValidTarget(target.gameObject)) {continue;} //optimize

                    if (Input.GetMouseButton(0))
                    {
                        GetComponent<Fighter>().Attack(target.gameObject);
                    }
                    return true;
                }
                return false;

        }

        private bool MovementInteraction()
        {
            _lastRay = GetRayFromMousePosition();
            
            if (Physics.Raycast(_lastRay, out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {
                    _mover.StartMoveAction(hit.point, 1f); //1f = full speed
                }
                return true;
            }

            return false;
        }
        
        private Ray GetRayFromMousePosition()
        {
            return _mainCamera.ScreenPointToRay(Input.mousePosition);
        }

        private Mover _mover;
        private Health _health;
        private Camera _mainCamera;
        private Ray _lastRay;
    }
}
