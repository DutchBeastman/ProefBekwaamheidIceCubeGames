// Created by: Jeremy Bond
// Date: 24/05/2016

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HungerMeter : MonoBehaviour 
{
	[SerializeField] private Image hungerBar;
	[SerializeField] private float percentDecreaseTime = 0.4f;
	private float hungerPercent = 100;
	private bool refill = true;
	private bool death = false;
	/// <summary>
	/// On Awake it updates all the art of the Hungermeter and invokes it constant decrease
	/// </summary>
	protected void Awake () 
	{
		UpdateUIArt();
		Invoke("DecreaseHunger", 0.7f);
	}
	/// <summary>
	/// OnEnable it adds all the listeners for the hungerMeter to listen to.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.ENDGAME, StopWorking);
		EventManager.AddListener (StaticEventNames.GOTPICKUP, GotPickUp);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
		EventManager.AddListener (StaticEventNames.ENDGAME, StopUIUpdate);
		EventManager.AddListener (StaticEventNames.LOSTLIFE, Restart);
	}
	/// <summary>
	/// OnDisable removes all the listeners
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.ENDGAME, StopWorking);
		EventManager.RemoveListener (StaticEventNames.GOTPICKUP, GotPickUp);
		EventManager.RemoveListener (StaticEventNames.RESTART, Restart);
		EventManager.RemoveListener (StaticEventNames.ENDGAME, StopUIUpdate);
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, Restart);
	}
	/// <summary>
	/// Resets hunger meter on death
	/// </summary>
	private void Restart ()
	{
		hungerPercent = 100;
		death = false;
		UpdateUIArt();
	}
	/// <summary>
	/// Constantly decreases hunger, unless its dead or already at zero.
	/// </summary>
	private void DecreaseHunger ()
	{
		hungerPercent --;
		UpdateUIArt();
		if (hungerPercent >= 0  && !death)
		{
			Invoke ("DecreaseHunger", percentDecreaseTime);
		}
		else
		{
			EventManager.TriggerEvent(StaticEventNames.LOSTLIFE);
			Invoke("ResetHungerBar", 1f);
		}
	}
	/// <summary>
	/// Resets hungerbar  
	/// </summary>
	private void ResetHungerBar ()
	{
		if (refill)
		{
			hungerPercent = 100;
			DecreaseHunger();
		}
	}
	/// <summary>
	/// Stops the refill
	/// </summary>
	private void StopWorking ()
	{
		refill = false;
	}
	/// <summary>
	/// Stops UI update. 
	/// </summary>
	private void StopUIUpdate ()
	{
		death = true;
	}
	/// <summary>
	/// regulates that if you get a pick-up you gain more on your hungerbar, if the hunger is already at 100 it stays at 100. 
	/// </summary>
	private void GotPickUp ()
	{
		hungerPercent += 20;
		if (hungerPercent > 100)
		{
			hungerPercent = 100;
		}
	}
	/// <summary>
	/// Updates the UI art with correct amount of fill amount.
	/// </summary>
	private void UpdateUIArt ()
	{	
		hungerBar.fillAmount = hungerPercent / 100;
	}
}
