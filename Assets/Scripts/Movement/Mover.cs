using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    #region Expose in inspector

    [SerializeField] private Transform target;

    #endregion

    #region Init

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
    }

    #endregion

    #region Update

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        } 
    }

    #endregion

    #region Private Methods

    private void MoveToCursor()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            _navMeshAgent.SetDestination(hit.point);
        }
        
        

    }

    #endregion


    #region Private fields

    private NavMeshAgent _navMeshAgent;
    private Camera _mainCamera;

    #endregion
}
