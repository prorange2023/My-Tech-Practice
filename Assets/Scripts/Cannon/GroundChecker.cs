using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] SphereCollider groundCollider;
    [SerializeField] float radius;
    [SerializeField] float distance;

    [Header("Debug")]
    [SerializeField] bool debug;

    private bool isHit;
    private Vector3 hitPos;
    private float hitDistance;
    private bool isGround;

    private void FixedUpdate()
    {
        
    }

    bool GroundCheck()
    {
        isHit = Physics.SphereCast(transform.position, radius, Vector3.down, out RaycastHit hit, distance, groundLayer);
        hitPos = hit.point;
        hitDistance = hit.distance;
        return isHit;
    }
}
