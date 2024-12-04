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
    [SerializeField] int score;
    //[SerializeField] int gameCount = 3;
    Vector3 originSpherePos;
    Quaternion originSphereRot;
    bool isBallTouched;
    Coroutine leftHandleCoroutine;
    bool isLeftHandleWorking;
    Coroutine rightHandleCoroutine;
    bool isRightHandleWorking;

    private void Start()
    {
        pointText.text = "SCORE: ";
        originSpherePos = sphereRB.transform.position;
        originSphereRot = sphereRB.transform.rotation;
        sphereRB.angularVelocity = Vector3.zero;
        sphereRB.linearVelocity = Vector3.zero;
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
        else if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            ReturnHandle(handleLeft);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateHandle(handleRight);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            ReturnHandle(handleRight);
        }
    }

    public void ResetGame()
    {
        isBallTouched = false;

        pointText.text = $"GREAT JOB\nFINAL SCORE: {score}\nIf you want to try one more time, please press ESC button.";

        sphereRB.transform.position = originSpherePos;
    }

    public void RotateHandle(Transform handle)
    {
        if(handle.name.Contains("Left") && !isLeftHandleWorking)
        {
            leftHandleCoroutine = StartCoroutine(RotateAngle(handle, 35, -45, .05f));
        }
        else if (handle.name.Contains("Right") && !isRightHandleWorking)
        {
            rightHandleCoroutine = StartCoroutine(RotateAngle(handle, -35, 45, .05f));
        }
    }

    public void ReturnHandle(Transform handle)
    {
        if (handle.name.Contains("Left") && !isLeftHandleWorking)
        {
            leftHandleCoroutine = StartCoroutine(RotateAngle(handle, -45, 35, .05f));
        }
        else if (handle.name.Contains("Right") && !isRightHandleWorking)
        {
            rightHandleCoroutine = StartCoroutine(RotateAngle(handle, 45, -35, .05f));
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

    public void UpScore(string tag)
    {
        if(tag.Contains("Obstacle"))
        {
            pointText.text = "SCORE: " + score + 50;
        }
        else if (tag.Contains("Obstacle1"))
        {
            pointText.text = "SCORE: " + (score + 100);
        }
        else if (tag.Contains("Obstacle2"))
        {
            pointText.text = "SCORE: " + (score + 1000);
        }
    }
}
