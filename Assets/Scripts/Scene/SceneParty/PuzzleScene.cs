using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScene : MonoBehaviour
{
    [Header("BuildingPuzzle")]
    [SerializeField] public List<PenakoniBuildingPuzzle> buildingPuzzle;

    void InterAction()
    {
        foreach (var puzzle in buildingPuzzle)
        {
            if (puzzle.puzzleOn == true)
            {
                puzzle.puzzleOn = false;
            }
            else if (puzzle.puzzleOn == false)
            {
                puzzle.puzzleOn = true;
            }
        }
    }
}
