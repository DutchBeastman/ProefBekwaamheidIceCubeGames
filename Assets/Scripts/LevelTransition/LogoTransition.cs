// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;

public class LogoTransition : MonoBehaviour
{
	[SerializeField] private Sprite[] transitionLogos;
	private Sprite currentLogo;
	private new SpriteRenderer renderer;
	private int nextTransition;
	
	protected void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}
	
	public void StartTransition()
	{
		currentLogo = transitionLogos[nextTransition];
		FadeInLogo();
		Invoke("FadeOut", 3);
		nextTransition++;
	}
	
	private void FadeInLogo()
	{
		renderer.sprite = currentLogo;
		
	}

	private void FadeOutLogo()
	{

	}	
}
