using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CannonPredict))]
//CannonPredict 컴포넌트랑 세트
public class CannonFireController : MonoBehaviour
{
    [SerializeField] CannonPredict cannonPredict; // 궤적을 예측할 CannonPredict 컴포넌트

    [SerializeField] Rigidbody objectToThrow; // 던질 오브젝트, Rigidbody 컴포넌트가 할당된 오브젝트

    [SerializeField, Range(0.0f, 50.0f)] float force; // 던질 때 적용할 힘, 0부터 50까지 범위를 설정할 수 있음

    [SerializeField] Transform FirePosition; // 발사 위치를 나타내는 Transform

    public InputAction fire; // 던질 때 사용할 InputAction

    void OnEnable()
    {
        // CannonPredict 컴포넌트를 가져옴 (이 스크립트와 같은 객체에 있어야 함)
        cannonPredict = GetComponent<CannonPredict>();

        // FirePosition이 설정되지 않았으면, 현재 객체의 위치를 기본값으로 설정
        if (FirePosition == null)
            FirePosition = transform;

        // fire InputAction을 활성화하고, performed 이벤트에 ThrowObject 메서드를 연결
        fire.Enable();
        fire.performed += ThrowObject;
    }

    void Update()
    {
        // 매 프레임마다 궤적을 예측
        Predict();
    }

    void Predict()
    {
        // CannonPredict의 PredictTrajectory 메서드를 호출하여 포탄 궤적을 예측
        cannonPredict.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        // 포탄의 속성 정보를 담는 ProjectileProperties 객체 생성
        ProjectileProperties properties = new ProjectileProperties();

        // 던질 오브젝트의 Rigidbody 컴포넌트 가져오기
        Rigidbody r = objectToThrow.GetComponent<Rigidbody>();

        // 포탄 속성 설정
        properties.direction = FirePosition.forward;    // 발사 방향 (FirePosition의 앞 방향)
        properties.initialPosition = FirePosition.position; // 발사 위치
        properties.initialSpeed = force;  // 초기 속도 (force 값 사용)
        properties.mass = r.mass;         // 포탄의 질량
        properties.drag = r.drag;         // 포탄의 공기 저항 (drag)

        return properties; // 설정된 포탄 속성 반환
    }

    void ThrowObject(InputAction.CallbackContext ctx)
    {
        // 던질 오브젝트를 FirePosition 위치에 새롭게 생성 (Instantiate)
        Rigidbody thrownObject = Instantiate(objectToThrow, FirePosition.position, Quaternion.identity);

        // 생성된 오브젝트에 힘을 가하여 던짐 (앞 방향으로 force 값을 곱하여 힘을 가함)
        thrownObject.AddForce(FirePosition.forward * force, ForceMode.Impulse);
    }
}