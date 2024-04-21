using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObject : MonoBehaviour
{
    [Header("Respawn")]
    public Vector3 respawnPoint; // ����������
    public string currentSceneName;
    [SerializeField] LayerMask player;
    private void OnTriggerEnter(Collider other)
    {
        // ���� �� �̸� ��������
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (((1 << other.gameObject.layer) & player) != 0)
        {
            //Manager.Scene.LoadScene(currentSceneName);
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
        }
    }
}
