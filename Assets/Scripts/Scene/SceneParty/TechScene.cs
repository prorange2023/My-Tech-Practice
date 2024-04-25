using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechScene : MonoBehaviour
{
    [SerializeField] protected List<List<Enemy>> EnemyGroups;
    [SerializeField] protected List<Enemy> EnemyGroup1;
    [SerializeField] protected List<Enemy> EnemyGroup2;

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
                //Monster[0].SetActive(false);
                //Monster[1].SetActive(false);
                //Monster[2].SetActive(false);
                foreach (Enemy enemy in EnemyGroup1)
                {
                    enemy.gameObject.SetActive(false);
                }
                break;
            case 2:
                
                for (int i = 0; i < 2;  i++)
                {
                    foreach (Enemy enemy in EnemyGroup1)
                    {
                        enemy.gameObject.SetActive(false);
                    }
                }


                foreach (Enemy enemy in EnemyGroup1)
                {
                    enemy.gameObject.SetActive(false);
                }
                foreach (Enemy enemy in EnemyGroup2)
                {
                    enemy.gameObject.SetActive(false);
                }
                break;
        }
    }
}
