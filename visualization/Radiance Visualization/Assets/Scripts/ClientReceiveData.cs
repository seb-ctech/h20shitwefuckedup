
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

/* 

This is the Network component that Allows connects to the Server and requests data from the Game State.

*/
public class ClientReceiveData : MonoBehaviour {  	
	public String serverIp = "172.23.25.147";
	public int serverPort = 10005;
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 
	private stateControlScript state;	
	#endregion  	
	// Use this for initialization 	
	void Start () {
		ConnectToTcpServer();
		state = gameObject.GetComponent<stateControlScript>();     
	}  	
	private void ConnectToTcpServer () { 		
    // Create and start a Thread to Listen for Data
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient(serverIp, serverPort);
			Byte[] bytes = new Byte[4];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incomingData = new byte[length]; 						
						Array.Copy(bytes, 0, incomingData, 0, length); 						
						// Convert byte array to string message. 				
						float serverData = BitConverter.ToSingle(incomingData, 0); 						
						Debug.Log("server message received as: " + serverData);
						state.SetVizState(serverData);
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
}