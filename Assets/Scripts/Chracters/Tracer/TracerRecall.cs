using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerRecall : MonoBehaviour
{
    // �÷��̾��� ��ġ�� ȸ���� ����Ͽ� �ǵ����� ����� ������ �ڵ�

    [SerializeField] private int maxRecallData = 6;  // �ִ� ���� ������ ��ġ �� ȸ�� ������ ����
    [SerializeField] private float secondsBetweenData = 0.5f;  // ��ġ �����͸� �����ϴ� ���� (��)
    [SerializeField] private float recallDuration = 1.25f;  // ��ġ�� �ǵ��� �� �ɸ��� �ð�
    [SerializeField] private float recallCooltime = 7f;  // �ǵ����� �� ��Ÿ�� (��)
    [SerializeField] private float recallCost = 1f;  // �ǵ����⸦ ���� ���, ���⼭�� �⺻�� 1�� ����

    // �÷��̾� ī�޶� ��Ʈ�ѷ�
    // private PlayerCameraController playerCameraController;

    private bool canCollectRecallData = true;  // ��ġ �����͸� ������ �� �ִ��� ����
    private float currentDataTimer = 0f;  // ��ġ ������ ���� ������ üũ�ϴ� Ÿ�̸�

    [System.Serializable]
    private class RecallData
    {
        public Vector3 CharacterPosition;  // ĳ������ ��ġ
        public Quaternion CharacterRotation;  // ĳ������ ȸ��
        // public Quaternion cameraRotation;  // ī�޶��� ȸ��
    }

    [SerializeField] private List<RecallData> recallData = new List<RecallData>();  // ��ġ�� ȸ�� �����͸� �����ϴ� ����Ʈ

    private void Start()
    {
        // �÷��̾� ī�޶� ��Ʈ�ѷ��� �ʱ�ȭ
        // playerCameraController = GetComponent<PlayerCameraController>();
    }

    private void Update()
    {
        // �� �����Ӹ��� ��ġ �����͸� ����
        StoreRecallData();

        // ����� ��ġ �����Ͱ� 2�� �̻��� ��, ������ �׷���
        if (recallData.Count >= 2)
        {
            // ����� ��ġ ���� ������ �׷� ĳ������ �̵� ������ ����� �뵵�� �ð�ȭ
            for (int i = 0; i < recallData.Count - 1; i++)
            {
                Debug.DrawLine(recallData[i].CharacterPosition, recallData[i + 1].CharacterPosition);
            }
        }
    }

    private void OnRecall()
    {
        // Recall �ڷ�ƾ�� ���� (�÷��̾ ���� ��ġ�� �ǵ���)
        StartCoroutine(Recall());
    }

    private void StoreRecallData()
    {
        // Ÿ�̸Ӹ� �������� ��ġ �����͸� ������ �ð��� ���
        currentDataTimer += Time.deltaTime;

        // ��ġ �����͸� ������ �� �ִ� �������� Ȯ��
        if (canCollectRecallData)
        {
            // Ÿ�̸Ӱ� ������ ���� ���ݺ��� ũ�� �����͸� ����
            if (currentDataTimer >= secondsBetweenData)
            {
                // ����� �����Ͱ� �ִ� �������� ���ٸ� ���� ������ �����͸� ����
                if (recallData.Count >= maxRecallData)
                {
                    recallData.RemoveAt(0);
                }

                // ���ο� ��ġ �����͸� ����Ʈ�� �߰�
                recallData.Add(GetRecallData());

                // Ÿ�̸Ӹ� �ʱ�ȭ
                currentDataTimer = 0f;
            }
        }
    }

    // ���� ĳ������ ��ġ�� ȸ�� �����͸� �������� �Լ�
    private RecallData GetRecallData()
    {
        return new RecallData
        {
            CharacterPosition = transform.position,  // ĳ������ ���� ��ġ
            CharacterRotation = transform.rotation,  // ĳ������ ���� ȸ��
            // cameraRotation = playerCameraController.transform.rotation  // ī�޶� ȸ�� (�ּ� ó����)
        };
    }

    // Recall ����� �����ϴ� �ڷ�ƾ (�÷��̾ ���� ��ġ�� �ǵ����� �۾� ����)
    private IEnumerator Recall()
    {
        // recallCost�� 1�� ���� ���� (1ȸ���� ����ǵ��� ����)
        if (recallCost == 1f)
        {
            recallCost = 0f;  // �ǵ����� ����� 0���� ���� (�ǵ����� ���� ������ ǥ��)

            // �÷��̾� ī�޶� ��״� �۾�
            // playerCameraController.Lock(true);

            canCollectRecallData = false;  // ��ġ ������ ������ ����

            // �ǵ����� �� �� ��ġ �����͸� ���� ������ �ð��� ���
            float secondsForEachData = recallDuration / recallData.Count;

            // ���� ��ġ�� ȸ���� �����Ͽ� �ǵ����� ����
            Vector3 currentDataPlayerStartPos = transform.position;
            Quaternion currentDataPlayerStartRotation = transform.rotation;
            // Quaternion currentDataCameraStartRotation = playerCameraController.transform.rotation;

            // �ǵ����� �۾��� ���� (�����Ͱ� ���� ���� �ݺ�)
            while (recallData.Count > 0)
            {
                float t = 0f;  // �̵��� �ð��� ������ ���

                // �� ��ġ �����͸� ���� õõ�� �ǵ��ư�
                while (t < secondsForEachData)
                {
                    // ���� ��ġ���� ������ ����� ��ġ�� �̵�
                    transform.position = Vector3.Lerp(currentDataPlayerStartPos, recallData[recallData.Count - 1].CharacterPosition, t / secondsForEachData);

                    // ���� ȸ������ ������ ����� ȸ������ ȸ��
                    transform.rotation = Quaternion.Lerp(currentDataPlayerStartRotation, recallData[recallData.Count - 1].CharacterRotation, t / secondsForEachData);

                    // ī�޶� ȸ���� �����ϴ� �۾�
                    // playerCameraController.transform.rotation = Quaternion.Lerp(currentDataCameraStart, recallData[recallData.Count - 1].cameraRotation, t / secondsForEachData);

                    t += Time.deltaTime;  // �ð��� ������Ŵ

                    yield return null;  // ���� �����ӱ��� ���
                }

                // ���� ��ġ�� �������� ����� ��ġ�� ������Ʈ
                currentDataPlayerStartPos = recallData[recallData.Count - 1].CharacterPosition;
                currentDataPlayerStartRotation = recallData[recallData.Count - 1].CharacterRotation;
                // currentDataCameraStartRotation = recallData[recallData.Count - 1].cameraRotation;

                // ����� �����͸� ����Ʈ���� ����
                recallData.RemoveAt(recallData.Count - 1);
            }

            // �÷��̾� ī�޶� ��� ����
            // playerCameraController.Lock(false);

            // ��ġ ������ ���� �簳
            canCollectRecallData = true;

            // ��Ÿ���� ������ �ǵ����� ����� �ٽ� 1�� ����
            yield return new WaitForSeconds(recallCooltime);
            recallCost = 1f;
        }
    }
}
