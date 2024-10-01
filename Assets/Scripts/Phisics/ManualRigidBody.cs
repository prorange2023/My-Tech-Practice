using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRigidBody : MonoBehaviour
{
    // ������Ʈ�� �ӵ�
    [SerializeField] Vector3 velocity = Vector3.zero;

    // �߷� ���ӵ�
    [SerializeField] Vector3 gravity = new Vector3(0, -9.81f, 0);

    // ���� ���� (����)
    [SerializeField] float drag = 0.1f;

    // ���ӵ� (�ܺο��� �������� ��)
    [SerializeField] Vector3 acceleration = Vector3.zero;

    // �ӵ� ������Ʈ�� ���� �ð���
    [SerializeField] float deltaTime;

    // �߷� Ʋ�� ���� Ȯ���ϴ� bool ��
    [SerializeField] bool isGround;

    // �� Layermask
    [SerializeField] LayerMask groundLayer;

    // Start�� ù ��° ������ ������Ʈ ���� ȣ��˴ϴ�.
    void Start()
    {
        // �ð� �ʱ�ȭ
        deltaTime = Time.fixedDeltaTime;
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        // ��: Ű���� �Է��� �޾� ���ӵ� ����
        if (Input.GetKey(KeyCode.W))
        {
            // ���� �������� ���ӵ� ����
            acceleration += transform.forward * 10f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // �Ĺ� �������� ���ӵ� ����
            acceleration -= transform.forward * 10f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            acceleration += transform.right * 10f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            acceleration -= transform.right * 10f;
        }

        // ���� ������ ���� �ӵ� ����
        velocity *= 1 - drag * deltaTime;

        if (true == isGround)
        {
            // �߷� ����
            velocity += gravity * deltaTime;
        }
        
        // ���ӵ��� �̿��Ͽ� �ӵ� ������Ʈ
        velocity += acceleration * deltaTime;

        // �ӵ��� ���� ��ġ ������Ʈ
        transform.position += velocity * deltaTime;

        // �����Ӹ��� ���ӵ� �ʱ�ȭ
        acceleration = Vector3.zero;
    }

    void CollisionDetection()
    {
        Collider colliderA = GetComponent<Collider>();
        //Collider colliderB = otherObject.GetComponent<Collider>();

        //if (colliderA.bounds.Intersects(colliderB.bounds))
        //{
        //    Debug.Log("���� �浹 ����!");
        //}
    }
}
