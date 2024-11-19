using System.Collections.Generic;
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

    // 일반
    public GameObject destination1;
    public GameObject destination2;
    public GameObject destination3;
    public GameObject destination4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
