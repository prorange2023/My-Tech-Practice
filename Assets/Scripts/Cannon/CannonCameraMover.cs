using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCameraMover : MonoBehaviour
{
    public float sensitivity = 100f;  // 마우스 감도
    public Transform playerBody;      // 플레이어 캐릭터의 Transform
    private float xRotation = 0f;     // 카메라의 x축 회전값을 저장할 변수

    void Start()
    {
        // 마우스 커서를 잠금 상태로 설정
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력 값을 읽음
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // y축 회전(위아래) 값 제한: 상하 회전을 제한하여 카메라가 뒤집히지 않도록 함
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라의 로컬 회전 설정 (위아래)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 플레이어의 몸체 회전 설정 (좌우)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
