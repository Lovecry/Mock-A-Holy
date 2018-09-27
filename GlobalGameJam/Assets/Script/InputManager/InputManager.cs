using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public delegate void InputEvent();
	public event InputEvent OnSlideToRight;
	public event InputEvent OnSlideToLeft;
	public event InputEvent OnSlideToTop;
	public event InputEvent OnSlideToBottom;
	public event InputEvent OnSingleClick;
	public event InputEvent OnCLickContinuos;

	void Start()
	{
		InitInput();
	}
	
	void Update()
	{
		if(m_oInput != null)
		{
			m_oInput.InputUpdate();
		}
	}

	private void InitInput()
	{
		m_oInput = InputFactory.GetInput(m_eInputSource);
		
		if(m_oInput != null)
		{
			m_oInput.Init();

			m_oInput.Activate(
				OnSlideToRightDetc,
				OnSlideToLeftDect,
				OnSlideToTopDetc,
				OnSlideToBottomDetc,
				OnSingleClickDetc,
				OnCLickContinuosDetc
			);
		}
	}

	private void OnSlideToRightDetc()	{
		if(OnSlideToRight != null)		{
			OnSlideToRight();
		}
	}

	private void OnSlideToLeftDect()	{
		if(OnSlideToLeft != null)		{
			OnSlideToLeft();
		}
	}

	private void OnSlideToTopDetc()	{
		if(OnSlideToTop != null)		{
			OnSlideToTop();
		}
	}

	private void OnSlideToBottomDetc()	{
		if(OnSlideToBottom != null)		{
			OnSlideToBottom();
		}
	}

	private void OnSingleClickDetc()	{
		if(OnSingleClick != null)		{
			OnSingleClick();
		}
	}

	private void OnCLickContinuosDetc()	{
		if(OnCLickContinuos != null)		{
			OnCLickContinuos();
		}
	}

	public void ChangeInput(eInputSource eNewInputSource)
	{
		if(eNewInputSource != m_eInputSource)
		{
			if(m_oInput != null)
			{
				m_oInput.Deactivate();
				m_oInput = null;
			}

			m_eInputSource = eNewInputSource;

			InitInput(); 
		}
	}

	//VARS
	public enum eInputSource
	{
		PLAYER = 0,
		AI,
		REPLAY,
		NETWORK,
		COUNT
	}
	
	[SerializeField] private eInputSource m_eInputSource = eInputSource.PLAYER;

	private InputBase m_oInput;
}
