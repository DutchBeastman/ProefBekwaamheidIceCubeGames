﻿// Created by: Jeremy Bond
// Date: 20/05/2016

using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
	[SerializeField] private Image livesImage;
	[SerializeField] private Sprite[] lives;
	private int livesCounter = 3;


	protected void Awake ()
	{
		livesImage.sprite = lives[livesCounter];
	}

	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.LOSTLIFE, LossOfLife);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, LossOfLife);
	}

	private void LossOfLife ()
	{
		livesCounter--;
		if (livesCounter >= 0)
		{
			livesImage.sprite = lives[livesCounter];
		}

		if (livesCounter == 0)
		{
			EventManager.TriggerEvent (StaticEventNames.ENDGAME);
		}
	}
}
