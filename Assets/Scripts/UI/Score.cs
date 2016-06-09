// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	[HideInInspector] public static int score;
	[SerializeField] private Text[] scoreTexts;
	/// <summary>
	/// Sets UI for the beginning of the game.
	/// </summary>
	protected void Awake ()
	{
		UpdateUI();
	}
	/// <summary>
	/// if the object gets enabled it will add all the pre-set listeners so that points can be achieved.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddScoreListener (GainPoints);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
	}
	/// <summary>
	/// Removes all listeners if the objects get disabled
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveScoreListener (GainPoints);
		EventManager.RemoveListener (StaticEventNames.RESTART, Restart);
	}
	/// <summary>
	/// On restart all UI and score will be set to 0
	/// </summary>
	private void Restart ()
	{
		score = 0;
		UpdateUI();
	}
	/// <summary>
	/// Add the parameter "score" to the total score
	/// </summary>
	/// <param name="score"></param>
	private void GainPoints (int addedScore)
	{
		score += addedScore;
		UpdateUI ();
	}
	/// <summary>
	/// Function for the UI update, so the text will always remain relevant to the actual score.
	/// </summary>
	private void UpdateUI ()
	{
		foreach (Text scoreText in scoreTexts)
		{
			scoreText.text = score.ToString ();
		}
	}
}
