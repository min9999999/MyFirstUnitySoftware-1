using System.Collections.Generic;
using UnityEngine;

namespace MPS
{
    /// <summary>
    /// 모든 설비의 상태를 확인, 물체를 생성
    /// 속성: 각 설비들, 물체프리팹 변수
    /// </summary>
    public class MPSManager : MonoBehaviour
    {
        [Header("Facilities")]
        [SerializeField] List<Cylinder> cylinders = new List<Cylinder>();
        [SerializeField] List<MeshRenderer> lamps = new List<MeshRenderer>();
        [SerializeField] List<Pusher> pushers = new List<Pusher>();
        [SerializeField] List<Sensor> sensors = new List<Sensor>();

        [Space(20)]
        [Header("Etc")]
        [SerializeField] GameObject[] objPrefabs;
        [SerializeField] Transform spawnPos;
        int count;
        Color redLamp;
        Color yellowLamp;
        Color greenLamp;

        private void Start()
        {
            redLamp = lamps[0].material.GetColor("_BaseColor");
            yellowLamp = lamps[1].material.GetColor("_BaseColor");
            greenLamp = lamps[2].material.GetColor("_BaseColor");
        }

        public void OnSpawnObjBtnClkEvent()
        {
            if (count > objPrefabs.Length - 1) count = 0;

            Instantiate(objPrefabs[count++], spawnPos.position, Quaternion.identity, transform);
            //obj.transform.position = spawnPos.position;
        }

        public void OnLampOnOffBtnClkEvent(string name)
        {
            Color color;

            switch (name)
            {
                case "Red":
                    color = lamps[0].material.GetColor("_BaseColor");
                    print(color);

                    if (color == redLamp)
                    {
                        lamps[0].material.SetColor("_BaseColor", Color.black);
                    }
                    else if (color == Color.black)
                    {
                        lamps[0].material.SetColor("_BaseColor", redLamp);
                    }
                    break;

                case "Yellow":
                    color = lamps[1].material.GetColor("_BaseColor");
                    print(color);

                    if (color == yellowLamp)
                    {
                        lamps[1].material.SetColor("_BaseColor", Color.black);
                    }
                    else if (color == Color.black)
                    {
                        lamps[1].material.SetColor("_BaseColor", yellowLamp);
                    }
                    break;

                case "Green":
                    color = lamps[2].material.GetColor("_BaseColor");
                    print(color);

                    if (color == greenLamp)
                    {
                        lamps[2].material.SetColor("_BaseColor", Color.black);
                    }
                    else if (color == Color.black)
                    {
                        lamps[2].material.SetColor("_BaseColor", greenLamp);
                    }
                    break;
            }
        }

        public void OnConvCWBtnClkEvent()
        {
            foreach (var pusher in pushers)
                pusher.Move(true);
        }

        public void OnConvCCWBtnClkEvent()
        {
            foreach (var pusher in pushers)
                pusher.Move(false);
        }

        public void OnConvStopBtnClkEvent()
        {
            foreach (var pusher in pushers)
                pusher.Stop();
        }
    }

}