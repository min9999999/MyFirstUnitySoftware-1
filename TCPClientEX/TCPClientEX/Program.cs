using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // 서버에 연결
            TcpClient client = new TcpListener(IPAddress.Any, 12345);
            client.Start();

            Console.WriteLine("서버에 연결됨");

            // 네트워크 스트림 얻기
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // 서버에 데이터 보내기
                string data = Console.ReadLine();
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                stream.Write(dataBytes, 0, dataBytes.Length);

                // 서버로부터 데이터 읽기
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("서버: " + response);

            }

            // 연결 종료
            client.Close();

        }
    }
}