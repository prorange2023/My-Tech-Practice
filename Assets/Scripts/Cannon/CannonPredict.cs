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
        // 궤적 라인랜더러 할당
        if (trajectory == null)
            trajectory = GetComponent<LineRenderer>();

        // 궤적을 보이도록 설정
        SetTrajectoryVisible(true);
    }

    public void PredictTrajectory(ProjectileProperties projectile)
    {
        // 초기 속도 계산: 발사체의 방향과 초기 속도, 질량을 사용
        Vector3 velocity = projectile.direction * (projectile.initialSpeed / projectile.mass);
        Vector3 position = projectile.initialPosition;  // 발사체의 초기 위치
        Vector3 nextPosition;  // 다음 프레임에서의 위치를 저장할 변수
        float overlap;  // 레이캐스트의 거리 오버랩을 위한 변수

        // 라인 렌더러를 초기 위치로 업데이트 (포인트 수는 maxPoints, 첫 번째 포인트는 초기 위치)
        UpdateLineRender(maxPoints, (0, position));

        // 각 프레임마다 포물선을 계산하여 그리기
        for (int i = 1; i < maxPoints; i++)
        {
            // 속도를 갱신하고, 새로운 예측된 위치를 계산
            velocity = CalculateNewVelocity(velocity, projectile.drag, increment);
            nextPosition = position + velocity * increment;

            // 오버랩: 두 위치 간 거리에 작은 여유를 둬서 표면을 놓치지 않도록 함
            overlap = Vector3.Distance(position, nextPosition) * rayOverlap;

            // Raycast로 표면에 부딪혔는지 확인
            if (Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                // 표면에 부딪혔다면, 라인 렌더러에 부딪힌 지점까지 업데이트
                UpdateLineRender(i, (i - 1, hit.point));
                MoveHitMarker(hit);  // 히트 마커를 표면으로 이동
                break;  // 표면에 부딪히면 포물선 그리기를 중단
            }

            // 표면에 부딪히지 않았을 경우 계속해서 포물선을 그리며 히트 마커는 비활성화
            hitMarker.gameObject.SetActive(false);
            position = nextPosition;  // 위치를 다음 위치로 갱신
            UpdateLineRender(maxPoints, (i, position)); // 라인 렌더러를 갱신 (여기서 count 설정은 필수는 아님)
        }
    }

    /// <summary>
    /// 라인 포인트의 수와 개별 포인트 위치를 동시에 설정할 수 있는 함수ㅋ
    /// </summary>
    /// <param name="count">라인에 있는 포인트의 개수</param>
    /// <param name="pointPos">개별 포인트의 인덱스와 위치</param>
    /// 튜플
    private void UpdateLineRender(int count, (int point, Vector3 pos) pointPos) 
    {
        trajectory.positionCount = count;  // 라인 포인트의 개수를 설정
        trajectory.SetPosition(pointPos.point, pointPos.pos);  // 특정 포인트의 위치를 설정
    }

    private Vector3 CalculateNewVelocity(Vector3 velocity, float drag, float increment)
    {
        // 중력의 영향을 받도록 속도를 갱신
        velocity += Physics.gravity * increment;

        // 공기저항을 적용하여 속도를 감소 (0에서 1 사이 값으로)
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;  // 갱신된 속도를 반환
    }

    private void MoveHitMarker(RaycastHit hit)
    {
        // 히트 마커를 활성화하여 부딪힌 표면을 표시
        // 임시 비활성화, 궤적 표시용 구체로 충돌 꺼서 사용 가능하지만 보기 안좋음
        hitMarker.gameObject.SetActive(false);

        // 표면으로부터 약간 떨어진 위치에 마커를 배치
        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;  // 표면의 법선 방향으로 약간 이동
        hitMarker.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);  // 마커가 표면을 향하도록 회전
    }

    public void SetTrajectoryVisible(bool visible)
    {
        // 궤적 라인과 히트 마커를 표시하거나 숨김
        trajectory.enabled = visible;
        hitMarker.gameObject.SetActive(visible);
    }
}
