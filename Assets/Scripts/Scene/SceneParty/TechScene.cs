using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechScene : MonoBehaviour
{
    [SerializeField] protected GameObject[] Monster;

    private void Start()
    {
        LoadRespawn();
    }

    public void LoadRespawn()
    {
        switch (Manager.Game.savePoint)
        {
            case 0:
                break;
            case 1:
                Monster[0].SetActive(false);
                Monster[1].SetActive(false);
                Monster[2].SetActive(false);
                break;
            case 2:
                Monster[0].SetActive(false);
                Monster[1].SetActive(false);
                Monster[2].SetActive(false);
                Monster[3].SetActive(false);
                Monster[4].SetActive(false);
                Monster[5].SetActive(false);
                break;
        }
    }
}
