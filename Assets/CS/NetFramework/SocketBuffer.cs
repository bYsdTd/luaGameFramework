using UnityEngine;
using System.Collections;
using System;
using System.Net;

public class SocketBuffer 
{
	public const int PACKET_HEADER_SIZE = 8;
	public const int PACKET_SIZE_OFFSET = 0;
	public const int PACKET_ID_OFFSET = 2;

	// buffer array
	byte[] buffer = null;
	// current data offset
	public uint offset = 0;
	int totalSize = 0;

	object mutex = new object();

	public SocketBuffer(int size)
	{
		totalSize = size;
		buffer = new byte[size];
		offset = 0;
	}

	public int read(ref byte[] dest, int len)
	{
		lock(mutex)
		{
			if(len > dest.Length)
			{
				len = dest.Length;
			}

			if(len > offset)
			{
				Debug.Log("socket buffer read error");
				return 0;
			}

			Array.Copy(buffer, 0, dest, 0, len);
			Array.Copy(buffer, len, buffer, 0, offset - len);

			offset = offset - (uint)len;
			return len;
		}
	}

	public void write(byte[] src, int len)
	{
		lock(mutex)
		{
			if(len + offset > totalSize)
			{
				Debug.Log("socket buffer write error");

				return;
			}

			Array.Copy(src, 0, buffer, offset, len);

			offset += (uint)len;
		}
	}

	// for receive buffer
	public bool isReadyToHandle(ref short packetsize, ref short packetid)
	{
		lock(mutex)
		{
			if(offset > PACKET_HEADER_SIZE )
			{
				packetsize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, PACKET_SIZE_OFFSET));
				packetid = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(buffer, PACKET_ID_OFFSET));

				return offset >= packetsize + PACKET_HEADER_SIZE;
			}

			return false;
		}
	}
}
