// Created by: Jeremy Bond
// Date: 08/06/2016

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupScore : MonoBehaviour 
{
	[SerializeField] private Text[] scoreTexts;
	[SerializeField] private GameObject player;
	private List<RectTransform> rectTransforms;
	private int justEarnedPoints;

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

	protected void Update ()
	{
		if (rectTransforms[0].position.y <= 10)
		{
			Vector3 nextPosition = new Vector3(player.transform.position.x, rectTransforms[0].position.y + 0.1f, 0);
			for (int i = 0; i < rectTransforms.Count; i++)
			{
				rectTransforms[i].position = nextPosition;
			}
		}
	}
	/// <summary>
	/// Points that you just earned will be added to the score popup.
	/// </summary>
	/// <param name="score"></param>
	private void GainPoints (int score)
	{
		justEarnedPoints += score;
		Invoke ("UpdateUI", .15f);
	}
	/// <summary>
	/// Function for the UI update, so the text will always remain relevant to the actual score.
	/// </summary>
	private void UpdateUI ()
	{
		if (justEarnedPoints != 0)
		{
			Vector2 randomPosition = Vector2.up + new Vector2(player.transform.position.x, player.transform.position.y);
			for (int i = 0; i < rectTransforms.Count; i++)
			{
				rectTransforms[i].position = randomPosition;
			}
			foreach (Text t in scoreTexts)
			{
				t.text = justEarnedPoints.ToString ();
			}
		}
	}
}