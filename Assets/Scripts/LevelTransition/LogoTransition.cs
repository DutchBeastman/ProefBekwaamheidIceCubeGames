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

	protected void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}

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

	private void FadeOut()
	{
		StartCoroutine(FadingOut());
	}

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
