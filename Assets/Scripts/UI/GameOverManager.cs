// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour 
{

	protected void Start () 
	{
		
	}

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

	}
}
