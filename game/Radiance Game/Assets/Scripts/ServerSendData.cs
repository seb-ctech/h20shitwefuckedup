using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

/* 

This is the Network component that Allows other Applications to connect and request data.

*/
public class ServerSendData : MonoBehaviour
{
    #region private members 	
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;
    public String lanIp;
    public int port = 10000;
    private float testCounter;
    private WaterLevel wl;
    #endregion

    void Start()
    {
        StartListenerThread();
        wl = GameObject.Find("WaterTank").GetComponent<WaterLevel>();
        testCounter = 0;
    }


    void StartListenerThread()
    {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }
    // Update is called once per frame
    void Update()
    {
        float leakeValue = wl.GetLeakedWater();
        SendMessage(leakeValue);
    }

    private void ListenForIncomingRequests()
    {
        try
        {
            // Create listener on localhost port 8052. 			
            tcpListener = new TcpListener(IPAddress.Parse(lanIp), port);
            tcpListener.Start();
            Debug.Log("Server is listening on port " + port);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incommingData);
                            Debug.Log("client message received as: " + clientMessage);
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
    /// <summary> 	
    /// Send message to client using socket connection. 	
    /// </summary> 	
    private void SendMessage(float value)
    {
        if (connectedTcpClient == null)
        {
            return;
        }

        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = connectedTcpClient.GetStream();
            if (stream.CanWrite)
            {
                string serverMessage = value.ToString();
                // Convert string message to byte array.                 
                byte[] serverValueAsByteArray = BitConverter.GetBytes(value);
                // byte[] serverValueAsByteArray = Encoding.ASCII.GetBytes(serverMessage); // <-- For VVVV
                // Write byte array to socketConnection stream.               
                stream.Write(serverValueAsByteArray, 0, serverValueAsByteArray.Length);
                Debug.Log(serverMessage);
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}