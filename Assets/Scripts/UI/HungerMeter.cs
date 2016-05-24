// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HungerMeter : MonoBehaviour 
{
	[SerializeField] private Image hungerBar;
	private float hungerPercent = 100;
	private bool refill = true;

	protected void Awake () 
	{
		UpdateUIArt();
		DecreaseHunger();
	}

	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.ENDGAME, StopWorking);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.ENDGAME, StopWorking);
	}

	private void DecreaseHunger ()
	{
		hungerPercent --;
		UpdateUIArt();
		if (hungerPercent >= 0)
		{
			Invoke ("DecreaseHunger", .7f);
		}
		else
		{
			EventManager.TriggerEvent(StaticEventNames.LOSTLIFE);
			Invoke("ResetHungerBar", 1f);
		}
	}

	private void ResetHungerBar ()
	{
		if (refill)
		{
			hungerPercent = 100;
			DecreaseHunger();
		}
	}

	private void StopWorking ()
	{
		refill = false;
	}

	private void UpdateUIArt ()
	{	
		hungerBar.fillAmount = hungerPercent / 100;
	}
}
