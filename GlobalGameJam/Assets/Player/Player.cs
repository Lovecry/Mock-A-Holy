using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	
	private int id;
	private int m_animationId = -1;
	private bool m_isRealPlayer;
	Animator m_Animator;

	public int AnimationId { get { return m_animationId; } set { m_animationId = value;	} }
	public bool IsRealPlayer { get { return m_isRealPlayer; } set {m_isRealPlayer = value; } }

    float Timer;
    int TURN = 0;
	float idleTimer;
    

    void Start () {
		id = this.transform.GetSiblingIndex ();
		m_Animator = GetComponent<Animator>();

        Timer = StaticConf.DELTA_TIME;
		idleTimer = Random.Range (0.9f, 2.5f);
    }

	void Update () {
        Timer -= Time.deltaTime;
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			idleTimer -= 0.01f;
		if (idleTimer < 0) {
			//m_Animator.SetTrigger("IdleAnim");
			idleTimer = Random.Range (0.9f, 2.5f);
		}
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 2)
            {
                // Per i giocatori umani abilito l'input, per i giocatori AI eseguo una animazione a caso
                if (IsRealPlayer)
                {
                   
                }
                else if (m_animationId == -1)
                {
                    if (Random.Range(0, 1000) < 150)
                    {
                        m_animationId = Random.Range(1, 5);
                    }
                    else
                    {
                        m_animationId = StaticConf.RIGHT_MOVE;
                    }
                }

                TURN = 0;
            }
            else TURN++;
        }

        if (!m_Animator.IsInTransition(0) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(StaticConf.IdleState))
        {
            if (m_animationId != -1)
            {
				if (IsRealPlayer) {
					CheckScore ();
					switch (m_animationId) {
					case 1:
						m_Animator.SetTrigger ("Move2");//Move0");
						break;
					case 2:
						m_Animator.SetTrigger ("Move2Flipped");//Move1");
						break;
					case 3:
						m_Animator.SetTrigger ("Move2");
						break;
					case 4:
						m_Animator.SetTrigger ("Move2Flipped");
						break;
					}
					m_animationId = -1;
				} else
					StartAnimation ();
					//StartCoroutine (StartAnimationAfterDelay(StartAnimation));

            }
        }
    }

	void CheckScore()
	{
        if (m_animationId == StaticConf.RIGHT_MOVE && (TURN ==0 && Timer > StaticConf.DELTA_TIME - StaticConf.RANGE_TIME ||
            TURN == 2 && Timer < StaticConf.RANGE_TIME))
        {
            StaticConf.SCORE += StaticConf.PLAY_OK;
        }
        else
            StaticConf.SCORE += StaticConf.PLAY_KO;
    }

	IEnumerator StartAnimationAfterDelay(System.Action onEndCallback)
	{
		yield return new WaitForSeconds (Random.Range (0.0f, 0.0f));
		onEndCallback ();
	}

	void StartAnimation()
	{
		switch (m_animationId)
		{
		case 1:
			m_Animator.SetTrigger ("Move0");
			break;
		case 2:
			m_Animator.SetTrigger("Move1");
			break;
		case 3:
			m_Animator.SetTrigger("Move2");
			break;
		case 4:
			m_Animator.SetTrigger("Move2Flipped");
			break;
		}
		m_animationId = -1;
	}
}
