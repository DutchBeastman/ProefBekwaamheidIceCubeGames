// Created by: Jeremy Bond
// Date: 21/04/2016

using UnityEngine;

public class GamePartsManager : MonoBehaviour
{
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
	/// here the mainmenu is set active and levelcreation is set false
	/// </summary>
	public void ActivateMainMenu()
	{
		levelCreation.SetActive(false);
		mainMenu.SetActive(true);
	}
	/// <summary>
	/// here the game creation is set active and the main menu false, while Fade out is being invoked by a short delay.
	/// </summary>
	public void ActivateGameCreation()
	{
		mainMenu.SetActive(false);
		levelCreation.SetActive(true);
		Invoke("FadeOut", .5f);	
		EventManager.TriggerAudioMusicEvent(AudioClips.gameMusicTrack);
	}
	/// <summary>
	/// here the overlay fades out
	/// </summary>
	private void FadeOut()
	{
		Overlay.FadeOut();
	}
}
