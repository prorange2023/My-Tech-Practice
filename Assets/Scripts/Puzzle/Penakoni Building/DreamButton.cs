using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PuzzleButton : MonoBehaviour, IInterActable
{
    public List<PenakoniBuildingPuzzle> buildingPuzzles;

    public void Interact(PlayerInterActor player)
    {
        StartCoroutine(Elebator(player));
    }
    
    IEnumerator Elebator(PlayerInterActor player)
    {
        player.GetComponentInParent<PlayerSet>().GetComponent<Transform>().parent = transform;
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
        yield return new WaitForSeconds(2f);
        player.GetComponentInParent<PlayerSet>().GetComponent<Transform>().parent = null;
    }
}
