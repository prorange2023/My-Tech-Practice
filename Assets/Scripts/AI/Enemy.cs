using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Spec")]
    [SerializeField] protected int hp;
    public void TakeDamage(int damage)
    {
        Debug.Log("damaged");
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
