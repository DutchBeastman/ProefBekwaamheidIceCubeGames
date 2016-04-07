//Created By: Jeremy Bond
//Date: 29/03/2016

using UnityEngine;

public class MainMenuButtonCommands : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject settings;
	
	private bool fullAlpha;
	
	public void PlayButtonPushed ()
	{
		Overlay.FadeIn ();
		Debug.Log ("Log Start");
	}

	public void SettingsButtonPushed ()
	{
		Overlay.FadeIn ();
		Invoke ("SwitchFromMainToSettings", 1);
	}

	public void ExitButtonPushed ()
	{
		Debug.Log ("Log Quit");
		Application.Quit ();
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