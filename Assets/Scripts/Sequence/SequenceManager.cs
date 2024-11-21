using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Obj 게임오브젝트를 일정 시간 마다 이동시킨다.
/// </summary>
public class SequenceManager : MonoBehaviour
{
    [SerializeField] Transform obj;
    [SerializeField] List<Transform> positions = new List<Transform>();
    [SerializeField] float duration;
    [SerializeField] int count;
    [SerializeField] float randomMin;
    [SerializeField] float randomMax;
    Vector3 originPos;
    //[Range(0, 1)] [SerializeField] float lerpValue;
    float currentTime;

    private void Start()
    {
        originPos = obj.position;

        //StartCoroutine(Sequence2());
        //StartCoroutine(Sequence3());
        //StartCoroutine(Sequence4());
    }
    // Update is called once per frame
    void Update()
    {
        Sequence1();
    }

    /// <summary>
    /// obj가 positions을 순회하는 메서드
    /// </summary>
    private void Sequence1()
    {
        currentTime += Time.deltaTime;

        // LERP
        float lerpValue = currentTime / duration;

        if (lerpValue >= 0 && lerpValue <= 1)
        {
            if (count - 1 >= 0)
                obj.position = Vector3.Lerp(positions[count - 1].position, positions[count].position, lerpValue);
            else
                obj.position = Vector3.Lerp(originPos, positions[count].position, lerpValue);
        }
        else
        {
            duration = Random.Range(randomMin, randomMax);

            count = (count < positions.Count - 1) ? count + 1 : 0; // 삼항연산자 (조건) ? A : B 

            currentTime = 0;
        }
    }

/*    /// <summary>
    /// obj2와 obj3이 현 위치에서 목적지까지 이동시키는 메서드
    /// </summary>
    float curTime2;
    private IEnumerator Sequence2()
    {
        while((curTime2 / 2) < 1)
        {
            curTime2 += Time.deltaTime;
            obj2.position = Vector3.Lerp(obj2OriginPos, obj2Destination, curTime2 / 2);

            yield return new WaitForEndOfFrame();
        }
    }

    float curTime3;
    private IEnumerator Sequence3()
    {
        while ((curTime3 / 3) < 1)
        {
            curTime3 += Time.deltaTime;
            obj3.position = Vector3.Lerp(obj3OriginPos, obj3Destination, curTime3 / 3);

            yield return new WaitForEndOfFrame();
        }
    }

    // Sequence2와 Sequence3를 순차적으로 실행
    IEnumerator Sequence4()
    {
        yield return Sequence2();

        yield return Sequence3();
    }*/

    // Quaternion Lerp, Quaternion Slerp

}
