using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private Rigidbody rb;

}
