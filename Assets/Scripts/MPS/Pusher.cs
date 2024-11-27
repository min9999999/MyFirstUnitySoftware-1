using System.Collections;
using UnityEngine;

/// <summary>
/// Pusher를 OriginPos에서 DestPos까지 특정 속도로 이동
/// </summary>
public class Pusher : MonoBehaviour
{
    [SerializeField] Transform pusherDestPos;
    Vector3 pusherOriginPos;
    public float pusherSpeed;
    public bool isClockWise = true;

    private void Start()
    {
        pusherOriginPos = transform.localPosition;
    }

    public void Move(bool _isClockWise)
    {
        if(_isClockWise)
        {
            StartCoroutine(RotateCW());
        }
        else
        {
            StartCoroutine(RotateCCW());
        }
    }

    IEnumerator RotateCW()
    {
        while (true)
        {
            Vector3 direction = pusherDestPos.position - transform.position;
            float distance = direction.magnitude;

            transform.position += direction.normalized * pusherSpeed * Time.deltaTime;

            if (distance < 0.5f)
            {
                transform.position = pusherOriginPos;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RotateCCW()
    {
        while (true)
        {
            Vector3 direction = pusherOriginPos - transform.position;
            float distance = direction.magnitude;

            transform.position += direction.normalized * pusherSpeed * Time.deltaTime;

            if (distance < 0.5f)
            {
                transform.position = pusherDestPos.position;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
    }
}
