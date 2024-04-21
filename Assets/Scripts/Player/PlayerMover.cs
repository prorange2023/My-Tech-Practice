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


    // 오버랩 찾아서 들어보자....
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
    private void OnCamera(InputValue value)// 클릭한 경우
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
        if (lookDir.magnitude > 0) // if (lookDir != Vector3.zero) 이게 연산량은 적을듯
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10);
        }
        // 언덕이동 구현하고 싶으면 Vector3.project, 투영 조사
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

        //(scrollValue > 0) // 마우스 휠업
        //(scrollValue < 0) // 마우스 휠 다운
    }
    private void Update()
    {
        Move();
        Zoom();
    }
}
