using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Save")]
    [SerializeField] public int savePoint;
    public void Test()
    {
        Debug.Log(GetInstanceID());
    }
}
