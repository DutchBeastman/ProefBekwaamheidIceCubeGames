//Created by: Fabian Verkuijlen

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
	protected void Awake()
	{
		Invoke ("FadingIn", 3f);
		Invoke ("TransitionToMain", 4f);
	}

	private void FadingIn ()
	{
		Overlay.FadeIn();
		print("start fading");
	}

	private void TransitionToMain()
	{
		SceneManager.LoadSceneAsync("Game");
	}
}
