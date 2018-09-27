using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Santone : MonoBehaviour {

	[SerializeField] AudioSource m_audioSource;
	[SerializeField] Animator m_Animator;
    [SerializeField] AudioClip m_VikingHappyClip;
	[SerializeField] AudioClip m_VikingSadClip;

	float Timer;
    int TURN = 0;
	float idleTimer;
    private int m_animationId = -1;

    void Start () {
		Timer = StaticConf.DELTA_TIME;
		idleTimer = Random.Range (0.5f, 2.5f);
    }

	void Update () 
	{
		//Temp test
		if (Input.GetKeyDown (KeyCode.Space))
			StaticConf.SCORE += 30.0f;
		if (Input.GetKeyDown (KeyCode.LeftAlt))
			StaticConf.SCORE -= 30.0f;
		//This code play an idle after idle timer elapse
		if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(StaticConf.IdleState))
			idleTimer -= 0.01f;
		if (idleTimer < 0) {
			m_Animator.SetTrigger("IdleAnim");
			idleTimer = Random.Range (0.5f, 2.5f);
		}

		Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 2)
            {
                TURN = 0;
            }
            else
            {
                TURN++;
            }
        }

        if (!m_Animator.IsInTransition(0) && m_Animator.GetCurrentAnimatorStateInfo(0).IsName(StaticConf.IdleState)) //Errato
        {
            float firstAxisValue = Input.GetAxis("FirstVertical");
            float secondAxisValue = Input.GetAxis("SecondVertical");
            if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER)
            {
                m_animationId = 1;
				m_audioSource.PlayOneShot(m_VikingHappyClip);
            }
            else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER)
            {
                m_animationId = 2;
				m_audioSource.PlayOneShot(m_VikingSadClip);
            }

            firstAxisValue = Input.GetAxis("FirstHorizontal");
            secondAxisValue = Input.GetAxis("SecondHorizontal");
            if (firstAxisValue > StaticConf.ANALOGIC_TRIGGER && secondAxisValue > StaticConf.ANALOGIC_TRIGGER)
            {
                m_animationId = 3;
            }
            else if (firstAxisValue < -StaticConf.ANALOGIC_TRIGGER && secondAxisValue < -StaticConf.ANALOGIC_TRIGGER)
            {
                m_animationId = 4;
            }

            if (m_animationId != -1)
            {
                CheckScore();
                switch (m_animationId)
                {
                    case 1:
                        m_Animator.SetTrigger("Move0");
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
	}

	void CheckScore ()
    {
        // Verifico se sono nella giusta condizione del timer
        if (m_animationId == StaticConf.RIGHT_MOVE && (TURN == 2 && Timer > StaticConf.DELTA_TIME - StaticConf.RANGE_TIME ||
            TURN == 1 && Timer < StaticConf.RANGE_TIME))
        {
            StaticConf.SCORE += StaticConf.SANT_OK;
        }
        else
            StaticConf.SCORE += StaticConf.SANT_KO;
    }
}
