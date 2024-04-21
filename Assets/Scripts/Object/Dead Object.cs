using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObject : MonoBehaviour
{
    [Header("Respawn")]
    public Vector3 respawnPoint; // 리스폰지점
    public string currentSceneName;
    [SerializeField] LayerMask player;
    private void OnTriggerEnter(Collider other)
    {
        // 현재 씬 이름 가져오기
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (((1 << other.gameObject.layer) & player) != 0)
        {
            //Manager.Scene.LoadScene(currentSceneName);
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
        }
    }
}
