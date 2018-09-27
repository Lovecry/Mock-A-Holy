using UnityEngine;
using System.Collections;

public class InputMouse : InputBase
{
	public override void InputUpdate()
	{
		base.InputUpdate();

		if(Input.GetMouseButtonDown(0))
		{
			m_vStartPosition = Input.mousePosition;
			m_vStartPosition.x /= Screen.width;
			m_vStartPosition.y /= Screen.height;

			m_fInitialTime = Time.time;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			m_fTimeElapsed = Time.time - m_fInitialTime;

			m_vEndPosition = Input.mousePosition;
			m_vEndPosition.x /= Screen.width;
			m_vEndPosition.y /= Screen.height;

			CheckGesture();
		}
	}

	private void CheckGesture()
	{
		Vector3 vDirection = m_vEndPosition - m_vStartPosition;
		float fDistance = vDirection.magnitude;

		if(fDistance >= mk_fMinDistanceForValidate)
		{
			float fSpeed = fDistance / m_fTimeElapsed;
			if(fSpeed > mk_fMinSpeedForValidate)
			{
				vDirection.Normalize();

				bool bValidAngle = VectorUtils.IsAngleWithinThreshold(mk_vReferenceDirection, mk_vUpVector, vDirection, mk_iAngleTreshold);

				float fAngle = VectorUtils.Angle(mk_vReferenceDirection, mk_vUpVector, vDirection);
				Debug.Log("fDistance = " + fDistance + "fSpeed = " + fSpeed + " bValidAngle = " + bValidAngle + " fAngle = " + fAngle);

				//if(bValidAngle)
				//{
				float VerticOffset = m_vEndPosition.y > m_vStartPosition.y ? m_vEndPosition.y - m_vStartPosition.y : m_vStartPosition.y - m_vEndPosition.y;
				float HorizOffset = m_vEndPosition.x > m_vStartPosition.x ? m_vEndPosition.x - m_vStartPosition.x : m_vStartPosition.x - m_vEndPosition.x;

				if (VerticOffset > HorizOffset) {
					if (m_vEndPosition.y < m_vStartPosition.y) {
						Int_SlideToBottomDect ();
					} else if (m_vEndPosition.y > m_vStartPosition.y) {
						Int_SlideToTopDect ();
					}
				} else {
					if (m_vEndPosition.x < m_vStartPosition.x) {
						Int_SlideToLeftDect ();
					} else if (m_vEndPosition.x > m_vStartPosition.x) {
						Int_SlideToRightDect ();
					}
				}
				//}
			}
		}
	}

	//VARS
	private Vector3 m_vStartPosition;
	private Vector3 m_vEndPosition;
	private float 	m_fTimeElapsed = 0.0f;
	private float 	m_fInitialTime = 0.0f;

	private const int 			mk_iAngleTreshold = 30;
	private const float 		mk_fMinSpeedForValidate = 0.5f;
	private const float 		mk_fMinDistanceForValidate = 0.15f;
	private readonly Vector3 	mk_vReferenceDirection = new Vector3(0.0f, 1.0f, 0.0f);
	private readonly Vector3 	mk_vUpVector = new Vector3(0.0f, 0.0f, 1.0f);
}
