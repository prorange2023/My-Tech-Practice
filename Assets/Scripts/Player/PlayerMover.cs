using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] int moveSpeed;

    [Header("MouseSpin")]
    [SerializeField] CinemachineFreeLook[] freeLookCams;
    [SerializeField] float rotationSpeedX = 150f;
    [SerializeField] float rotationSpeedY = 2f;
    public float sensivity = 15f;


    // ������ ã�Ƽ� ����....
    private Vector3 moveDir;


    private void Start()
    {
        freeLookCams = GetComponentsInChildren<CinemachineFreeLook>();

    }
    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;

    }
    private void OnCamera(InputValue value)// Ŭ���� ���
    {
        if (value.isPressed == true)
        {
            freeLookCams[0].m_YAxis.m_MaxSpeed = rotationSpeedY;
            freeLookCams[0].m_XAxis.m_MaxSpeed = rotationSpeedX;
            freeLookCams[0].m_XAxis.m_InputAxisValue = -freeLookCams[0].m_XAxis.m_InputAxisValue;
        }
        else if (value.isPressed == false)
        {
            freeLookCams[0].m_XAxis.m_MaxSpeed = 0;
        }
    }
    private void OnJump()
    {

    }
    private void Jump()
    {

    }
    private void Move()
    {
        Vector3 forwardDir = Camera.main.transform.forward;
        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;
        Vector3 rightDir = Camera.main.transform.right;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;

        controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);

        Vector3 lookDir = forwardDir * moveDir.z + rightDir * moveDir.x;
        if (lookDir.magnitude > 0) // if (lookDir != Vector3.zero) �̰� ���귮�� ������
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
        }
        // ����̵� �����ϰ� ������ Vector3.project, ���� ����
    }
    private void Zoom()
    {
        float scrollValue = Input.mouseScrollDelta.y;
        if (freeLookCams[0].m_YAxis.Value <= 0.5f)
        {
            freeLookCams[0].m_YAxis.m_InputAxisValue = scrollValue * -0.5f;
        }
        else if ((freeLookCams[0].m_YAxis.Value >= 0.5f))
        {
            freeLookCams[0].m_YAxis.Value = 0.5f;
            freeLookCams[0].m_YAxis.m_InputAxisValue = -0.1f;
        }

        //(scrollValue > 0) // ���콺 �پ�
        //(scrollValue < 0) // ���콺 �� �ٿ�
    }
    private void Update()
    {
        Move();
        Zoom();
    }
}
