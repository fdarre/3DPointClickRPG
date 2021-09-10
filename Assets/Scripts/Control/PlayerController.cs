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
            if (Input.GetMouseButton(0))
            { 
                MoveToCursor();
            }
        }
        
        private void MoveToCursor()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _mover.MoveTo(hit.point);
            }
        }

        private Mover _mover;
        private Camera _mainCamera;
    }
}
