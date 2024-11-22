using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class CinemachineManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cameras = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartCamMovement());
        }
    }

    IEnumerator StartCamMovement()
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(true);
            yield return new WaitForSeconds(2);
        }
    }
}
