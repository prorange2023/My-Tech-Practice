using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechScene : MonoBehaviour
{
    [SerializeField] protected List<List<Enemy>> enemyGroups;
    [SerializeField] protected List<Enemy> enemyGroup1;
    [SerializeField] protected List<Enemy> enemyGroup2;

    [SerializeField] protected int enemyGroupCount;
    private void Start()
    {
        enemyGroups = new List<List<Enemy>>();
        enemyGroups.Add(enemyGroup1);
        enemyGroups.Add(enemyGroup2);
        enemyGroupCount = enemyGroups.Count;
        LoadRespawn();
    }
    public void LoadRespawn()
    {
        int savePoint = Manager.Game.savePoint;
        if (savePoint > 0)
        {
            for (int i = 0; i < savePoint; i++)
            {
                Debug.Log(i);
                foreach (Enemy enemy in enemyGroups[i])
                {
                    enemy.gameObject.SetActive(false);
                }
            }
        }
        
        //switch (Manager.Game.savePoint)
        //{
        //    case 0:
        //        break;
        //    case 1:
        //        foreach (Enemy enemy in EnemyGroup1)
        //        {
        //            enemy.gameObject.SetActive(false);
        //        }
        //        break;
        //    case 2:
        //        foreach (Enemy enemy in EnemyGroup1)
        //        {
        //            enemy.gameObject.SetActive(false);
        //        }
        //        foreach (Enemy enemy in EnemyGroup2)
        //        {
        //            enemy.gameObject.SetActive(false);
        //        }
        //        break;
        //}
    }
}
