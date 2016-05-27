﻿//Created By: Jeremy Bond
//Date: 29/03/2016

using UnityEngine;
using UnityEngine.Events;

public class MainMenuButtonCommands : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject gameCreation;
	[SerializeField] private AudioClips[] buttonPushSounds;

	private bool fullAlpha;
	
	public void PlayButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke("CreateLevel", 2);
		EventManager.TriggerAudioSFXEvent(AudioClips.buttonSound);
	}

	private void CreateLevel()
	{
		EventManager.TriggerEvent(StaticEventNames.ENABLEGAMECREATION);
	}

	public void SettingsButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke ("SwitchFromMainToSettings", 1);
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}

	public void ExitButtonPushed ()
	{
		Application.Quit ();
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}

	public void BackToMainMenuButtonPushed()
	{
		Overlay.FadeIn ();
		Invoke ("SwitchFromSettingsToMain", 1);
	}

	private void SwitchFromMainToSettings ()
	{
		DisableMainMenu ();
		EnableSettings ();
		Overlay.FadeOut();
	}

	private void SwitchFromSettingsToMain ()
	{
		DisableSettings ();
		EnableMainMenu ();
		Overlay.FadeOut ();
	}

	private void EnableMainMenu ()
	{
		mainMenu.gameObject.SetActive (true);
	}

	private void DisableMainMenu ()
	{
		mainMenu.gameObject.SetActive (false);
	}

	private void EnableSettings ()
	{
		settings.gameObject.SetActive (true);
	}

	private void DisableSettings ()
	{
		settings.gameObject.SetActive (false);
	}
}