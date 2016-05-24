// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour 
{
	[SerializeField] private GameObject gameOverObject;
	[SerializeField] private Text[] scoreText;
	
	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.ENDGAME, GameOverTrigger);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.ENDGAME, GameOverTrigger);
	}

	private void GameOverTrigger ()
	{
		gameOverObject.SetActive(true);
		foreach (Text t in scoreText)
		{
			t.text = Score.score.ToString();
		}
	}

	public void ResetGame ()
	{
		gameOverObject.SetActive(false);
		EventManager.TriggerEvent(StaticEventNames.RESTART);
	}
}
