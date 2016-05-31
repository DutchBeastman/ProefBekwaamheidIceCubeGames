// Created by: Jeremy Bond
// Date: 17/05/2016

using UnityEngine;
using UnityEngine.UI;

public class StageArtManager : MonoBehaviour 
{
	[SerializeField] private new SpriteRenderer renderer;
	[SerializeField] private Image image;
	[SerializeField] private Sprite[] stagesImages;
	private int currentState;
	/// <summary>
	/// Adds listener on enable.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.NEXTSTAGE, NextStage);
	}
	/// <summary>
	/// Removes listener on disable
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.NEXTSTAGE, NextStage);
	}
	/// <summary>
	/// On start it sets a new sprite
	/// </summary>
	protected void Start ()
	{
		SetNewSprite();
	}
	/// <summary>
	/// Sets art to the new stage art when the stage is changed
	/// </summary>
	private void NextStage ()
	{
		currentState++;
		if (currentState == stagesImages.Length)
		{
			currentState = 0;
		}
		SetNewSprite();
	}
	/// <summary>
	/// Sets the new sprite in the renderer for the right stage.
	/// </summary>
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
