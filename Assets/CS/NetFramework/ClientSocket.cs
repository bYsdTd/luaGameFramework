using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using SLua;

public class ClientSocket 
{
	const int MAX_BUFFER_SIZE = 32768;

	Socket socket = null;
	Thread socketThread = null;

	string m_host = "";
	int m_port = 0;

	SocketBuffer receiveBuffer = new SocketBuffer(MAX_BUFFER_SIZE);
	SocketBuffer sendBuffer = new SocketBuffer(MAX_BUFFER_SIZE);

	public ClientSocket()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

	}

	public void Connect(string host, int port)
	{
		m_host = host;
		m_port = port;

		socketThread = new Thread(ThreadFunc);
		socketThread.Start();
	}

	void ThreadFunc()
	{
		Debug.Log("socket thread start");

		do
		{
			// connect
			try
			{
				socket.Connect(m_host, m_port);
			}
			catch
			{
				Debug.Log("socket connect failed");
				break;
			}

			// receive packet
			while(true)
			{
				bool ret = socket.Poll(10, SelectMode.SelectRead);
				if(ret)
				{
					if(socket.Available == 0)
					{
						Debug.Log("server disconnect the socket");
						break;
					}
					else
					{
						byte[] receive = new byte[socket.Available];
						int len = socket.Receive(receive);

						receiveBuffer.write(receive, len);
					}
				}

				// send packet
				if(sendBuffer.offset > 0)
				{			
					byte[] buffer = new byte[sendBuffer.offset];
					
					sendBuffer.read(ref buffer, (int)sendBuffer.offset);

					socket.Send(buffer);
				}
			}


		}while(false);

		Debug.Log("socket thread end");
	}

	public void MainThreadFunc()
	{
		// called every frame
		short packetsize = 0;
		short packetid = 0;

		if(receiveBuffer.isReadyToHandle(ref packetsize, ref packetid))
		{
			byte[] bufferHeader = new byte[SocketBuffer.PACKET_HEADER_SIZE];
			receiveBuffer.read(ref bufferHeader, SocketBuffer.PACKET_HEADER_SIZE);

			byte[] bufferPacket = new byte[packetsize];
			receiveBuffer.read(ref bufferPacket, packetsize);

			LuaFunction func = GameManager.instance().luaManager.l.luaState.getFunction("HandleNetworkData");
			
			if(func != null)
			{
				func.call(packetid, new ByteArray(bufferPacket));
			}
		}
	}

	public void SendPacket(byte[] senddata)
	{
		sendBuffer.write(senddata, senddata.Length);
	}
}
