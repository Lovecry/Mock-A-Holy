using UnityEngine;
using System.Collections;

public class InputPlayerTouch : InputBase
{
	public override void Init()
	{
		base.Init();

		m_aoTouchInfos = new TouchInfo[mk_iMaxTouchNumber];
	}

	public override void InputUpdate()
	{
		base.InputUpdate();

		for(int i = 0; i < mk_iMaxTouchNumber; ++i)
		{
			m_aoTouchInfos[i].m_bStillTouched = false;
		}

		for(int i = 0; i < Input.touchCount; ++i)
		{
			if(!m_aoTouchInfos[Input.touches[i].fingerId].m_bStarted)
			{
				StartTouch(Input.touches[i]);
			}
			else
			{
				//Update the touch position
				m_aoTouchInfos[Input.touches[i].fingerId].m_vEndPosition = Input.touches[i].position;
				m_aoTouchInfos[Input.touches[i].fingerId].m_vEndPosition.x /= Screen.width;
				m_aoTouchInfos[Input.touches[i].fingerId].m_vEndPosition.y /= Screen.height;
				m_aoTouchInfos[Input.touches[i].fingerId].m_bStillTouched = true;
			}
		}

		for(int i = 0; i < mk_iMaxTouchNumber; ++i)
		{
			if(m_aoTouchInfos[i].m_bStarted && !m_aoTouchInfos[i].m_bStillTouched)
			{
				//Touch finished..
				TouchFinished(i);
			}
		}
	}

	private void StartTouch(Touch iTouch)
	{
		if(iTouch.fingerId >= mk_iMaxTouchNumber)
		{
			Debug.LogError("Finger ID excedes max touch numbers");
			return;
		}

		int iID = iTouch.fingerId;
		m_aoTouchInfos[iID].m_bStarted = true;
		m_aoTouchInfos[iID].m_bStillTouched = true;
		m_aoTouchInfos[iID].m_fTimeElapsed = Time.time;
		m_aoTouchInfos[iID].m_vStartPosition = iTouch.position;
		m_aoTouchInfos[iID].m_vStartPosition.x /= Screen.width;
		m_aoTouchInfos[iID].m_vStartPosition.y /= Screen.height;
		m_aoTouchInfos[iID].m_vEndPosition = m_aoTouchInfos[iID].m_vStartPosition;
	}

	private void TouchFinished(int iID)
	{
		float fDistance = Vector3.Distance(m_aoTouchInfos[iID].m_vStartPosition, m_aoTouchInfos[iID].m_vEndPosition);
		if(fDistance >= mk_fMinDistanceForValidate)
		{
			m_aoTouchInfos[iID].m_fTimeElapsed = Time.time - m_aoTouchInfos[iID].m_fTimeElapsed;

			float fSpeed = fDistance / m_aoTouchInfos[iID].m_fTimeElapsed;
			if(fSpeed > mk_fMinSpeedForValidate)
			{
				Vector3 vDirection = m_aoTouchInfos[iID].m_vEndPosition - m_aoTouchInfos[iID].m_vStartPosition;
				vDirection.Normalize();
				
				float fAngle = VectorUtils.Angle(mk_vReferenceDirection, mk_vUpVector, vDirection);
				bool bValidAngle = VectorUtils.IsAngleWithinThreshold(vDirection, mk_vUpVector, mk_vReferenceDirection, mk_iAngleTreshold);
				
				Debug.Log("fDistance = " + fDistance + "fSpeed = " + fSpeed + " bValidAngle = " + bValidAngle + " fAngle = " + fAngle + " iID = " + iID);
				
				//if(bValidAngle)
				//{
				if (vDirection.y < 0) {
						//InternalShootDetected ();
					} else {
						//InternalJumpDetected ();
					}
				//}
			}
		}

		m_aoTouchInfos[iID].m_bStarted = false;
	}

	//VARS
	private struct TouchInfo
	{
		public Vector3 	m_vStartPosition;
		public Vector3 	m_vEndPosition;
		public float 	m_fTimeElapsed;
		public bool 	m_bStarted;
		public bool 	m_bStillTouched;
	}

	private TouchInfo[] 		m_aoTouchInfos;

	private const int 			mk_iMaxTouchNumber = 10;

	private const int 			mk_iAngleTreshold = 30;
	private const float 		mk_fMinSpeedForValidate = 0.5f;
	private const float 		mk_fMinDistanceForValidate = 0.15f;
	private readonly Vector3 	mk_vReferenceDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private readonly Vector3 	mk_vUpVector = new Vector3(0.0f, 0.0f, 1.0f);
}
