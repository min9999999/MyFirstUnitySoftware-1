using System;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 플레이어를 방향키 입력에 따라 특정 속도로 움직이게 하고싶다.
/// 필요조건: 방향, 특정속도
/// </summary>
public class PlayerMove : MonoBehaviour
{
    public float speed = 2f;
    public float rotSpeed = 2f;
    Vector3 direction; // 벡터: 위치와 크기를 가지고있는 값

    void Update()
    {
        //transform.position += Vector3.forward * speed;

        MovePlayer(speed);

        RotatePlayer(rotSpeed);
    }

    private void RotatePlayer()
    {
        float horizontalInput = Input.GetAxis("Mouse X");

        Vector3 rotationValue = Vector3.up * horizontalInput * rotSpeed;
        transform.Rotate(rotationValue * Time.deltaTime); // Vector3(0, -100~100 * 0.03, 0)

        // print(horizontalInput);
    }

    float yRotValue = 0;
    private void RotatePlayer(float rotSpeed)
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float rotationValue = horizontalInput * rotSpeed * Time.deltaTime;

        yRotValue += rotationValue;
        //print(yRotValue);

        transform.rotation *= Quaternion.Euler(0, rotationValue, 0);
    }

    // 1. Vector3.forward: 절대좌표(월드 방향) 기준
    // 2. trasform.forward: 로컬좌표(내 방향) 기준
    private void MovePlayer()
    {
        // 키입력 받기
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * speed;
        }
    }

    private void MovePlayer(float speed)
    {
        // 키의 부드러운 입력
        float h = Input.GetAxis("Horizontal"); // 좌우키 / AD키
        float v = Input.GetAxis("Vertical");   // 상하키 / WS키
        // print(String.Format("h: {0}, v: {1}", h, v));

        direction = (h * transform.right) + (v * transform.forward);

        transform.position += direction * speed * Time.deltaTime;
    }

    // 강체(RigidBody)를 가지고 있는 물체가 다른 충돌체(Collider)에 부딪혔을 때 실행
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "button")
        {
            print("초인종을 눌렀습니다.");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "button")
        {
            print("초인종을 누른 상태입니다.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "button")
        {
            print("초인종을 눌렀다 떼었습니다.");
        }
    }

    // 상대 충돌체에 isTrigger 토글 버튼이 클릭되어 있을 경우 접촉을 체크
    // * 물리엔진은 작동하지 않음
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sensor")
        {
            print("문에 진입했습니다.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sensor")
        {
            print("문에 서있습니다.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sensor")
        {
            print("문에서 나왔습니다.");
        }
    }
}
