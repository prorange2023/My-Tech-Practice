using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysics : MonoBehaviour
{
    [SerializeField] Gravity gravity;
    [SerializeField] GroundChecker groundChecker;

    private void Start()
    {
        gravity = GetComponent<Gravity>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void Update()
    {
        gravityActive();
    }
    private void gravityActive()
    {
        gravity.enabled = !groundChecker.IsGround;
    }
}
