using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 셰퍼드가 물체 1, 2, 3, 4로 순차적으로 이동한다.
/// </summary>
public class ShepherdMove : MonoBehaviour
{
    // 자료구조
    public GameObject[] dest = new GameObject[4];
    public List<GameObject> destinations = new List<GameObject>();
    public Queue<GameObject> queue = new Queue<GameObject>();
    public Vector3 originPos;
    bool isWalking = false;
    bool isReturning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isWalking = true;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            isReturning = true;
        }

        if (isWalking)
        {
            Move();
        }
        else if(isReturning)
        {

        }
    }

    int count = 0;
    Vector3 dir;
    public float speed;

    void Move()
    {
        float distance = 0;

        dir = (dest[count].transform.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
/*        switch (count)
        {
            case 0:
                // 1번 위치로 가세요(반복)
                dir = (dest[count].transform.position - transform.position).normalized;
                break;
            case 1:
                // 2번 위치로 가세요(반복)
                dir = (dest[count].transform.position - transform.position).normalized;
                break;
            case 2:
                // 3번 위치로 가세요(반복)
                dir = (dest[count].transform.position - transform.position).normalized;
                break;
            case 3:
                // 0번 위치로 가세요(반복)
                dir = (dest[count].transform.position - transform.position).normalized;
                break;
        }*/

        distance = dir.magnitude;
        if (distance < 0.5f)
        {
            count++;

            if(count > 3)
                count = 0;
        }
    }
}
