using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    [Header("Save")]
    [SerializeField] LayerMask player;
    [SerializeField] int savePosition;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & player) != 0)
        {
            Manager.Game.savePoint = savePosition;
        }
    }
}
