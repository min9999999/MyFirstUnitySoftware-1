using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 일정 시간에 한번 반복적으로 물체를 생성한다.
/// 속성 : 물체, 시간
/// </summary>
public class Manager : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject sphere;
    [SerializeField] List<GameObject> spawnedObj = new List<GameObject>();
    [SerializeField] int number;
    [SerializeField] float spawnTime;
    float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn(box, number);
        Spawn(sphere, number);
    }

    private void Spawn(GameObject box, int number)
    {
        for(int i = 0; i < number; i++)
        {
            GameObject go = Instantiate(box, transform);
            spawnedObj.Add(go); // 오프젝트 풀링을 위해 물체 생성시 리스트에 넣기

            go.transform.position = new Vector3(Random.Range(-10f, 10f),
                                                Random.Range(0, 20f), Random.Range(-10f, 10f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 오프젝트 풀링: 최적화 기법 중 하나, 리소스를 재사용하는 방법
            foreach (GameObject go in spawnedObj)
            {
                go.transform.position = new Vector3(Random.Range(-10f, 10f),
                                    Random.Range(0, 20f), Random.Range(-10f, 10f));
            }
        }

       /* currentTime += Time.deltaTime;

        if (currentTime > spawnTime)
        {
            GameObject go = Instantiate(box, transform);
            go.transform.position = new Vector3(Random.Range(-10f, 10f),
                                                Random.Range(0, 20f), Random.Range(-10f, 10f));

            GameObject go2 = Instantiate(sphere, transform);
            go2.transform.position = new Vector3(Random.Range(-10f, 10f),
                                                Random.Range(0, 20f), Random.Range(-10f, 10f));

            currentTime = 0;
        }*/
    }
}
