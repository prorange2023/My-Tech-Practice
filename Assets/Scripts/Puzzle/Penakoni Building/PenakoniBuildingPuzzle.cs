using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PenakoniBuildingPuzzle : MonoBehaviour
{
    public enum Penakoni { Normal01, Normal02, Dream01, Dream02 }

    [Header("PuzzleScene")]
    [SerializeField] PuzzleScene puzzleScene;

    [SerializeField] public Vector3 normalPos1;
    [SerializeField] public Vector3 normalPos2;
    [SerializeField] public Vector3 dreamPos1;
    [SerializeField] public Vector3 dreamPos2;

   
    // 어렵군
    [Header("Puzzle")]
    [SerializeField] public bool moved;
    [SerializeField] public bool puzzleOn;
    [SerializeField] float speed = 3f; // 이동 속도 (단위: 유닛/초)
    [SerializeField] float t = 0.0f; // 보간 변수
    // 저장 왜이래

    
    public Penakoni puzzleState { get; private set; }
    public void SetEnum(Penakoni penakoni)
    {
        
    }
    public void SetPosition(Penakoni penakoni)
    {
        if (moved == false && puzzleOn == false)
        {
            penakoni = Penakoni.Normal01;
        }
        else if (moved == true && puzzleOn == false)
        {
            penakoni = Penakoni.Normal02;
        }
        else if (moved == false && puzzleOn == true)
        {
            penakoni = Penakoni.Dream01;
        }
        else
        {
            penakoni = Penakoni.Dream02;
        }
        switch (penakoni)
        {
            case Penakoni.Normal01:
                t += speed * Time.deltaTime; // 매 프레임마다 t 증가
                transform.position = Vector3.Lerp(transform.position, normalPos1, t);
                t = 0;
                break;
            case Penakoni.Normal02:
                t += speed * Time.deltaTime; // 매 프레임마다 t 증가
                transform.position = Vector3.Lerp(transform.position, normalPos2, t);
                t = 0;
                break;
            case Penakoni.Dream01:
                t += speed * Time.deltaTime; // 매 프레임마다 t 증가
                transform.position = Vector3.Lerp(transform.position, dreamPos1, t);
                t = 0;
                break;
            case Penakoni.Dream02:
                t += speed * Time.deltaTime; // 매 프레임마다 t 증가
                transform.position = Vector3.Lerp(transform.position, dreamPos2, t);
                t = 0;
                break;
        }
    }


    
    private void OnEnable()
    {
        transform.position = normalPos1;
    }
    private void Start()
    {
        puzzleScene.buildingPuzzle.Add(this);
    }
    private void Update()
    {
        SetPosition(puzzleState);
    }
    private void OnDisable()
    {
        transform.position = normalPos1;
    }
}
