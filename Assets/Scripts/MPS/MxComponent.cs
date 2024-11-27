using UnityEngine;
using ActUtlType64Lib;
using System;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class MxComponent : MonoBehaviour
{
    ActUtlType64 mxComponent;
    [SerializeField] TMP_InputField deviceInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mxComponent = new ActUtlType64();

        mxComponent.ActLogicalStationNumber = 1;
    }

    public void OnSimConnectBtnClkEvent()
    {
        int returnValue = mxComponent.Open();

        string hexValue = Convert.ToString(returnValue, 16);

        if(hexValue == "0")
        {
            print("Simulator와 연결이 잘 되었습니다.");
        }
        else
        {
            print($"에러코드를 확인해 주세요. 에러코드: {hexValue}");
        }
    }

    public void OnSimDisconnectBtnClkEvent()
    {
        int returnValue = mxComponent.Close();

        string hexValue = Convert.ToString(returnValue, 16);

        if (hexValue == "0")
        {
            print("Simulator와 연결이 해제되었습니다.");
        }
        else
        {
            print($"에러코드를 확인해 주세요. 에러코드: {hexValue}");
        }
    }

    public void OnGetDeviceBtnClkEvent()
    {
        int value;
        int returnValue = mxComponent.GetDevice(deviceInput.text, out value);

        if (returnValue == 0)
        {
            print("디바이스 읽기가 완료되었습니다.");

            deviceInput.text = value.ToString();
        }
        else
        {
            print($"에러가 발생하였습니다. 에러코드: {returnValue}");
        }
    }

    public void OnSetDeviceBtnClkEvent()
    {
        // string "X10,1" or "D20,54"
        string[] data = deviceInput.text.Split(",");

        int returnValue = mxComponent.SetDevice(data[0], int.Parse(data[1]));

        if (returnValue == 0)
        {
            print("디바이스 쓰기가 완료되었습니다.");
        }
        else
        {
            print($"에러가 발생하였습니다. 에러코드: {returnValue}");
        }
    }

    public void OnReadDeviceBlockBtnClkEvent()
    {
        int data;
        int returnValue = mxComponent.ReadDeviceBlock(deviceInput.text, 1, out data);

        if (returnValue == 0)
        {
            print("디바이스 블록 읽기가 완료되었습니다.");

            string input = Convert.ToString(data, 2);
            deviceInput.text = Reverse(input);
        }
        else
        {
            print($"에러가 발생하였습니다. 에러코드: {returnValue}");
        }
    }

    public static string Reverse(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    private void OnDestroy()
    {
        OnSimDisconnectBtnClkEvent();
    }
}
