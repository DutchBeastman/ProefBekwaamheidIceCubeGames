//Created By: Jeremy Bond
//Date: 29/03/2016

using UnityEngine;
using UnityEngine.Events;

public class MainMenuButtonCommands : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject howToPlay;
	[SerializeField] private GameObject settings;
	[SerializeField] private GameObject gameCreation;

	private bool fullAlpha;
	/// <summary>
	/// Regulates the fade in and starts the game.
	/// </summary>
	public void PlayButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke("CreateLevel", 2);
		EventManager.TriggerAudioSFXEvent(AudioClips.buttonSound);
	}
	/// <summary>
	/// Regulates the call for the event to create the level.
	/// </summary>
	private void CreateLevel()
	{
		EventManager.TriggerEvent(StaticEventNames.ENABLEGAMECREATION);
	}
	/// <summary>
	/// Regulates the fade in and opens the how to play screen.
	/// </summary>
	private void HowToButtonPushed ()
	{
		Overlay.FadeIn();
		Invoke("SwitchFromMainToHowToPlay", 1);
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}
	/// <summary>
	/// Regulates the fade in and opens settings screen.
	/// </summary>
	public void SettingsButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke ("SwitchFromMainToSettings", 1);
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}
	/// <summary>
	/// When called quits the application.
	/// </summary>
	public void ExitButtonPushed ()
	{
		Application.Quit ();
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}
	/// <summary>
	/// Regulates the back-button function. starts fadeIn and switchesback to main menu
	/// </summary>
	public void BackToMainMenuButtonPushed()
	{
		Overlay.FadeIn ();
		Invoke ("SwitchFromSettingsToMain", 1);
		EventManager.TriggerAudioSFXEvent (AudioClips.buttonSound);
	}
	/// <summary>
	/// Enables settings, disables main menu
	/// </summary>
	private void SwitchFromMainToSettings ()
	{
		DisableMainMenu ();
		EnableSettings ();
		Overlay.FadeOut();
	}
	/// <summary>
	/// Disables settings and enables main menu
	/// </summary>
	private void SwitchFromSettingsToMain ()
	{
		DisableSettings ();
		EnableMainMenu ();
		Overlay.FadeOut ();
	}
	/// <summary>
	/// Enables settings, disables main menu
	/// </summary>
	private void SwitchFromMainToHowToPlay ()
	{
		DisableMainMenu ();
		EnableHowToPlay ();
		Overlay.FadeOut ();
	}
	/// <summary>
	/// Disables settings and enables main menu
	/// </summary>
	private void SwitchFromHowToPlayToMain ()
	{
		DisableHowToPlay ();
		EnableMainMenu ();
		Overlay.FadeOut ();
	}
	/// <summary>
	/// sets main menu active
	/// </summary>
	private void EnableMainMenu ()
	{
		mainMenu.gameObject.SetActive (true);
	}
	/// <summary>
	/// disables main menu
	/// </summary>
	private void DisableMainMenu ()
	{
		mainMenu.gameObject.SetActive (false);
	}
	/// <summary>
	/// sets how to play active
	/// </summary>
	private void EnableHowToPlay ()
	{
		howToPlay.gameObject.SetActive (true);
	}
	/// <summary>
	/// disables how to play
	/// </summary>
	private void DisableHowToPlay ()
	{
		howToPlay.gameObject.SetActive (false);
	}
	/// <summary>
	/// enables settings
	/// </summary>
	private void EnableSettings ()
	{
		settings.gameObject.SetActive (true);
	}
	/// <summary>
	/// disables settings
	/// </summary>
	private void DisableSettings ()
	{
		settings.gameObject.SetActive (false);
	}
}