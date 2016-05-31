// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour 
{
	[SerializeField] private GameObject gameOverObject;
	[SerializeField] private Text[] scoreText;
	/// <summary>
	/// In OnEnable the listener is added.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.ENDGAME, GameOverTrigger);
	}
	/// <summary>
	/// In OnDisable the listener is removed.
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.ENDGAME, GameOverTrigger);
	}
	/// <summary>
	/// Gameovertrigger executes the game over screen and puts your score on to it.
	/// </summary>
	private void GameOverTrigger ()
	{
		gameOverObject.SetActive(true);
		foreach (Text t in scoreText)
		{
			t.text = Score.score.ToString();
		}
	}
	/// <summary>
	/// ResetGame resets the game and disables the gameoverscreen.
	/// </summary>
	public void ResetGame ()
	{
		gameOverObject.SetActive(false);
		EventManager.TriggerEvent(StaticEventNames.RESTART);
	}
}
