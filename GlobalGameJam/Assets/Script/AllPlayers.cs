using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllPlayers : MonoBehaviour 
{
	[SerializeField] BluetoothManager m_bluetoothManager;
	List<Player> m_PlayerList;
	List<Player> m_RealPlayerList;

	void Start () 
	{
		m_bluetoothManager.OnDevicePaired += HandleOnDevicePaired;
		m_bluetoothManager.OnDeviceEvent += HandleOnDeviceEvent;

		m_PlayerList = new List<Player>();
		m_RealPlayerList = new List<Player>();

		GameObject[] go = GameObject.FindGameObjectsWithTag(StaticConf.AdeptiTag);
		foreach(GameObject item in go)
			m_PlayerList.Add(item.GetComponent<Player>());

		enabled = false;
	}

	void Update () 
	{
	
	}

	public Player GetPlayer(int id)
	{
		return null;
	}

	void HandleOnDevicePaired(int numOfPlayers)
	{
		int randomId;

        StaticConf.PLAY_KO = StaticConf.SANT_KO / numOfPlayers;
        StaticConf.PLAY_OK = StaticConf.SANT_OK / numOfPlayers;

		for (int i=0; i<numOfPlayers; ++i)
		{
			randomId = Random.Range(0, numOfPlayers-1);	
			m_PlayerList[randomId].IsRealPlayer = true;
			m_RealPlayerList.Add(m_PlayerList[randomId]);
		}

		enabled = true;
	}

	void HandleOnDeviceEvent(int playerId, int animationId)
	{
		m_RealPlayerList[playerId].AnimationId = animationId;
	}

	void OnDestroy()
	{
		m_bluetoothManager.OnDevicePaired -= HandleOnDevicePaired;
		m_bluetoothManager.OnDeviceEvent -= HandleOnDeviceEvent;
	}
}
