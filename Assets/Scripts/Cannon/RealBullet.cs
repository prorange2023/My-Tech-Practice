using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RealBullet : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DestroySelf());
        Debug.Log("Fire");
    }
    // 3초뒤 소멸하는 코루틴
    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
