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
    public float speed;

    Vector3 originPos;
    bool isWalking = false;
    bool isReturning = false;
    int count = 0;
    Vector3 dir;

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
            isWalking = !isWalking;

            isReturning = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            isReturning = !isReturning;
         
            isWalking = false;
        }

        if (isWalking)
        {
            Move();
        }
        else if(isReturning)
        {
            Vector3 newDir = (originPos - transform.position).normalized;
            transform.position += newDir * speed * Time.deltaTime;
            transform.forward = newDir;
        }
    }


    void Move()
    {
        float distance = 0;

        dir = dest[count].transform.position - transform.position;
        distance = dir.magnitude;
        dir = dir.normalized;

        transform.position += dir * speed * Time.deltaTime;
        transform.forward = dir;

        if (distance < 0.5f)
        {
            count++;

            if(count > 3)
                count = 0;
        }
    }
}
