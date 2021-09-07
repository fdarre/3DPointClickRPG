using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    #region Init

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
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
        UpdateAnimator();
    }

    #endregion


    #region Private

    private void UpdateAnimator()
    {
        Vector3 velocity = _navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        _animator.SetFloat("forwardSpeed", speed);

    }

    private void MoveToCursor()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _navMeshAgent.SetDestination(hit.point);
        }
    }

    private NavMeshAgent _navMeshAgent;
    private Camera _mainCamera;
    private Animator _animator;

    #endregion
}
