using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _mainCamera = Camera.main;
        }
        
        private void Update()
        {
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

                    if (!GetComponent<Fighter>().GetIsValidTarget(target)) {continue;} //optimize

                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<Fighter>().Attack(target);
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
                    _mover.StartMoveAction(hit.point);
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
        private Camera _mainCamera;
        private Ray _lastRay;
    }
}
