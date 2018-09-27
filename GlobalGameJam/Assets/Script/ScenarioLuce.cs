using UnityEngine;
using System.Collections;

public class ScenarioLuce : MonoBehaviour {

	Renderer m_ScenarioLuce ;
	[SerializeField]  Renderer Fire1;
	[SerializeField]  Renderer Fire2;
	[SerializeField]  Renderer Fire3;
	[SerializeField]  Renderer Fire4;

	float perc = StaticConf.SCORE / StaticConf.MAX_SCORE;
	float AlphaValue;
	float ScalePerc = 0.0f;

	// Use this for initialization
	void Start () {
		m_ScenarioLuce = GetComponent<Renderer> ();

	}

	// Update is called once per frame
	void Update () {
		AlphaValue = m_ScenarioLuce.material.color.a;
		perc = StaticConf.SCORE / StaticConf.MAX_SCORE;
		AlphaValue = 1*perc;
		m_ScenarioLuce.material.color = new Color (m_ScenarioLuce.material.color.r, m_ScenarioLuce.material.color.g, m_ScenarioLuce.material.color.b, AlphaValue);
		Fire1.material.color = new Color (Fire1.material.color.r, Fire1.material.color.g, Fire1.material.color.b, AlphaValue);
		Fire2.material.color = new Color (Fire2.material.color.r, Fire2.material.color.g, Fire2.material.color.b, AlphaValue);
		Fire3.material.color = new Color (Fire3.material.color.r, Fire3.material.color.g, Fire3.material.color.b, AlphaValue);
		Fire4.material.color = new Color (Fire4.material.color.r, Fire4.material.color.g, Fire4.material.color.b, AlphaValue);
	}
} 
