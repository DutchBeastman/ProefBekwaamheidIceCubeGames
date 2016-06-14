// Created by: Jeremy Bond
// Date: 21/04/2016

using UnityEngine;

public class GamePartsManager : MonoBehaviour
{
	[SerializeField] private GameObject pauseScreen;
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject levelCreation;
	private bool buttonPushed = false;
	private float originTimeScale;
	/// <summary>
	/// in start listeners are added and the main menu is activated
	/// </summary>
	protected void Start()
	{
		originTimeScale = Time.timeScale;
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
		if (!buttonPushed)
		{
			buttonPushed = true;
			Invoke("EnableButtons", 2);
			BackToMainMenu();
		}
	}
	/// <summary>
	/// here the main menu activation is triggered
	/// </summary>
	private void BackToMainMenu ()
	{
		EventManager.TriggerEvent (StaticEventNames.ENABLEMAINMENU);
	}
	/// <summary>
	/// here the mainmenu is set active and levelcreation is set false
	/// </summary>
	public void ActivateMainMenu()
	{
		levelCreation.SetActive (false);
		DisablePauseScreen ();
		mainMenu.SetActive (true);
	}
	/// <summary>
	/// here the resume game is triggered
	/// </summary>
	public void ResumeButtonPushed ()
	{
		if (!buttonPushed)
		{
			buttonPushed = true;
			Invoke("EnableButtons", 2f);
			DisablePauseScreen();
			Invoke("FadeOut", 1f);
		}
	}
	/// <summary>
	/// here the pause screen is set active
	/// </summary>
	public void EnablePauseScreen ()
	{
		pauseScreen.SetActive(true);
		Time.timeScale = 0;
	}
	/// <summary>
	/// here the pause screen is disabled
	/// </summary>
	public void DisablePauseScreen ()
	{
		pauseScreen.SetActive (false);
		Time.timeScale = originTimeScale;
	}
	/// <summary>
	/// here the game creation is set active and the main menu false, while Fade out is being invoked by a short delay.
	/// </summary>
	public void ActivateGameCreation()
	{
		mainMenu.SetActive (false);
		levelCreation.SetActive (true);
		Invoke ("FadeOut", .5f);
	}
	/// <summary>
	/// the buttons will be enabled again when this function is called
	/// </summary>
	private void EnableButtons ()
	{
		buttonPushed = false;
	}
	/// <summary>
	/// here the overlay fades out
	/// </summary>
	private void FadeOut()
	{
		buttonPushed = false;
		Overlay.FadeOut ();
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
