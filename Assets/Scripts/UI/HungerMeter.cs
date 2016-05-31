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
	private bool death = false;

	protected void Awake () 
	{
		UpdateUIArt();
		Invoke("DecreaseHunger", 0.7f);
	}

	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.ENDGAME, StopWorking);
		EventManager.AddListener (StaticEventNames.GOTPICKUP, GotPickUp);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
		EventManager.AddListener (StaticEventNames.ENDGAME, StopUIUpdate);
		EventManager.AddListener (StaticEventNames.LOSTLIFE, Restart);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.ENDGAME, StopWorking);
		EventManager.RemoveListener (StaticEventNames.GOTPICKUP, GotPickUp);
		EventManager.RemoveListener (StaticEventNames.RESTART, Restart);
		EventManager.RemoveListener (StaticEventNames.ENDGAME, StopUIUpdate);
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, Restart);
	}

	private void Restart ()
	{
		hungerPercent = 100;
		death = false;
		UpdateUIArt();
	}

	private void DecreaseHunger ()
	{
		hungerPercent --;
		UpdateUIArt();
		if (hungerPercent >= 0  && !death)
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

	private void StopUIUpdate ()
	{
		death = true;
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
