using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour {

    // Thread receiveThread;
    // UdpClient client;

    // public int port;

    // public string lastReceivedUDPPacket="";
    // public string allReceivedUDPPackets=""; // clean up this from time to time!

    // public void Start()
    // {
    //     init();
    // }

    // void OnGUI()
    // {
    //     Rect rectObj = new Rect(40,10,200,400);
    //     GUIStyle style = new GUIStyle();
    //     style.alignment = TextAnchor.UpperLeft;
    //     GUI.Box(rectObj,"# UDPReceive\n127.0.0.1 "+port+" #\n"

    //                 + "shell> nc -u 127.0.0.1 : "+port+" \n"

    //                 + "\nLast Packet: \n"+ lastReceivedUDPPacket

    //                 + "\n\nAll Messages: \n"+allReceivedUDPPackets

    //             ,style);
    // }

    // // init
    // private void init()
    // {
    //     port = 1200;
    //     client = new UdpClient(port);
    //     // receiveThread = new Thread(new ThreadStart(ReceiveData));
    //     // receiveThread.IsBackground = true;
    //     // receiveThread.Start();
    // }

    // void Update()
    // {
    //     ReceiveData();
    // }

    // /**
    //  * 接收数据
    //  */
    // private  void ReceiveData()
    // {
    //     while (true)
    //     {
    //         Debug.Log("1");
    //         try
    //         {
    //             Debug.Log("2");
    //             IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
    //             Debug.Log(anyIP);
    //             byte[] data = client.Receive(ref anyIP);

    //             string text = Encoding.UTF8.GetString(data);

    //             print(">> " + text);

    //             lastReceivedUDPPacket=text;
    //             allReceivedUDPPackets=allReceivedUDPPackets+text;
    //         }
            
    //         catch (Exception err)
    //         {
    //             Debug.Log("error!");
    //             print(err.ToString());
    //         }
    //         Debug.Log("ReceiveData ing...");
    //     }

    // }

    // public string getLatestUDPPacket()
    // {
    //     allReceivedUDPPackets="";
    //     return lastReceivedUDPPacket;
    // }
    // 
    
	IPAddress mSenderAddress;
    Thread mReceiveThread;
    IPEndPoint mSender;
    UdpClient mClient;
    public void Start() {
	    //  Setup listener.
	    this.mSenderAddress = IPAddress.Parse("127.0.0.1");
	    this.mSender = new IPEndPoint(this.mSenderAddress, 1200);

	    //  Setup background UDP listener thread.
	    this.mReceiveThread = new Thread(new ThreadStart(ReceiveData));
	    this.mReceiveThread.IsBackground = true;
	    this.mReceiveThread.Start();
	}

	//  Function to receive UDP data.
	private void ReceiveData() {
	    try {
	        //  Setup UDP client.
	        this.mClient = new UdpClient(1200);
	        this.mClient.Client.ReceiveTimeout = 250;

	        //  While thread is still alive.
	        while(Thread.CurrentThread.IsAlive) {
	            try {
	                //  Grab the data.
	                byte[] data = this.mClient.Receive(ref this.mSender);

	                //  Convert the data from bytes to doubles.
	                double[] convertedData = new double[data.Length / 8];
	                for (int i=0; i < convertedData.Length; i++)
	                {
	                	Debug.Log(convertedData[i]);
	                }

	                // for(int ii = 0; ii < convertedData.Length; ii++)
	                //     convertedData[ii] = BitConverter.ToDouble(data, 8 * ii);

	                //  DO WHATEVER WITH THE DATA

	                //  Sleep the thread.
	                Thread.Sleep(this.mClient.Client.ReceiveTimeout);
	            } 
	            catch(SocketException e) {
	                continue;
	            }
	        }
	    }
	    catch(Exception e) {
	        Debug.Log(e.ToString());
	    }
	}

	public void OnApplicationQuit()
    {
       // end of application
       if (mReceiveThread != null)
       {
          mReceiveThread.Abort(); 
       }
       print("Stop"); 
    }

}