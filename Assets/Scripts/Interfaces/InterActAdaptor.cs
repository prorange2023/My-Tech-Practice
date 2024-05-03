using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InterActAdaptor : MonoBehaviour, IInterActable
{
    public UnityEvent<PlayerInterActor> OnInteractted;

    public void Interact(PlayerInterActor player)
    {
        Debug.Log("พ๎ด๐ลอยส");
        OnInteractted?.Invoke(player);
    }
}
