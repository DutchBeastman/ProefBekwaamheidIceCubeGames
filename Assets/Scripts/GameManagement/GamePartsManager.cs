// Created by: Jeremy Bond
// Date: 21/04/2016

using UnityEngine;

public class GamePartsManager : MonoBehaviour
{
	[SerializeField] private GameObject pauseScreen;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject levelCreation;
	/// <summary>
	/// in start listeners are added and the main menu is activated
	/// </summary>
	protected void Start()
	{
		EventManager.AddListener(StaticEventNames.ENABLEMAINMENU, ActivateMainMenu);
		EventManager.AddListener(StaticEventNames.ENABLEGAMECREATION, ActivateGameCreation);
		ActivateMainMenu();
		EventManager.TriggerAudioMusicEvent(AudioClips.gameMusicTrack);
	}
	/// <summary>
	/// here are the actions taken when the back to main menu button is pushed
	/// </summary>
	public void BackToMainMenuButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke ("BackToMainMenu", 2);
		Invoke ("FadeOut", .5f);
	}
	/// <summary>
	/// here the main menu activation is triggered
	/// </summary>
	private void BackToMainMenu ()
	{
		EventManager.TriggerEvent(StaticEventNames.ENABLEMAINMENU);
	}
	/// <summary>
	/// here the mainmenu is set active and levelcreation is set false
	/// </summary>
	public void ActivateMainMenu()
	{
		levelCreation.SetActive(false);
		DisablePauseScreen();
		mainMenu.SetActive(true);
	}
	/// <summary>
	/// here the pause screen is set active
	/// </summary>
	public void EnablePauseScreen ()
	{
		pauseScreen.SetActive(true);
	}
	/// <summary>
	/// here the pause screen is disabled
	/// </summary>
	public void DisablePauseScreen ()
	{
		pauseScreen.SetActive(false);
		Overlay.FadeOut();
	}
	/// <summary>
	/// here the game creation is set active and the main menu false, while Fade out is being invoked by a short delay.
	/// </summary>
	public void ActivateGameCreation()
	{
		mainMenu.SetActive(false);
		levelCreation.SetActive(true);
		Invoke("FadeOut", .5f);	
	}
	/// <summary>
	/// here the overlay fades out
	/// </summary>
	private void FadeOut()
	{
		Overlay.FadeOut();
	}
	/// <summary>
	/// Update function that checks when the escape button is pushed while in game 
	/// </summary>
	private void Update ()
	{
		if (levelCreation.activeSelf)
		{
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				Overlay.FadeIn();
				Invoke("EnablePauseScreen", 0.5f);
			}
		}
	}
}
