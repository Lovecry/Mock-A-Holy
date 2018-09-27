using UnityEngine;
using System.Collections;
using System;

public class InputBase
{
	public virtual void Init()
	{
	}

	public virtual void InputUpdate()
	{
	}

	protected void Int_SlideToRightDect()
	{
		if(m_onSlideToRightClbk != null)
		{
			m_onSlideToRightClbk();
		}
	}

	protected void Int_SlideToLeftDect()
	{
		if(m_onSlideToLeftClbk != null)
		{
			m_onSlideToLeftClbk();
		}
	}
	protected void Int_SlideToTopDect()
	{
		if(m_onSlideToTopClbk != null)
		{
			m_onSlideToTopClbk();
		}
	}
	protected void Int_SlideToBottomDect()
	{
		if(m_onSlideToBottomClbk != null)
		{
			m_onSlideToBottomClbk();
		}
	}
	protected void Int_ClickDect()
	{
		if(m_onClickClbk != null)
		{
			m_onClickClbk();
		}
	}
	protected void Int_ClickContinousDect()
	{
		if(m_onClickContinousClbk != null)
		{
			m_onClickContinousClbk();
		}
	}
	public void Activate(Action slideRight, Action slideLeft, Action slideTop, Action slideDown, Action click, Action clickcontinous)
	{
		m_onSlideToRightClbk = slideRight;
		m_onSlideToLeftClbk = slideLeft;
		m_onSlideToTopClbk = slideTop;
		m_onSlideToBottomClbk = slideDown;
		m_onClickClbk = click;
		m_onClickContinousClbk = clickcontinous;
		//m_bActive = true;
	}
	
	public void Deactivate()
	{
		m_onSlideToRightClbk = null;
		m_onSlideToLeftClbk = null;
		m_onSlideToTopClbk = null;
		m_onSlideToBottomClbk = null;
		m_onClickClbk = null;
		m_onClickContinousClbk = null;
		//m_bActive = false;
	}

	//VARS
	//private bool m_bActive = false;

	protected event Action m_onSlideToRightClbk = null;
	protected event Action m_onSlideToLeftClbk = null;
	protected event Action m_onSlideToTopClbk = null;
	protected event Action m_onSlideToBottomClbk = null;
	protected event Action m_onClickClbk = null;
	protected event Action m_onClickContinousClbk = null;

}
