using System.Collections;
using UnityEngine;

namespace MPS
{
    /// <summary>
    /// 공압이 공급되면 실린더 로드가 일정 거리만큼, 일정 속도로 전진 또는 후진한다.
    /// 전진 또는 후진 시, 전후진 Limit Switch(LS)가 작동한다.
    /// 속성: 실린더로드, Min-Max Range, Duration, 전후방 Limit Switch
    /// </summary>
    public class Cylinder : MonoBehaviour
    {
        [SerializeField] Transform cylinderRod;
        [SerializeField] float maxRange;
        [SerializeField] float minRange;
        [SerializeField] float duration;
        [SerializeField] Transform forwardLS;
        [SerializeField] Transform backwardLS;
        public bool isForward = false;
        public bool isRodMoving = false;

        public void OnForwardBtnClkEvent()
        {
            if (isForward || isRodMoving) return;

            StartCoroutine(MoveCylinder(cylinderRod, minRange, maxRange, duration));
        }

        public void OnBackwardBtnClkEvent()
        {
            if (!isForward || isRodMoving) return;

            StartCoroutine(MoveCylinder(cylinderRod, maxRange, minRange, duration));
        }

        IEnumerator MoveCylinder(Transform rod, float min, float max, float duration)
        {
            isRodMoving = true;

            Vector3 minPos = new Vector3(rod.transform.localPosition.x, min, rod.transform.localPosition.z);
            Vector3 maxPos = new Vector3(rod.transform.localPosition.x, max, rod.transform.localPosition.z);

            float currentTime = 0;

            while (currentTime <= duration)
            {
                currentTime += Time.deltaTime;

                rod.localPosition = Vector3.Lerp(minPos, maxPos, currentTime / duration);

                yield return new WaitForEndOfFrame();
            }

            isRodMoving = false;
            isForward = !isForward;
        }

    }

}