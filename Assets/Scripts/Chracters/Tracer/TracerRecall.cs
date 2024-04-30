using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerRecall : MonoBehaviour
{
    [SerializeField] private int maxRecallData = 6;
    [SerializeField] private float secondsBetweenData = 0.5f;
    [SerializeField] private float recallDuration = 1.25f;
    [SerializeField] private float recallCooltime = 7f;
    [SerializeField] private float recallCost = 1f;


    //Private playerCameraController playerCameraController
    private bool canCollectRecallData = true;
    private float currentDataTimer = 0f;

    [System.Serializable]
    private class RecallData
    {
        public Vector3 CharacterPosition;
        public Quaternion CharacterRotation;
        //public Quaternion cameraRotation;
    }

    [SerializeField] private List<RecallData> recallData = new List<RecallData>();

    private void Start()
    {
        //playerCameraController = GetComponent<PlayerCameraController>();
    }
    private void Update()
    {
        StoreRecallData();

        if (recallData.Count >= 2)
        {
            for (int i = 0; i < recallData.Count - 1; i++)
            {
                Debug.DrawLine(recallData[i].CharacterPosition, recallData[i + 1].CharacterPosition);
            }
        }
        
    }
    private void OnRecall()
    { 
        StartCoroutine(Recall());
    }
    private void StoreRecallData()
    {
        currentDataTimer += Time.deltaTime;

        if (canCollectRecallData)
        {
            if (currentDataTimer >= secondsBetweenData)
            {
                if (recallData.Count >= maxRecallData)
                {
                    recallData.RemoveAt(0);
                }
                recallData.Add(GetRecallData());

                currentDataTimer = 0f;
            }
        }
    }
    private RecallData GetRecallData()
    {
        return new RecallData
        {
            CharacterPosition = transform.position,
            CharacterRotation = transform.rotation,
            //cameraRotation = playerCameraController.transform.rotation
        };
    }

    private IEnumerator Recall()
    {
        if (recallCost == 1f)
        {
            recallCost = 0f;
            //playerCameraController.Lock(true);
            canCollectRecallData = false;

            float secondsForEachData = recallDuration / recallData.Count;
            Vector3 currentDataPlayerStartPos = transform.position;
            Quaternion currentDataPlayerStartRotation = transform.rotation;
            //Quaternion currentDataCameraStartRotation = playerCameraController.transform.rotation;

            while (recallData.Count > 0)
            {
                float t = 0f;

                while (t < secondsForEachData)
                {
                    transform.position = Vector3.Lerp(currentDataPlayerStartPos, recallData[recallData.Count - 1].CharacterPosition, t / secondsForEachData);

                    transform.rotation = Quaternion.Lerp(currentDataPlayerStartRotation, recallData[recallData.Count - 1].CharacterRotation, t / secondsForEachData);

                    //playerCameraController.transform.rotation = Quaternion.Lerp(currentDataCameraStart, recallData[recallData.Count - 1].cameraRotation, t/ secondsForEachData);

                    t += Time.deltaTime;

                    yield return null;
                }

                currentDataPlayerStartPos = recallData[recallData.Count - 1].CharacterPosition;
                currentDataPlayerStartRotation = recallData[recallData.Count - 1].CharacterRotation;
                //currentDataCameraStartRotation = recallData[recallData.Count - 1].cameraRotation;

                recallData.RemoveAt(recallData.Count - 1);

                
            }

            //playerCameraController.Lock(false);
            
            canCollectRecallData = true;
            yield return new WaitForSeconds(recallCooltime);
            recallCost = 1f;
        }
        
    }
}
