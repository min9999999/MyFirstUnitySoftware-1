using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

/// <summary>
/// Pusher를 OriginPos에서 DestPos까지 특정 속도로 이동
/// </summary>
public class Pusher : MonoBehaviour
{
    [SerializeField] Transform pusherOriginPos;
    [SerializeField] Transform pusherDestPos;
    public float pusherSpeed;
    public bool isClockWise = true;
    public bool isMoving = false;
    Coroutine runningCoroutine;

    public void Move(bool _isClockWise)
    {
        if (isMoving) return;

        isMoving = true;

        if (_isClockWise)
        {
            runningCoroutine = StartCoroutine(RotateCW());
        }
        else
        {
            runningCoroutine = StartCoroutine(RotateCCW());
        }
    }

    public void Stop()
    {
        isMoving = false;

        StopCoroutine(runningCoroutine);
    }

    IEnumerator RotateCW()
    {
        while (true)
        {
            Vector3 direction = pusherDestPos.position - transform.position;
            float distance = direction.magnitude;

            transform.position += direction.normalized * pusherSpeed * Time.deltaTime;

            if (distance < 0.1f)
            {                
                transform.position = pusherOriginPos.position;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RotateCCW()
    {
        while (true)
        {
            Vector3 direction = pusherOriginPos.position - transform.position;
            float distance = direction.magnitude;

            transform.position += direction.normalized * pusherSpeed * Time.deltaTime;

            if( distance > 0.1f && 3.5f > distance)
            {
                // 구속 해지
                if(transform.childCount > 1)
                    transform.GetChild(1).SetParent(null);
                
            }
            if (distance < 0.1f)
            {
                transform.position = pusherDestPos.position;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Metal" || other.tag == "NonMetal")
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 구속 해지
        if (transform.childCount > 1)
            transform.GetChild(1).SetParent(null);
    }
}
