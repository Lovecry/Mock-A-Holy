using UnityEngine;
using System.Collections;

public class InputCompositeGesture : MonoBehaviour 
{
	void Start()
	{
		
	}

	void Update()
	{
		if(m_bStarted)
		{
			m_fElapsedTime -= Time.deltaTime;
			if(m_fElapsedTime <= 0.0f)
			{
				Clear();
			}
		}
	}

	public bool IsGestureComplete()
	{
		return m_iMatchedCount == m_aeAtomicGesturesSequence.Length;
	}

	public bool CheckGesture(Vector3 vGestureVector)
	{
		Vector3 vDirection = Vector3.zero;
		int iTrasholdAngle = 0;

		if(!m_bStarted)
		{
			for(int i = 0; i < m_aeAtomicGesturesSequence.Length && !m_bStarted; ++i)//Find a gesture matching in the list
			{
				InputGestureData.GetGestureData((int)m_aeAtomicGesturesSequence[i], out vDirection, out iTrasholdAngle);

				m_bStarted = VectorUtils.IsAngleWithinThreshold(vGestureVector, mk_vUpVector, vDirection, iTrasholdAngle);

				m_iNextGesture = i;
			}

			if(m_bStarted)
			{
				m_iMatchedCount = 1;

				m_fElapsedTime = mk_fMaxTimeBeforeNextAtomic;

				return true;
			}

			return false;
		}
		else if(m_iValidationDirection == 0) //Not yet decided the direction..
		{
			int iNextGestureIndex = (m_iNextGesture + 1) % m_aeAtomicGesturesSequence.Length;

			InputGestureData.GetGestureData((int)m_aeAtomicGesturesSequence[iNextGestureIndex], out vDirection, out iTrasholdAngle);
			
			if(VectorUtils.IsAngleWithinThreshold(vGestureVector, mk_vUpVector, vDirection, iTrasholdAngle))
			{
				m_iNextGesture = iNextGestureIndex;
				m_iValidationDirection = 1;
				m_fElapsedTime = mk_fMaxTimeBeforeNextAtomic;

				++m_iMatchedCount;
				return true;
			}
			else
			{
				iNextGestureIndex = ((m_iNextGesture - 1 >= 0) ? (m_iNextGesture - 1) : (m_aeAtomicGesturesSequence.Length - 1));

				InputGestureData.GetGestureData((int)m_aeAtomicGesturesSequence[iNextGestureIndex], out vDirection, out iTrasholdAngle);

				if(VectorUtils.IsAngleWithinThreshold(vGestureVector, mk_vUpVector, vDirection, iTrasholdAngle))
				{
					m_iNextGesture = iNextGestureIndex;
					m_iValidationDirection = -1;
					m_fElapsedTime = mk_fMaxTimeBeforeNextAtomic;

					++m_iMatchedCount;
					return true;
				}
			}

			return false;
		}
		else 
		{
			int iNextGestureToCheck = m_iNextGesture;
			//Check the gesture in the direction i'm going..
			if(m_iValidationDirection == 1) //I'm going right
			{
				iNextGestureToCheck = (m_iNextGesture + 1) % m_aeAtomicGesturesSequence.Length;
			}
			else
			{
				iNextGestureToCheck = ((m_iNextGesture - 1 >= 0) ? (m_iNextGesture - 1) : (m_aeAtomicGesturesSequence.Length - 1));
			}

			InputGestureData.GetGestureData((int)m_aeAtomicGesturesSequence[iNextGestureToCheck], out vDirection, out iTrasholdAngle);
			
			if(VectorUtils.IsAngleWithinThreshold(vGestureVector, mk_vUpVector, vDirection, iTrasholdAngle))
			{
				m_fElapsedTime = mk_fMaxTimeBeforeNextAtomic;
				++m_iMatchedCount;

				m_iNextGesture = iNextGestureToCheck;

				return true;
			}
		}

		return false;
	}

	public void Clear()
	{
		m_iMatchedCount = 0;
		m_bStarted = false;
		m_iValidationDirection = 0;
		m_iNextGesture = 0;
	}

	public int GestureLength()
	{
		return m_aeAtomicGesturesSequence.Length;
	}

	//VARS
	[SerializeField] private InputGestureData.AtomicGestureType[] 			m_aeAtomicGesturesSequence;
	[SerializeField] private InputPlayerTouchContinuous.GestureCommandType 	m_eGestureCommand;
	public InputPlayerTouchContinuous.GestureCommandType CommandType
	{
		get { return m_eGestureCommand; }
	}

	private readonly Vector3 	mk_vUpVector = new Vector3(0.0f, 0.0f, 1.0f);

	private int m_iNextGesture = 0;
	public int NextGestureIndex
	{
		get { return m_iNextGesture; }
	}

	private int m_iValidationDirection = 0;
	private int m_iMatchedCount = 0;
	private bool m_bStarted = false;

	private float m_fElapsedTime = 0.0f;
	private const float mk_fMaxTimeBeforeNextAtomic = 1.0f;
}
