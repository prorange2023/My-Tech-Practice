using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterActAdaptor : MonoBehaviour, IInterActable
{
    public UnityEvent<PlayerInterActor> OnInteractted;
    public void Interact(PlayerInterActor player)
    {
        OnInteractted?.Invoke(player);
    }
}
