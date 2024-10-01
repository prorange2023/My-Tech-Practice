using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualRigidBody : MonoBehaviour
{
    // 오브젝트의 속도
    [SerializeField] Vector3 velocity = Vector3.zero;

    // 중력 가속도
    [SerializeField] Vector3 gravity = new Vector3(0, -9.81f, 0);

    // 공기 저항 (감쇠)
    [SerializeField] float drag = 0.1f;

    // 가속도 (외부에서 가해지는 힘)
    [SerializeField] Vector3 acceleration = Vector3.zero;

    // 속도 업데이트를 위한 시간값
    [SerializeField] float deltaTime;

    // 중력 틀지 말지 확인하는 bool 값
    [SerializeField] bool isGround;

    // 땅 Layermask
    [SerializeField] LayerMask groundLayer;

    // Start는 첫 번째 프레임 업데이트 전에 호출됩니다.
    void Start()
    {
        // 시간 초기화
        deltaTime = Time.fixedDeltaTime;
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        // 예: 키보드 입력을 받아 가속도 설정
        if (Input.GetKey(KeyCode.W))
        {
            // 전방 방향으로 가속도 증가
            acceleration += transform.forward * 10f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            // 후방 방향으로 가속도 증가
            acceleration -= transform.forward * 10f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            acceleration += transform.right * 10f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            acceleration -= transform.right * 10f;
        }

        // 공기 저항을 통한 속도 감소
        velocity *= 1 - drag * deltaTime;

        if (true == isGround)
        {
            // 중력 적용
            velocity += gravity * deltaTime;
        }
        
        // 가속도를 이용하여 속도 업데이트
        velocity += acceleration * deltaTime;

        // 속도에 따라 위치 업데이트
        transform.position += velocity * deltaTime;

        // 프레임마다 가속도 초기화
        acceleration = Vector3.zero;
    }

    void CollisionDetection()
    {
        Collider colliderA = GetComponent<Collider>();
        //Collider colliderB = otherObject.GetComponent<Collider>();

        //if (colliderA.bounds.Intersects(colliderB.bounds))
        //{
        //    Debug.Log("수동 충돌 감지!");
        //}
    }
}
