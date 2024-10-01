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

    [SerializeField] Transform FirePosition; // �߻� ��ġ�� ��Ÿ���� Transform

    public InputAction fire; // ���� �� ����� InputAction

    void OnEnable()
    {
        // CannonPredict ������Ʈ�� ������ (�� ��ũ��Ʈ�� ���� ��ü�� �־�� ��)
        cannonPredict = GetComponent<CannonPredict>();

        // FirePosition�� �������� �ʾ�����, ���� ��ü�� ��ġ�� �⺻������ ����
        if (FirePosition == null)
            FirePosition = transform;

        // fire InputAction�� Ȱ��ȭ�ϰ�, performed �̺�Ʈ�� ThrowObject �޼��带 ����
        fire.Enable();
        fire.performed += ThrowObject;
    }

    void Update()
    {
        // �� �����Ӹ��� ������ ����
        Predict();
    }

    void Predict()
    {
        // CannonPredict�� PredictTrajectory �޼��带 ȣ���Ͽ� ��ź ������ ����
        cannonPredict.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        // ��ź�� �Ӽ� ������ ��� ProjectileProperties ��ü ����
        ProjectileProperties properties = new ProjectileProperties();

        // ���� ������Ʈ�� Rigidbody ������Ʈ ��������
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        // ��ź �Ӽ� ����
        properties.direction = FirePosition.forward;    // �߻� ���� (FirePosition�� �� ����)
        properties.initialPosition = FirePosition.position; // �߻� ��ġ
        properties.initialSpeed = force;  // �ʱ� �ӵ� (force �� ���)
        properties.mass = r.mass;         // ��ź�� ����
        properties.drag = r.drag;         // ��ź�� ���� ���� (drag)

        return properties; // ������ ��ź �Ӽ� ��ȯ
    }

    void ThrowObject(InputAction.CallbackContext ctx)
    {
        // ���� ������Ʈ�� FirePosition ��ġ�� ���Ӱ� ���� (Instantiate)
        Rigidbody thrownObject = Instantiate(objectToThrow, FirePosition.position, Quaternion.identity);

        // ������ ������Ʈ�� ���� ���Ͽ� ���� (�� �������� force ���� ���Ͽ� ���� ����)
        thrownObject.AddForce(FirePosition.forward * force, ForceMode.Impulse);
    }
}