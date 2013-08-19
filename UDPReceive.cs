using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour {
    Thread receiveThread;
    UdpClient client;

    public int port;

    public string lastReceivedUDPPacket="";
    public string allReceivedUDPPackets=""; // clean up this from time to time!

    public void Start()
    {
        init();
    }

    void OnGUI()
    {
        Rect rectObj = new Rect(40,10,200,400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"

                    + "shell> nc -u 127.0.0.1 : "+port+" \n"

                    + "\nLast Packet: \n"+ lastReceivedUDPPacket

                    + "\n\nAll Messages: \n"+allReceivedUDPPackets

                ,style);
    }

    // init
    private void init()
    {
        port = 1200;
        client = new UdpClient(port);
        // receiveThread = new Thread(new ThreadStart(ReceiveData));
        // receiveThread.IsBackground = true;
        // receiveThread.Start();
    }

    void Update()
    {
        ReceiveData();
    }

    /**
     * 接收数据
     */
    private  void ReceiveData()
    {
        while (true)
        {
            Debug.Log("1");
            try
            {
                Debug.Log("2");
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                Debug.Log(anyIP);
                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

                print(">> " + text);

                lastReceivedUDPPacket=text;
                allReceivedUDPPackets=allReceivedUDPPackets+text;
            }
            
            catch (Exception err)
            {
                Debug.Log("error!");
                print(err.ToString());
            }
            Debug.Log("ReceiveData ing...");
        }

    }

    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets="";
        return lastReceivedUDPPacket;
    }

}