using System.Net.Sockets;
using System;
using UnityEngine;
using System.IO;
using System.Text;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class TCPClient : MonoBehaviour
{
    public static TCPClient Instance;

    [SerializeField] TMP_InputField dataInput;
    public bool isConnected;
    public float interval;
    public string xDevices = "0000000000000000";
    public string yDevices = "0000000000000000";
    public string dDevices = "0000000000000000";

    TcpClient client;
    NetworkStream stream;
    string msg;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            // ������ ����
            client = new TcpClient("127.0.0.1", 12345);
            print("������ �����");

            // ��Ʈ��ũ ��Ʈ�� ���
            stream = client.GetStream();
        }
        catch (Exception ex)
        {
            print(ex);
            print("������ ���� �۵����� �ּ���.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnConnectBtnClkEvent()
    {
       msg =  Request("Connect"); // CONNECT �� ��� OK

        if(msg == "CONNECTED")
        {
            isConnected = true;

            StartCoroutine(CoRequest());
        }
    }

    public void OnDisConnectBtnClkEvent()
    {
        msg = Request("Disconnect"); // DISCONNECT �� ��� OK

        if(msg == "DISCONNECTED")
        {
            isConnected = false;
        }
    }

     IEnumerator CoRequest()
    {
        while(isConnected)
        {
            // 1. MPS�� X ����̽� ������ ���������� �����Ѵ�.

            // 2. PLC�� Y ����̽� ������ 2���� ���·� �޴´�.

           string returnValue = WriteDevices("X0", 1, xDevices);
            print(returnValue);

            string data = Request("temp"); // GET, X0, 1 /  SET, X0, 1

            yield return new WaitForSeconds(interval);
        }
    }

    string totalMsg;
    public string WriteDevices(string deviceName, int blockSize, string data)
    {
        // data = "1101010001000000" or "110101000100000011010100010000001101010001000000" -> 555
        int[] convertedData = new int[blockSize];

        ConvertData(data);

        void ConvertData(string data)
        {
            for (int i = 0; i < blockSize; i++)
            {
                string splited = data.Substring(i * 16, 16);        // 1101010001000000
                splited = Reverse(splited);                         // 0000001000101011(reversed)
                convertedData[i] = Convert.ToInt32(splited, 2);     // 555(10���� ��ȯ)
            }
        }

        // 128, 64, 266,
       
        foreach(var d in convertedData)
        {
            totalMsg += d + ",";
        }

        // Server�� ������ ����
        string returnValue = Request($"{deviceName},{blockSize},{data},{totalMsg}"); // SET, X0, 3, 128, 64, 266

        return returnValue;
    }

    public static string Reverse(string input)
    {
        char[] chars = input.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public void Request()
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(dataInput.text);
        stream.Write(dataBytes, 0, dataBytes.Length);

        // �����κ��� ������ �б�
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("����: " + response);
    }

    public string Request(string message)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(message);
        stream.Write(dataBytes, 0, dataBytes.Length);

        // �����κ��� ������ �б�
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("����: " + response);

        return response;  
    }

    private void OnDestroy()
    {
        Request("Disconnect&Quit");

        if (isConnected)
        {
            isConnected = false;
        }
    }
}
