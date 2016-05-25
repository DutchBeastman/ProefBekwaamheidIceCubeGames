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
		Invoke("DecreaseHunger", 0.7f);
	}

	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.ENDGAME, StopWorking);
		EventManager.AddListener(StaticEventNames.GOTPICKUP, GotPickUp);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.ENDGAME, StopWorking);
		EventManager.RemoveListener(StaticEventNames.GOTPICKUP, GotPickUp);
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

	private void GotPickUp ()
	{
		hungerPercent += 20;
		if (hungerPercent > 100)
		{
			hungerPercent = 100;
		}
	}

	private void UpdateUIArt ()
	{	
		hungerBar.fillAmount = hungerPercent / 100;
	}
}
