using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerRecall : MonoBehaviour
{
    // 플레이어의 위치와 회전을 기록하여 되돌리는 기능을 구현한 코드

    [SerializeField] private int maxRecallData = 6;  // 최대 저장 가능한 위치 및 회전 정보의 개수
    [SerializeField] private float secondsBetweenData = 0.5f;  // 위치 데이터를 저장하는 간격 (초)
    [SerializeField] private float recallDuration = 1.25f;  // 위치를 되돌릴 때 걸리는 시간
    [SerializeField] private float recallCooltime = 7f;  // 되돌리기 후 쿨타임 (초)
    [SerializeField] private float recallCost = 1f;  // 되돌리기를 위한 비용, 여기서는 기본값 1로 설정

    // 플레이어 카메라 컨트롤러
    // private PlayerCameraController playerCameraController;

    private bool canCollectRecallData = true;  // 위치 데이터를 저장할 수 있는지 여부
    private float currentDataTimer = 0f;  // 위치 데이터 저장 간격을 체크하는 타이머

    [System.Serializable]
    private class RecallData
    {
        public Vector3 CharacterPosition;  // 캐릭터의 위치
        public Quaternion CharacterRotation;  // 캐릭터의 회전
        // public Quaternion cameraRotation;  // 카메라의 회전
    }

    [SerializeField] private List<RecallData> recallData = new List<RecallData>();  // 위치와 회전 데이터를 저장하는 리스트

    private void Start()
    {
        // 플레이어 카메라 컨트롤러를 초기화
        // playerCameraController = GetComponent<PlayerCameraController>();
    }

    private void Update()
    {
        // 매 프레임마다 위치 데이터를 저장
        StoreRecallData();

        // 저장된 위치 데이터가 2개 이상일 때, 궤적을 그려줌
        if (recallData.Count >= 2)
        {
            // 저장된 위치 간에 라인을 그려 캐릭터의 이동 궤적을 디버깅 용도로 시각화
            for (int i = 0; i < recallData.Count - 1; i++)
            {
                Debug.DrawLine(recallData[i].CharacterPosition, recallData[i + 1].CharacterPosition);
            }
        }
    }

    private void OnRecall()
    {
        // Recall 코루틴을 실행 (플레이어를 이전 위치로 되돌림)
        StartCoroutine(Recall());
    }

    private void StoreRecallData()
    {
        // 타이머를 증가시켜 위치 데이터를 저장할 시간을 계산
        currentDataTimer += Time.deltaTime;

        // 위치 데이터를 저장할 수 있는 상태인지 확인
        if (canCollectRecallData)
        {
            // 타이머가 설정한 저장 간격보다 크면 데이터를 저장
            if (currentDataTimer >= secondsBetweenData)
            {
                // 저장된 데이터가 최대 개수보다 많다면 가장 오래된 데이터를 삭제
                if (recallData.Count >= maxRecallData)
                {
                    recallData.RemoveAt(0);
                }

                // 새로운 위치 데이터를 리스트에 추가
                recallData.Add(GetRecallData());

                // 타이머를 초기화
                currentDataTimer = 0f;
            }
        }
    }

    // 현재 캐릭터의 위치와 회전 데이터를 가져오는 함수
    private RecallData GetRecallData()
    {
        return new RecallData
        {
            CharacterPosition = transform.position,  // 캐릭터의 현재 위치
            CharacterRotation = transform.rotation,  // 캐릭터의 현재 회전
            // cameraRotation = playerCameraController.transform.rotation  // 카메라 회전 (주석 처리됨)
        };
    }

    // Recall 기능을 구현하는 코루틴 (플레이어를 이전 위치로 되돌리는 작업 수행)
    private IEnumerator Recall()
    {
        // recallCost가 1일 때만 실행 (1회에만 실행되도록 제어)
        if (recallCost == 1f)
        {
            recallCost = 0f;  // 되돌리기 비용을 0으로 설정 (되돌리기 실행 중임을 표시)

            // 플레이어 카메라를 잠그는 작업
            // playerCameraController.Lock(true);

            canCollectRecallData = false;  // 위치 데이터 저장을 중지

            // 되돌리기 중 각 위치 데이터를 통해 움직일 시간을 계산
            float secondsForEachData = recallDuration / recallData.Count;

            // 현재 위치와 회전을 저장하여 되돌리기 시작
            Vector3 currentDataPlayerStartPos = transform.position;
            Quaternion currentDataPlayerStartRotation = transform.rotation;
            // Quaternion currentDataCameraStartRotation = playerCameraController.transform.rotation;

            // 되돌리기 작업을 수행 (데이터가 있을 동안 반복)
            while (recallData.Count > 0)
            {
                float t = 0f;  // 이동할 시간의 비율을 계산

                // 각 위치 데이터를 따라 천천히 되돌아감
                while (t < secondsForEachData)
                {
                    // 현재 위치에서 이전에 저장된 위치로 이동
                    transform.position = Vector3.Lerp(currentDataPlayerStartPos, recallData[recallData.Count - 1].CharacterPosition, t / secondsForEachData);

                    // 현재 회전에서 이전에 저장된 회전으로 회전
                    transform.rotation = Quaternion.Lerp(currentDataPlayerStartRotation, recallData[recallData.Count - 1].CharacterRotation, t / secondsForEachData);

                    // 카메라 회전을 변경하는 작업
                    // playerCameraController.transform.rotation = Quaternion.Lerp(currentDataCameraStart, recallData[recallData.Count - 1].cameraRotation, t / secondsForEachData);

                    t += Time.deltaTime;  // 시간을 증가시킴

                    yield return null;  // 다음 프레임까지 대기
                }

                // 현재 위치를 마지막에 저장된 위치로 업데이트
                currentDataPlayerStartPos = recallData[recallData.Count - 1].CharacterPosition;
                currentDataPlayerStartRotation = recallData[recallData.Count - 1].CharacterRotation;
                // currentDataCameraStartRotation = recallData[recallData.Count - 1].cameraRotation;

                // 사용한 데이터를 리스트에서 제거
                recallData.RemoveAt(recallData.Count - 1);
            }

            // 플레이어 카메라를 잠금 해제
            // playerCameraController.Lock(false);

            // 위치 데이터 수집 재개
            canCollectRecallData = true;

            // 쿨타임이 지나면 되돌리기 비용을 다시 1로 설정
            yield return new WaitForSeconds(recallCooltime);
            recallCost = 1f;
        }
    }
}
