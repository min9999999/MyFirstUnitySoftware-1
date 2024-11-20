using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    [SerializeField] List<Transform> team1 = new List<Transform>();
    [SerializeField] Transform destination1;
    [SerializeField] List<Transform> team2 = new List<Transform>();
    [SerializeField] Transform destination2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(Task1());
        //StartCoroutine("Task2");

        //StartCoroutine(Task1AndTask2());

        StartCoroutine(RunTeam1());
        StartCoroutine(RunTeam2());
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
    IEnumerator RunTeam1()
    {
        yield return Run(team1[0], destination1, 2);
        yield return Run(team1[1], destination1, 2);
        yield return Run(team1[2], destination1, 2);
    }

    
    private IEnumerator Run(Transform obj, Transform dest, float duration)
    {
        float currentTime = 0;

        print(obj + " 달리는중!");
        Vector3 originPos = obj.position;

        while ((currentTime / (duration * 0.5f)) < 1)
        {
            currentTime += Time.deltaTime;
            obj.position = Vector3.Lerp(originPos, dest.position, currentTime / (duration * 0.5f));

            if (currentTime >= (duration * 0.5f))
                break;

            yield return new WaitForEndOfFrame();
        }

        currentTime = 0;

        while ((currentTime / (duration * 0.5f)) < 1)
        {
            currentTime += Time.deltaTime;
            obj.position = Vector3.Lerp(dest.position, originPos, currentTime / (duration * 0.5f));

            if (currentTime >= (duration * 0.5f))
                break ;
            yield return new WaitForEndOfFrame();
        }

        print(obj + " 도착!");
    }

    /// <summary>
    /// Team2의 각 주자가 목적지를 찍고 2.5초 만에 원래 위치로 돌아온다.
    /// </summary>
    /// <returns></returns>
    IEnumerator RunTeam2()
    {
        yield return Run(team2[0], destination2, 2.5f);
        yield return Run(team2[1], destination2, 2.5f);
        yield return Run(team2[2], destination2, 2.5f);
    }
}
