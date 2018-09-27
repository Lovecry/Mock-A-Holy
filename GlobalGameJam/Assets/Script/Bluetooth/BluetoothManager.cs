using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class BluetoothManager : MonoBehaviour
{
	public System.Action<int> OnDevicePaired;
	public System.Action<int, int> OnDeviceEvent;

	[SerializeField] TCPConnection m_tcpConnection;
	[SerializeField] string m_msgToServer;

	System.Action UpdateInternal;
		
    void Awake()
    {
		enabled = false;
		m_tcpConnection.OnSocketSetuped += EnableScript;

		//if connection has not been made, display button to connect
		if (m_tcpConnection.socketReady == false)
		{
			m_tcpConnection.setupSocket();
			Debug.Log("Attempting to connect..");
		}

		//once connection has been made, display editable text field with a button to send that string to the server (see function below)
		if (m_tcpConnection.socketReady == true)
		{
			SendToServer(m_msgToServer);
		}

		UpdateInternal = SocketResponseAtStart;
	}

	void Update()
    {
        //keep checking the server for messages, if a message is received from server, it gets logged in the Debug console (see function below)
		UpdateInternal();
    }

	//socket reading script at start
	void SocketResponseAtStart()
	{
		int devicePaired = m_tcpConnection.readSocketAtStart();	//num of device paired
		if (devicePaired != -1)
		{
			if (OnDevicePaired != null)
				OnDevicePaired(devicePaired);

			Debug.Log("[SERVER] say: " + devicePaired);
			UpdateInternal = SocketResponse;
		}
	}

	//socket reading script
    void SocketResponse()
    {
		int playerId = -1;
		int animationId = -1;

		m_tcpConnection.readSocket(out playerId, out animationId);

		if (playerId != -1)
        {
			if (OnDeviceEvent != null)
				OnDeviceEvent(playerId, animationId);

			Debug.Log("[SERVER] PlayerID: " + playerId + " - AnimationID: " + animationId);
        }
    }

    //send message to the server
    public void SendToServer(string str)
    {
        m_tcpConnection.writeSocket(str);
        Debug.Log("[CLIENT] -> " + str);
    }

	void EnableScript()
	{
		m_tcpConnection.OnSocketSetuped -= EnableScript;
		enabled = true;
	}
}

