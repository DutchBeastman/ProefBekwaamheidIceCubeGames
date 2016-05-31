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
		EventManager.AddListener ("GetPoints10", Get10Points);
		EventManager.AddListener ("GetPoints20", Get20Points);
		EventManager.AddListener ("GetPoints30", Get30Points);
		EventManager.AddListener ("GetPoints50", Get50Points);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
	}
	/// <summary>
	/// Removes all listeners if the objects get disabled
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener ("GetPoints10", Get10Points);
		EventManager.RemoveListener ("GetPoints20", Get20Points);
		EventManager.RemoveListener ("GetPoints30", Get30Points);
		EventManager.RemoveListener ("GetPoints50", Get50Points);
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
	/// adds 10 points to score and updates the UI
	/// </summary>
	private void Get10Points ()
	{
		score += 10;
		UpdateUI ();
	}

	/// <summary>
	/// adds 20 points to score and updates the UI
	/// </summary>
	private void Get20Points ()
	{
		score += 20;
		UpdateUI ();
	}

	/// <summary>
	/// adds 30 points to score and updates the UI
	/// </summary>
	private void Get30Points ()
	{
		score += 30;
		UpdateUI ();
	}

	/// <summary>
	/// adds 50 points to score and updates the UI
	/// </summary>
	private void Get50Points ()
	{
		score += 50;
		UpdateUI();
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
