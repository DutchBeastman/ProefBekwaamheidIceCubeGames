//Created by: Fabian Verkuijlen

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
	/// <summary>
	/// Starts fadein and the transition after a while while it gets awoken.
	/// </summary>
	protected void Awake()
	{
		Invoke ("FadingIn", 3f);
		Invoke ("TransitionToMain", 4f);
	}
	/// <summary>
	/// Get the overlay to fade in
	/// </summary>
	private void FadingIn ()
	{
		Overlay.FadeIn();
		print("start fading");
	}
	/// <summary>
	/// Loads in the Game Scene, optimized with Async loading.
	/// </summary>
	private void TransitionToMain()
	{
		SceneManager.LoadSceneAsync("Game");
	}
}
