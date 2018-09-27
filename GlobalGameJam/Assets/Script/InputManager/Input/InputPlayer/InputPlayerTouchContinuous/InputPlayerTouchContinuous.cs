using UnityEngine;
using System.Collections;

public class InputPlayerTouchContinuous: InputBase
{
	public override void Init()
	{
		base.Init();

		m_aoTouchInfos = new TouchInfo[mk_iMaxTouchNumber];

		InitTouch();

		m_aoCommandsGesture = new InputCompositeGesture[(int)GestureCommandType.COUNT];
		InputCompositeGesture[] aoCommand = GameObject.FindObjectsOfType<InputCompositeGesture>();
		for(int i = 0; i < aoCommand.Length; ++i)
		{
			m_aoCommandsGesture[(int)aoCommand[i].CommandType] = aoCommand[i];
		}
	}

	private void InitTouch()
	{
		for(int i = 0; i < mk_iMaxTouchNumber; ++i)
		{
			m_aoTouchInfos[i].m_oContInput = new ContinuousInput(mk_iTouchWindowSize);
			m_aoTouchInfos[i].m_bStarted = false;
		}
	}

	public override void InputUpdate()
	{
		base.InputUpdate();

		int iTouches = 0;

		for(int i = 0; i < Input.touchCount; ++i)
		{
			if(Input.touches[i].fingerId >= mk_iMaxTouchNumber)
			{
				Debug.LogError("Finger ID excedes max touch numbers");
				return;
			}
			else
			{
				m_aoTouchInfos[Input.touches[i].fingerId].m_bStarted = true;

				//Update the touch position
				iTouches |= 1 << i;
				int iID = Input.touches[i].fingerId;
				
				Vector3 vPos = Input.touches[i].position;
				vPos.x /= Screen.width;
				vPos.y /= Screen.height;
				
				m_aoTouchInfos[iID].m_oContInput.AddPosition(vPos, Time.deltaTime);
			}
		}

		for(int i = 0; i < mk_iMaxTouchNumber; ++i)
		{
			if(m_aoTouchInfos[i].m_bStarted)
			{
				//Check Gestures..
				CheckGesture(i);

				//Touch finished..
				if((iTouches & 1 << i) == 0)
				{
					TouchFinished(i);
				}
			}
		}
	}


	private void TouchFinished(int iID)
	{
		m_aoTouchInfos[iID].m_oContInput.Clear();

		m_aoTouchInfos[iID].m_bStarted = false;
	}

	private void CheckGesture(int iID)
	{
		float fTime = 0.0f;
		float fDistance = 0.0f;
		Vector3 vDirection = Vector3.zero;

		m_aoTouchInfos[iID].m_oContInput.GetGestureStatus(out fDistance, out fTime, out vDirection);

		float fSpeed = fDistance / fTime;
		if(fDistance >= mk_fMinDistanceForValidate && fSpeed > mk_fMinSpeedForValidate)
		{
			for(int i = 0; i < m_aoCommandsGesture.Length; ++i)
			{
				if(m_aoCommandsGesture[i] != null && m_aoCommandsGesture[i].CheckGesture(vDirection))
				{
					//Clear the gesture info..
					m_aoTouchInfos[iID].m_oContInput.Clear();

					if(m_aoCommandsGesture[i].IsGestureComplete())
					{
						m_aoCommandsGesture[i].Clear();
						switch(m_aoCommandsGesture[i].CommandType)
						{
						case GestureCommandType.SHOOT:
							//InternalShootDetected();
							break;
						case GestureCommandType.JUMP:
							//InternalJumpDetected();
							break;
						}
					}
				}
			}
		}
	}

	//VARS
	private struct TouchInfo
	{
		public ContinuousInput 	m_oContInput;
		public bool 			m_bStarted;
	}

	public enum GestureCommandType
	{
		SHOOT = 0,
		JUMP,
		COUNT
	}

	private InputCompositeGesture[] m_aoCommandsGesture;
	private TouchInfo[] 			m_aoTouchInfos;

	private const int 			mk_iMaxTouchNumber = 10;
	private const int 			mk_iTouchWindowSize = 15;

	private const int 			mk_iAngleTreshold = 30;
	private const float 		mk_fMinSpeedForValidate = 0.5f;
	private const float 		mk_fMinDistanceForValidate = 0.15f;
}
