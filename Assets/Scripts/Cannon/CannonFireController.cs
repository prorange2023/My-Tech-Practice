using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CannonPredict))]
//CannonPredict ������Ʈ�� ��Ʈ
public class CannonFireController : MonoBehaviour
{
    [SerializeField] CannonPredict cannonPredict; // ������ ������ CannonPredict ������Ʈ

    [SerializeField] Rigidbody objectToThrow; // ���� ������Ʈ, Rigidbody ������Ʈ�� �Ҵ�� ������Ʈ

    [SerializeField, Range(0.0f, 50.0f)] float force; // ���� �� ������ ��, 0���� 50���� ������ ������ �� ����

    [SerializeField] Transform firePosition; // �߻� ��ġ�� ��Ÿ���� Transform

    public InputAction fire; // ���� �� ����� InputAction

    void OnEnable()
    {
        // CannonPredict ������Ʈ�� ������ (�� ��ũ��Ʈ�� ���� ��ü�� �־�� ��)
        cannonPredict = GetComponent<CannonPredict>();

        // FirePosition�� �������� �ʾ�����, ���� ��ü�� ��ġ�� �⺻������ ����
        if (firePosition == null)
            firePosition = transform;

        // fire InputAction�� Ȱ��ȭ�ϰ�, performed �̺�Ʈ�� ThrowObject �޼��带 ����, ������Ʈ���� �������� �޸� ������
        fire.Enable();
        fire.performed += FireBullet;
    }

    void Update()
    {
        // �� �����Ӹ��� ������ ����
        Predict();
        // �̰� CPU ��뷮�� ����
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FireBullet();
        //}
    }

    void Predict()
    {
        // CannonPredict�� PredictTrajectory �޼��带 ȣ���Ͽ� ��ź ������ ����
        cannonPredict.PredictTrajectory(ProjectileData());
    }

    //�����Ͱ� �Է��ϴ� �Լ�
    ProjectileProperties ProjectileData()
    {
        // ��ź�� �Ӽ� ������ ��� ProjectileProperties ��ü ����
        ProjectileProperties properties = new ProjectileProperties();

        // ���� ������Ʈ�� Rigidbody ������Ʈ ��������
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        // ��ź �Ӽ� ����
        properties.direction = firePosition.forward;    // �߻� ���� (FirePosition�� �� ����)
        properties.initialPosition = firePosition.position; // �߻� ��ġ
        properties.initialSpeed = force;  // �ʱ� �ӵ� (force �� ���)
        properties.mass = r.mass;         // ��ź�� ����
        properties.drag = r.drag;         // ��ź�� ���� ���� (drag)

        return properties; // ������ ��ź �Ӽ� ��ȯ
    }

    void FireBullet(InputAction.CallbackContext ctx)
    {
        // ���� ������Ʈ�� FirePosition ��ġ�� ���Ӱ� ����
        Rigidbody thrownObject = Instantiate(objectToThrow, firePosition.position, Quaternion.identity);

        // ������ ������Ʈ�� ���� ���Ͽ� ����
        thrownObject.AddForce(firePosition.forward * force, ForceMode.Impulse);
    }
}