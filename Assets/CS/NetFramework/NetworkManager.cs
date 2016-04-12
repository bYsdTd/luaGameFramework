﻿using UnityEngine;
using System.Collections;
using System.Text;

public class NetworkManager 
{	
	ClientSocket clientSocket = null;

	public void Init()
	{
		clientSocket = new ClientSocket();
		clientSocket.Connect("127.0.0.1", 8086);

		string sendtext = "Hello World\n";
		clientSocket.SendPacket(Encoding.UTF8.GetBytes(sendtext));
	}

	public void Update(float dt) 
	{
		// check network mannager

		clientSocket.MainThreadFunc();
	}

	public void send(byte[] data)
	{
		clientSocket.SendPacket(data);
	}
}