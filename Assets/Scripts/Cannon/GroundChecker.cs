using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] BoxCollider CheckCollider;
    [SerializeField] bool isGround;
    public bool IsGround { get { return isGround; } }

    [Header("Debug")]
    [SerializeField] bool debug;

    private void Start()
    {
        //바닥체크용 콜리더의 트리거 속성 켜기
        CheckCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //비트시프트로 바닥레이어 체크
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //비트시프트로 바닥레이어 체크
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //비트시프트로 바닥레이어 체크
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = false;
        }
    }
    //바닥여부 확인 반환
    public bool GetIsGround()
    {
        return isGround;
    }
}
