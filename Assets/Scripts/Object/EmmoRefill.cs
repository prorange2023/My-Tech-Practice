using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmoRefill : MonoBehaviour
{
    // Gun Tag �ý��ۿ��� ����ϴ� �±� �̸�
    private const string GUN_TAG_NAME = "Gun";


    void OnTriggerEnter(Collider other)
    {
        //// �浹�� ������Ʈ�� Player���� Ȯ��
        //if (other.gameObject.layer == 3)
        //{
        //    // ���� ������ Gun Tag ������Ʈ�� ã��
        //    GameObject[] gunTagObjects = GameObject.FindGameObjectsWithTag(GUN_TAG_NAME);
        //    foreach (GameObject gunTagObject in gunTagObjects)
        //    {
        //        // �ڽ� ������Ʈ�� B ������Ʈ���� Ȯ��

        //        if (gunTagObject.gameObject.GetComponentInChildren<AmmoSystem>() != null)
        //        {
        //            // B ������Ʈ�� ź�� ����
        //            AmmoSystem ammo = gunTagObject.gameObject.GetComponentInChildren<AmmoSystem>();
        //            if (ammo != null)
        //            {
        //                ammo.AmmoLeft = 48;
        //                // �������� ź�� �ִ밪 �Է� ���(�÷��̾���)
        //            }
        //        }
        //        Debug.Log("Gun Tag ������Ʈ �̸�: " + gunTagObject.name);
        //    }


        //    //A ������Ʈ �Ҹ꿩�� ����
        //    Destroy(gameObject);
        //}
    }
}
