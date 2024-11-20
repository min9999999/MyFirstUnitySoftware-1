using System.Drawing;
using UnityEngine;

// 마우스 Y 입력을 받아, 카메라를 X축 기준으로 회전
// 속성: 회전속도, 회전각도
public class LookRotate : MonoBehaviour
{
    public float rotSpeed;
    float rotAngle;

    // Update is called once per frame
    void Update()
    {
        float mouseInput = Input.GetAxis("Mouse Y"); // -1 ~ 1
        float rotValue = mouseInput * rotSpeed * Time.deltaTime;

        rotAngle += rotValue;
        //print(rotAngle);

        rotAngle = Mathf.Clamp(rotAngle, -90, 90);

        transform.localRotation = Quaternion.Euler(-rotAngle, 0, 0);
    }
}
