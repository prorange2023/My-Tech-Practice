using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [Header("Fly")]
    [SerializeField] float targetHeight; // ������ ����
    [SerializeField] float amplitude; // ����
    [SerializeField] float period; // �ֱ�(��)

    private void Update()
    {
        // ���� �ð�
        float time = Time.time;

        // y �� ���
        float y = targetHeight + amplitude * Mathf.Sin(2 * Mathf.PI * time / period);

        // ��ġ ������Ʈ
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
