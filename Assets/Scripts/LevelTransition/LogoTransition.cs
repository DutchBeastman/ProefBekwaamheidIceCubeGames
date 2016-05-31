// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class LogoTransition : MonoBehaviour
{
	
	[SerializeField, Range(1, 5)] private float duration = 1f;
	[SerializeField, Range(1, 5)] private float startFadingOutTime = 3;
	[SerializeField] private Sprite[] transitionLogos;
	private Sprite currentLogo;
	private new SpriteRenderer renderer;
	private int nextTransition = 1;
	/// <summary>
	/// Get the renderer of the logotransition
	/// </summary>
	protected void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}
	/// <summary>
	/// Displayes logo, and transitions with a fade out.
	/// </summary>
	public void ShowLogo()
	{
		if (renderer)
		{
			nextTransition++;
			if (nextTransition == transitionLogos.Length)
			{
				nextTransition = 0;
			}
			currentLogo = transitionLogos[nextTransition];
			renderer.sprite = currentLogo;

			StartCoroutine (FadingIn());
			Invoke("FadeOut", startFadingOutTime);
		}
	}
	/// <summary>
	/// FadeIn takes care of a smooth transition for the logo to fade in
	/// </summary>
	/// <returns>Returns a WaitForEndOfFrame</returns>
	private IEnumerator FadingIn()
	{
		for (float f = 0f; f <= 1;)
		{
			Color c = renderer.material.color;
			f += 0.01f * duration;
			c.a = f;
			renderer.color = c;
			yield return new WaitForEndOfFrame();
		}
	}
	/// <summary>
	/// Starts the fadeout Coroutine
	/// </summary>
	private void FadeOut()
	{
		StartCoroutine(FadingOut());
	}
	/// <summary>
	/// FadingOut takes care of a smooth transition for the logo to fade out
	/// </summary>
	/// <returns>Returns a WaitForEndOfFrame</returns>
	private IEnumerator FadingOut()
	{
		for (float f = 1f; f >= 0;)
		{
			Color c = renderer.material.color;
			c.a = f;
			f -= 0.01f * duration;
			renderer.color = c;
			yield return new WaitForEndOfFrame();
		}
	}
}
