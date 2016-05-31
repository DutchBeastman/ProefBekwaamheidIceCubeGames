// Created by: Jeremy Bond
// Date: 20/05/2016

using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
	[SerializeField] private Image livesImage;
	[SerializeField] private Sprite[] lives;
	private int livesCounter = 3;

	/// <summary>
	/// In Awake the correct amount of lives are being set
	/// </summary>
	protected void Awake ()
	{
		livesImage.sprite = lives[livesCounter];
	}
	/// <summary>
	/// In OnEnable all the listeners are being added
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.LOSTLIFE, LossOfLife);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
	}
	/// <summary>
	/// In OnDisable all the listeners are being removed
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, LossOfLife);
		EventManager.RemoveListener (StaticEventNames.RESTART, Restart);
	}
	/// <summary>
	/// On restart all the lives are beind set to their old value and the art is being updated
	/// </summary>
	private void Restart ()
	{
		livesCounter = 3;
		UpdateArt();
	}
	/// <summary>
	/// LossOfLife the lifes are being made less and art is being updated
	/// </summary>
	private void LossOfLife ()
	{
		livesCounter--;
		UpdateArt();
	}
	/// <summary>
	/// UpdateArt takes care of that the art is correctly being updated to the value of lifes it has
	/// </summary>
	private void UpdateArt ()
	{
		if (livesCounter >= 0)
		{
			livesImage.sprite = lives[livesCounter];
		}

		if (livesCounter == 0)
		{
			EventManager.TriggerEvent (StaticEventNames.ENDGAME);
		}
	}
}
