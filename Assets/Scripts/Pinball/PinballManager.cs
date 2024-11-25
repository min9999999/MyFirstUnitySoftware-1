using System.Collections;
using TMPro;
using UnityEngine;

public class PinballManager : MonoBehaviour
{
    [SerializeField] Rigidbody sphereRB;
    [SerializeField] Transform handleLeft;
    [SerializeField] Transform handleRight;
    [SerializeField] TMP_Text pointText;
    [SerializeField] float power = 5;
    [SerializeField] int point;
    [SerializeField] int gameCount = 3;
    Vector3 originSpherePos;
    bool isBallTouched;
    Coroutine leftHandleCoroutine;
    bool isLeftHandleWorking;
    Coroutine rightHandleCoroutine;
    bool isRightHandleWorking;

    private void Start()
    {
        pointText.text = "SCORE: ";
        originSpherePos = sphereRB.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // 2. AddTorque: 특정 축 기준으로 힘을 가하여 회전
        //float turn = Input.GetAxis("Vertical");
        //sphereRB.AddTorque(Vector3.right * power * turn);

        // 스페이스바 버튼을 눌렀을 때, sphereRB에 윗쪽 방향으로 힘을 가하고 싶다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 1. AddForce
            sphereRB.AddForce(sphereRB.transform.forward * power, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateHandle(handleLeft);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.R))
        {
            RotateHandle(handleRight);
        }
    }

    public void ResetGame()
    {
        isBallTouched = false;
        sphereRB.transform.position = originSpherePos;
    }

    public void RotateHandle(Transform handle)
    {
        if(handle.name.Contains("Left") && !isLeftHandleWorking)
        {
            leftHandleCoroutine = StartCoroutine(RotateAngle(handle, 45, -45, .1f));
        }
        else if (handle.name.Contains("Right") && !isRightHandleWorking)
        {
            rightHandleCoroutine = StartCoroutine(RotateAngle(handle, -45, 45, .1f));
        }
    }

    IEnumerator RotateAngle(Transform handle, float minAngle, float maxAngle, float time)
    {
        if(handle.name.Contains("Left"))
            isLeftHandleWorking = true;
        else
            isLeftHandleWorking = true;

        Quaternion rotationMin = Quaternion.Euler(0, minAngle, 0);
        Quaternion rotationMax = Quaternion.Euler(0, maxAngle, 0);

        float currentTime = 0;

        while ((currentTime / time) <= 1)
        {
            currentTime += Time.deltaTime;

            handle.localRotation = Quaternion.Lerp(rotationMin, rotationMax, currentTime / time);

            yield return new WaitForEndOfFrame();
        }

        if (handle.name.Contains("Left"))
            isLeftHandleWorking = false;
        else
            isLeftHandleWorking = false;
    }
}
