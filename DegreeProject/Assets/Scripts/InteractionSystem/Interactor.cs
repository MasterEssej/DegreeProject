using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionRange = 0.75f;
    [SerializeField] private LayerMask interactionLayerMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int colliderCount;

    private void Update()
    {
        colliderCount = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionRange, colliders, interactionLayerMask);
        
        if (colliderCount > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();
            if (interactable != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }





}
