using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float attackCooltime;
    [SerializeField] int deal;
    [SerializeField] bool moving;
    [SerializeField] bool activable = true;

    public void OnFire()
    {
        if (activable == true)
        {
            StartCoroutine(fireCoolTime());
            if (Physics.Raycast(attackPoint.position, attackPoint.forward, out RaycastHit hit, attackRange, targetLayerMask))
            {

                IDamageable damageable = hit.collider.gameObject.GetComponent<IDamageable>();

                Debug.Log(hit.collider.gameObject.name);
                damageable?.TakeDamage(deal);
                Manager.Sound.PlaySFX(0);
            }
        }
        else
            return;

    }
    IEnumerator fireCoolTime()
    {
        activable = false;
        yield return new WaitForSeconds(attackCooltime);
        activable = true;
    }
}
