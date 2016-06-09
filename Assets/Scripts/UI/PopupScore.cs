// Created by: Jeremy Bond
// Date: 

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupScore : MonoBehaviour 
{
	[SerializeField] private Text[] scoreTexts;
	private List<RectTransform> rectTransforms;
	private int justEarnedPoints;
	private bool updatedCalled = false;
	private bool changed = false;

	/// <summary>
	/// Sets UI for the beginning of the game.
	/// </summary>
	protected void Awake ()
	{
		rectTransforms = new List<RectTransform>();
		foreach (Text t in scoreTexts)
		{
			rectTransforms.Add(t.GetComponent<RectTransform>());
		}
		Restart ();
	}
	/// <summary>
	/// if the object gets enabled it will add all the pre-set listeners so that points can be achieved.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddScoreListener (GainPoints);
	}
	/// <summary>
	/// Removes all listeners if the objects get disabled.
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveScoreListener (GainPoints);
	}
	/// <summary>
	/// Points that you just earned will be added to the score popup.
	/// </summary>
	/// <param name="score"></param>
	private void GainPoints (int score)
	{
		if (updatedCalled)
		{
			changed = true;
		}
		updatedCalled = true;
		justEarnedPoints += score;
		Invoke ("UpdateUI", .15f);
		Invoke("Restart", 1f);
	}
	/// <summary>
	/// Empties the text areas and enables the restart mechanism.
	/// </summary>
	private void Restart ()
	{
		if (!changed)
		{
			foreach (Text scoreText in scoreTexts)
			{
				scoreText.text = "";
			}
			justEarnedPoints = 0;
		}
		changed = false;
		updatedCalled = false;
	}
	/// <summary>
	/// Function for the UI update, so the text will always remain relevant to the actual score.
	/// </summary>
	private void UpdateUI ()
	{
		if (justEarnedPoints != 0)
		{
			Vector2 randomPosition = new Vector2 (Random.Range (-200, 200), Random.Range (-200, 200));
			for (int i = 0; i < rectTransforms.Count; i++)
			{
				rectTransforms[i].anchoredPosition = randomPosition;
			}
			foreach (Text t in scoreTexts)
			{
				t.text = justEarnedPoints.ToString ();
			}
		}
	}
}