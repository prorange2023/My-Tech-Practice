using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour, IInterActable
{
    public List<PenakoniBuildingPuzzle> buildingPuzzles;
    public void Dreamer()
    {
        Debug.Log("dreamer");
        foreach (var buildingPuzzle in buildingPuzzles)
        {
            if (buildingPuzzle.puzzleOn == true)
            {
                buildingPuzzle.puzzleOn = false;
            }
            else
            {
                buildingPuzzle.puzzleOn = true;
            }
        }
    }
    public void Interact(PlayerInterActor player)
    {
        Dreamer();
    }
}
