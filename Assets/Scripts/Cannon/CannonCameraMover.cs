using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCameraMover : MonoBehaviour
{
    public float sensitivity = 100f;  // ���콺 ����
    public Transform playerBody;      // �÷��̾� ĳ������ Transform
    private float xRotation = 0f;     // ī�޶��� x�� ȸ������ ������ ����

    void Start()
    {
        // ���콺 Ŀ���� ��� ���·� ����
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���콺 �Է� ���� ����
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // y�� ȸ��(���Ʒ�) �� ����: ���� ȸ���� �����Ͽ� ī�޶� �������� �ʵ��� ��
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶��� ���� ȸ�� ���� (���Ʒ�)
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // �÷��̾��� ��ü ȸ�� ���� (�¿�)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
