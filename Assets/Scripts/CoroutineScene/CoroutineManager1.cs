using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorountineManager1 : MonoBehaviour
{
    [SerializeField] List<Transform> obj1 = new List<Transform>();
    [SerializeField] List<Vector3> obj1OriginPos = new List<Vector3>();
    [SerializeField] Vector3 obj1Destination;

    [SerializeField] List<Transform> obj2 = new List<Transform>();
    [SerializeField] List<Vector3> obj2OriginPos = new List<Vector3>();
    [SerializeField] Vector3 obj2Destination;
    int count1 = 0;
    int count2 = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            obj1OriginPos[i] = obj1[i].position;
            obj2OriginPos[i] = obj2[i].position;
        }

        /*        StartCoroutine(Task1());
                StartCoroutine("Task2");*/
        //StartCoroutine(Task1AndTask2());

        StartCoroutine(RunTeam1());
        StartCoroutine(RunTeam2());
        //print("count: " + count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Task1()
    {
        print("Task 시작");
        yield return new WaitForSeconds(1);
        print("Task 끝");
    }

    IEnumerator Task2()
    {
        print("Task2 시작");
        yield return new WaitForSeconds(2);
        print("Task2 끝");
    }

    IEnumerator Task1AndTask2()
    {
        yield return Task1();
        yield return Task2();
    }

    /// <summary>
    /// Team1의 각 주자가 목적지를 찍고 2초만에 원래 위치로 돌아온다.
    /// </summary>
    /// <returns></returns>
    public float curTime1;
    IEnumerator RunTeam1()
    {

        while (true)
        {
            if ((curTime1 / 2) < 1)
            {
                curTime1 += Time.deltaTime;
                obj1[count1].position = Vector3.Lerp(obj1OriginPos[count1], obj1Destination, curTime1 / 2);
                //print(curTime1);

            }
            else if ((curTime1 / 2) > 1 && (curTime1 / 2) < 2)
            {
                curTime1 += Time.deltaTime;
                obj1[count1].position = Vector3.Lerp(obj1Destination, obj1OriginPos[count1], curTime1 / 4);
            }
            else if ((curTime1 / 2) > 2)
            {
                count1++;
                if (count1 >= obj1OriginPos.Count)
                    count1 = 0;
                curTime1 = 0;
            }
            print("Time: " + (curTime1 / 2));
            yield return new WaitForEndOfFrame();
        }


    }
    /// <summary>
    /// Team2의 각 주자가 목적지를 찍고 2.5초만에 원래 위치로 돌아온다.
    /// </summary>
    /// <returns></returns>
    float curTime2;
    IEnumerator RunTeam2()
    {
        while (true)
        {
            if ((curTime2 / 2.5f) < 1)
            {
                curTime2 += Time.deltaTime;
                obj2[count2].position = Vector3.Lerp(obj2OriginPos[count2], obj2Destination, (curTime2 / 2.5f));
            }
            else if ((curTime2 / 2.5f) > 1 && (curTime2 / 2.5f) < 2)
            {
                curTime2 += Time.deltaTime;
                obj2[count2].position = Vector3.Lerp(obj2Destination, obj2OriginPos[count2], curTime2 / 5);
            }
            else if ((curTime2 / 2.5f) > 2)
            {
                count2++;
                if (count2 >= obj2OriginPos.Count)
                    count2 = 0;
                curTime2 = 0;
            }
            print("Time: " + (curTime2 / 2.5f));
            yield return new WaitForEndOfFrame();
        }
    }
}
