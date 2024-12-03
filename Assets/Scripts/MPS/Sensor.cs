using UnityEngine;

namespace MPS
{
    /// <summary>
    /// 센서의 콜라이더에 금속 또는 비금속 물체가 닿았을 경우 센서를 활성화시킨다.
    /// 속성: 센서타입, 센서 활성화 유무
    /// </summary>
    public class Sensor : MonoBehaviour
    {
        public enum SensorType
        {
            근접센서,
            유도형센서,
            용량형센서
        }
        public SensorType sensorType = SensorType.근접센서;
        public bool isEnabled = false;

        private void OnTriggerStay(Collider other)
        {
            if(sensorType == SensorType.근접센서 || sensorType == SensorType.용량형센서)
            {
                isEnabled = true;
                print("물체 감지");
            }
            else if(sensorType == SensorType.유도형센서)
            {
                if(other.tag == "Metal")
                {
                    isEnabled = true;
                    print("금속 감지");
                }
            }
        }

        int count = 0;

        private void OnTriggerExit(Collider other)
        {
            if (isEnabled)
            {
                isEnabled = false;
                if (sensorType == SensorType.근접센서)
                {
                    count++;
                    print($"제품 갯수는 {count} 입니다.");
                }
            }

        }
    }
}

