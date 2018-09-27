using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class God : MonoBehaviour {

    [SerializeField] AudioSource m_audioSource;
    [SerializeField] AudioClip m_DrumClip;
    [SerializeField] AudioClip m_LightingClip;
    [SerializeField] AudioClip m_IronBashClip;
	[SerializeField] Slider m_ScoreSlider;

    float Timer;
    int TURN = 0;
   
    void Start () {
        m_audioSource.Play();
        Timer = StaticConf.DELTA_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticConf.SCORE < 0) //Insert here lose display
            StaticConf.SCORE = 0;
		m_ScoreSlider.value = StaticConf.SCORE;

        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            Timer = StaticConf.DELTA_TIME;
            if (TURN == 0)
            {
                if (Random.Range(0, 100) < 50)
                {
					m_audioSource.PlayOneShot(m_LightingClip);
                    StaticConf.RIGHT_MOVE = 1;
                }
                else
                {
					m_audioSource.PlayOneShot(m_IronBashClip);
                    StaticConf.RIGHT_MOVE = 2;
                }
            }

            if (TURN == 2)
            {
                TURN = 0;
            }
            else
            {
                TURN++;
            }
			m_audioSource.PlayOneShot(m_DrumClip);
        }
    }
}