using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonPredict : MonoBehaviour
{
    #region Members
    LineRenderer trajectory;
    [SerializeField, Tooltip("The marker will show where the projectile will hit")]
    Transform hitMarker;
    [SerializeField, Range(10, 100), Tooltip("The maximum number of points the LineRenderer can have")]
    int maxPoints = 50;
    [SerializeField, Range(0.01f, 0.5f), Tooltip("The time increment used to calculate the trajectory")]
    float increment = 0.025f;
    [SerializeField, Range(1.05f, 2f), Tooltip("The raycast overlap between points in the trajectory, this is a multiplier of the length between points. 2 = twice as long")]
    float rayOverlap = 1.1f;
    #endregion

    private void Start()
    {
        // ���� ���η����� �Ҵ�
        if (trajectory == null)
            trajectory = GetComponent<LineRenderer>();

        // ������ ���̵��� ����
        SetTrajectoryVisible(true);
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        // �ʱ� �ӵ� ���: �߻�ü�� ����� �ʱ� �ӵ�, ������ ���
        Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
        Vector3 position = projectile.initialPosition;  // �߻�ü�� �ʱ� ��ġ
        Vector3 nextPosition;  // ���� �����ӿ����� ��ġ�� ������ ����
        float overlap;  // ����ĳ��Ʈ�� �Ÿ� �������� ���� ����

        // ���� �������� �ʱ� ��ġ�� ������Ʈ (����Ʈ ���� maxPoints, ù ��° ����Ʈ�� �ʱ� ��ġ)
        UpdateLineRender(maxPoints, (0, position));

        // �� �����Ӹ��� �������� ����Ͽ� �׸���
        for (int i = 1; i < maxPoints; i++)
        {
            // �ӵ��� �����ϰ�, ���ο� ������ ��ġ�� ���
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            // ������: �� ��ġ �� �Ÿ��� ���� ������ �ּ� ǥ���� ��ġ�� �ʵ��� ��
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            // Raycast�� ǥ�鿡 �ε������� Ȯ��
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                // ǥ�鿡 �ε����ٸ�, ���� �������� �ε��� �������� ������Ʈ
                UpdateLineRender(i, (i - 1, hit.point));
                MoveHitMarker(hit);  // ��Ʈ ��Ŀ�� ǥ������ �̵�
                break;  // ǥ�鿡 �ε����� ������ �׸��⸦ �ߴ�
            }

            // ǥ�鿡 �ε����� �ʾ��� ��� ����ؼ� �������� �׸��� ��Ʈ ��Ŀ�� ��Ȱ��ȭ
            hitMarker.gameObject.SetActive(false);
            position = nextPosition;  // ��ġ�� ���� ��ġ�� ����
            UpdateLineRender(maxPoints, (i, position)); // ���� �������� ���� (���⼭ count ������ �ʼ��� �ƴ�)
        }
    }

    /// <summary>
    /// ���� ����Ʈ�� ���� ���� ����Ʈ ��ġ�� ���ÿ� ������ �� �ִ� �Լ���
    /// </summary>
    /// <param name="count">���ο� �ִ� ����Ʈ�� ����</param>
    /// <param name="pointPos">���� ����Ʈ�� �ε����� ��ġ</param>
    /// Ʃ��
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos) 
    {
        trajectory.positionCount = count;  // ���� ����Ʈ�� ������ ����
        trajectory.SetPosition(pointPos.point, pointPos.pos);  // Ư�� ����Ʈ�� ��ġ�� ����
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        // �߷��� ������ �޵��� �ӵ��� ����
        velocity += Physics.gravity * increment;

        // ���������� �����Ͽ� �ӵ��� ���� (0���� 1 ���� ������)
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;  // ���ŵ� �ӵ��� ��ȯ
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        // ��Ʈ ��Ŀ�� Ȱ��ȭ�Ͽ� �ε��� ǥ���� ǥ��
        // �ӽ� ��Ȱ��ȭ, ���� ǥ�ÿ� ��ü�� �浹 ���� ��� ���������� ���� ������
        hitMarker.gameObject.SetActive(false);

        // ǥ�����κ��� �ణ ������ ��ġ�� ��Ŀ�� ��ġ
        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;  // ǥ���� ���� �������� �ణ �̵�
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);  // ��Ŀ�� ǥ���� ���ϵ��� ȸ��
    }

    public void SetTrajectoryVisible(bool visible)
    {
        // ���� ���ΰ� ��Ʈ ��Ŀ�� ǥ���ϰų� ����
        trajectory.enabled = visible;
        hitMarker.gameObject.SetActive(visible);
    }
}
