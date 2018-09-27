using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class TCPConnection : MonoBehaviour
{
	public string conName = "Localhost";	//the name of the connection, not required but better for overview if you have more than 1 connections running
	public string conHost = "127.0.0.1";	//ip/address of the server, 127.0.0.1 is for your own computer
	public int conPort = 27015;				//port for the server, make sure to unblock this in your router firewall if you want to allow external connections
	public bool socketReady = false;		//a true/false variable for connection status

    TcpClient mySocket;
    NetworkStream theStream;
    StreamWriter theWriter;
    StreamReader theReader;

	public System.Action OnSocketSetuped;

    //try to initiate connection
    public void setupSocket()
    {
        try
        {
            mySocket = new TcpClient(conHost, conPort);
            theStream = mySocket.GetStream();
            theWriter = new StreamWriter(theStream);
            theReader = new StreamReader(theStream);
            socketReady = true;

			if (OnSocketSetuped != null)
				OnSocketSetuped();
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);
        }
    }

    //send message to server
    public void writeSocket(string theLine)
    {
        if (!socketReady)
            return;
        String tmpString = theLine + "\r\n";
        theWriter.Write(tmpString);
        theWriter.Flush();
    }

    //read message from server only at start
    public int readSocketAtStart()
    {
        int result = -1;		//num of paired players

        if (theStream.DataAvailable)
        {
            Byte[] inStream = new Byte[mySocket.SendBufferSize];
            theStream.Read(inStream, 0, inStream.Length);
			result = BitConverter.ToInt32(inStream, 0);
        }
        return result;
    }

	//read message from server
	public void readSocket(out int playerId, out int animationId)
	{
		playerId = -1;
		animationId = -1;

		if (theStream.DataAvailable)
		{
			Byte[] inStream = new Byte[StaticConf.STREAM_BYTE];
			theStream.Read(inStream, 0, inStream.Length);
			playerId = BitConverter.ToInt32(inStream, 0);
			theStream.Read(inStream, 0, inStream.Length);
			animationId = BitConverter.ToInt32(inStream, 0);
		}
	}

    //disconnect from the socket
    public void closeSocket()
    {
        if (!socketReady)
            return;
        theWriter.Close();
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }

    //keep connection alive, reconnect if connection lost
    public void maintainConnection()
    {
        if (!theStream.CanRead)
        {
            setupSocket();
        }
    }
}
