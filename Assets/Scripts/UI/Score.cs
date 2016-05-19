// Created by: Jeremy Bond
// Date: 

using UnityEngine;

public class Score : MonoBehaviour
{
	[HideInInspector] public int score;
	protected void OnEnable ()
	{
		EventManager.AddListener ("GetPoints10", Get10Points);
		EventManager.AddListener ("GetPoints20", Get20Points);
		EventManager.AddListener ("GetPoints30", Get30Points);
		EventManager.AddListener ("GetPoints50", Get50Points);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener ("GetPoints10", Get10Points);
		EventManager.RemoveListener ("GetPoints20", Get20Points);
		EventManager.RemoveListener ("GetPoints30", Get30Points);
		EventManager.RemoveListener ("GetPoints50", Get50Points);
	}

	private void Get10Points ()
	{
		score += 10;
		UpdateUI ();
	}

	private void Get20Points ()
	{
		score += 20;
		UpdateUI ();
	}

	private void Get30Points ()
	{
		score += 30;
		UpdateUI ();
	}

	private void Get50Points ()
	{
		score += 50;
		UpdateUI();
	}

	private void UpdateUI ()
	{
		Debug.Log(score);
	}
}
