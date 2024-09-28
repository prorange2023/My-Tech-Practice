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
        //�ٴ�üũ�� �ݸ����� Ʈ���� �Ӽ� �ѱ�
        CheckCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        //��Ʈ����Ʈ�� �ٴڷ��̾� üũ
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //��Ʈ����Ʈ�� �ٴڷ��̾� üũ
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //��Ʈ����Ʈ�� �ٴڷ��̾� üũ
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGround = false;
        }
    }
    //�ٴڿ��� Ȯ�� ��ȯ
    public bool GetIsGround()
    {
        return isGround;
    }
}
