using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 initialVelocity;    // �ʱ� �ӵ�
    public float gravity = -9.81f;     // �߷� ���ӵ�
    private Vector3 velocity;          // ���� �ӵ�
    private Vector3 position;          // ���� ��ġ


    void Start()
    {
        // �ʱ� �ӵ��� ��ġ ����
        velocity = initialVelocity;
        position = transform.position;
    }

    void Update()
    {
        // �߷� ���ӵ��� �ӵ��� ����
        velocity.y += gravity * Time.deltaTime;

        // �ӵ��� ���� ��ġ ���
        position += velocity * Time.deltaTime;

        // ���� ��ġ�� ���� ��ü ��ġ�� �ݿ�
        transform.position = position;

        // �ٴڿ� ����� üũ�ϰ� �ٴڿ� ������ �ӵ��� 0���� ����
        //if (transform.position.y <= 0)
        //{
        //    velocity.y = 0;
        //    position.y = 0;
        //}
    }
}
