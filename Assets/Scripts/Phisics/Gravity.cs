using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 initialVelocity;    // 초기 속도
    public float gravity = -9.81f;     // 중력 가속도
    private Vector3 velocity;          // 현재 속도
    private Vector3 position;          // 현재 위치


    void Start()
    {
        // 초기 속도와 위치 설정
        velocity = initialVelocity;
        position = transform.position;
    }

    void Update()
    {
        // 중력 가속도를 속도에 더함
        velocity.y += gravity * Time.deltaTime;

        // 속도에 따른 위치 계산
        position += velocity * Time.deltaTime;

        // 계산된 위치를 실제 객체 위치에 반영
        transform.position = position;

        // 바닥에 닿는지 체크하고 바닥에 닿으면 속도를 0으로 설정
        //if (transform.position.y <= 0)
        //{
        //    velocity.y = 0;
        //    position.y = 0;
        //}
    }
}
