using UnityEngine;
using System.Collections;
using System;

public class SocketBuffer 
{
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
}
