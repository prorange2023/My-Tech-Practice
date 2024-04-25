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
        int savePoint = Manager.Game.savePoint;
        if (savePoint > 0 && savePoint <= EnemyGroups.Count)
        {
            foreach (Enemy enemy in EnemyGroups[savePoint - 1])
            {
                enemy.gameObject.SetActive(false);
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
