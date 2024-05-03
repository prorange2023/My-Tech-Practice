using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInterActor : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] bool debug;


    Collider[] colliders = new Collider[20];
    private void OnInterActor(InputValue value)
    {
        Interact();
    }

    private void Interact()
    {
        int size = Physics.OverlapSphereNonAlloc(transform.position, range, colliders);
        for (int i = 0; i < size; i++)
        {
            IInterActable interactable = colliders[i].GetComponent<IInterActable>();
            if (interactable != null)
            {
                interactable.Interact(this);
                return;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (debug == false)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
