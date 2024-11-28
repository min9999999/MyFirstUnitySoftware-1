using System.Collections;
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
        [SerializeField] int startBtnState = 0;
        [SerializeField] int stopBtnState = 0;
        [SerializeField] int eStopBtnState = 0;

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

        private void Update()
        {
            if (MxComponent.Instance.state == MxComponent.State.DISCONNECTED)
                return;

            if(MxComponent.Instance.yDevices.Length == 0) return;

            int 공급실린더전진 = MxComponent.Instance.yDevices[0] - '0';
            int 공급실린더후진 = MxComponent.Instance.yDevices[1] - '0';
            int 가공실린더전진 = MxComponent.Instance.yDevices[2] - '0';
            int 가공실린더후진 = MxComponent.Instance.yDevices[3] - '0';
            int 송출실린더전진 = MxComponent.Instance.yDevices[4] - '0';
            int 송출실린더후진 = MxComponent.Instance.yDevices[5] - '0';
            int 배출실린더전진 = MxComponent.Instance.yDevices[6] - '0';
            int 배출실린더후진 = MxComponent.Instance.yDevices[7] - '0';

            if (공급실린더전진 == 1) cylinders[0].OnForwardBtnClkEvent();
            else if (공급실린더후진 == 1) cylinders[0].OnBackwardBtnClkEvent();

            if (가공실린더전진 == 1) cylinders[1].OnForwardBtnClkEvent();
            else if (가공실린더후진 == 1) cylinders[1].OnBackwardBtnClkEvent();

            if (송출실린더전진 == 1) cylinders[2].OnForwardBtnClkEvent();
            else if (송출실린더후진 == 1) cylinders[2].OnBackwardBtnClkEvent();

            if (배출실린더전진 == 1) cylinders[3].OnForwardBtnClkEvent();
            else if (배출실린더후진 == 1) cylinders[3].OnBackwardBtnClkEvent();

            // 시작버튼,정지버튼,긴급정지버튼,공급센서,물체확인센서,금속확인센서

            MxComponent.Instance.xDevices = $"{startBtnState}{stopBtnState}{eStopBtnState}" +
                                            $"{(sensors[0].isEnabled == true ? 1 : 0)}" +
                                            $"{(sensors[1].isEnabled == true ? 1 : 0)}" +
                                            $"{(sensors[2].isEnabled == true ? 1 : 0)}" + "0000000000";
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

        public void OnStartBtnClkEvent()
        {
            startBtnState = 1;
            stopBtnState = 0;
        }
        public void OnStopBtnClkEvent()
        {
            stopBtnState = 1;
            startBtnState = 0;
        }

        public void OnEStopBtnClkEvent()
        {
            eStopBtnState = (eStopBtnState == 1) ? 0 : 1;
            startBtnState = 0;
            stopBtnState = 0;
        }
    }

}