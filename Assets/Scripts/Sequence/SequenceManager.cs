using UnityEngine;
using System.Collections.Generic;

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
    }
    // Update is called once per frame
    void Update()
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

            count = (count < positions.Count - 1) ? count + 1 : 0;

            currentTime = 0;
        }
    }
}
