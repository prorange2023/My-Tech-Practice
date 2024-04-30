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
    [SerializeField] public Vector3 DreamPos1;
    [SerializeField] public Vector3 DreamPos2;

   
    // ¾î·Æ±º
    [Header("Puzzle")]
    [SerializeField] bool moved;
    [SerializeField] bool puzzleOn;
    
    public void SetEnum()
    {
        
    }
    public void SetPosition(Penakoni penakoni)
    {
        switch(penakoni)
        {
            case Penakoni.Normal01: 
                transform.position = normalPos1;
                break;
            case Penakoni.Normal02:
                transform.position = normalPos2;
                break;
            case Penakoni.Dream01:
                transform.position = DreamPos1;
                break;
            case Penakoni.Dream02:
                transform.position = DreamPos2;
                break;
        }

    }


    
    private void OnEnable()
    {
        normalPos1 = transform.position;
    }
    private void Start()
    {
        puzzleScene.buildingPuzzle.Add(this);
    }
    private void OnDisable()
    {
        transform.position = normalPos1;
    }


}
