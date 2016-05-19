// Created by: Jeremy Bond
// Date: 17/05/2016

using UnityEngine;
using UnityEngine.UI;

public class StageArtManager : MonoBehaviour 
{
	[SerializeField] private Sprite[] stagesImages;
	[SerializeField] private new SpriteRenderer renderer;
	[SerializeField] private Image image;
	private int currentState;

	protected void OnEnable ()
	{
		EventManager.AddListener("NextStage", NextStage);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener("NextStage", NextStage);
	}

	protected void Start ()
	{
		SetNewSprite();
	}

	private void NextStage ()
	{
		currentState++;
		SetNewSprite();
	}

	private void SetNewSprite ()
	{
		if (renderer)
		{
			renderer.sprite = stagesImages[currentState];
		}
		if (image)
		{
			image.sprite = stagesImages[currentState];
		}
	}
}
