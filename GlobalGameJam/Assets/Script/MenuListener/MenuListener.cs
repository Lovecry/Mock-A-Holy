using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuListener : MonoBehaviour {

	[SerializeField] Button m_closeButton;
	[SerializeField] Button m_startButton;
	[SerializeField] Button m_creditsButton;
	[SerializeField] GameObject m_creditsImage;
	[SerializeField] Button m_creditsImageButton;


	void Start () 
	{
		m_closeButton.onClick.AddListener(OnCloseGame);
		m_startButton.onClick.AddListener(OnStartGame);
		m_creditsButton.onClick.AddListener(OnCreditsButton);
		m_creditsImageButton.onClick.AddListener(OnCloseCredits);
	}

	void OnDestroy () 
	{
		m_closeButton.onClick.RemoveAllListeners();
		m_startButton.onClick.RemoveAllListeners();
		m_creditsButton.onClick.RemoveAllListeners();
		m_creditsImageButton.onClick.RemoveAllListeners();
	}

	void OnStartGame()
	{
		SceneManager.LoadScene("1");
	}

	void OnCreditsButton()
	{
		m_creditsImage.SetActive(true);
	}

	void OnCloseGame()
	{
		Application.Quit();
	}

	void OnCloseCredits()
	{
		m_creditsImage.SetActive(false);
	}
}
