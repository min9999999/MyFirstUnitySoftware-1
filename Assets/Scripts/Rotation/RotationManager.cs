using UnityEngine;

public class RotationManager : MonoBehaviour
{
    [SerializeField] Transform tick_Lerp;
    [SerializeField] Transform tick_Slerp;
    Quaternion originRot_Lerp;
    Quaternion originRot_Slerp;
    Quaternion destRot;
    float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originRot_Lerp = tick_Lerp.transform.rotation;
        originRot_Slerp = tick_Slerp.transform.rotation;
        destRot = Quaternion.Euler(0, 0, -180);
    }

    // Update is called once per frame
    void Update()
    {
        RotateBySlerp(tick_Slerp);
        RotateByLerp(tick_Lerp);
    }

    void RotateBySlerp(Transform obj)
    {
        currentTime += Time.deltaTime;

        if (currentTime > 3)
            currentTime = 0;

        obj.rotation = Quaternion.Slerp(originRot_Slerp, destRot, currentTime / 3);
    }

    void RotateByLerp(Transform obj)
    {
        currentTime += Time.deltaTime;

        if (currentTime > 3)
            currentTime = 0;

        obj.rotation = Quaternion.Lerp(originRot_Slerp, destRot, currentTime / 3);
    }
}
