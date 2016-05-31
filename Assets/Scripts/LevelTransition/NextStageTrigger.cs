// Created by: Jeremy Bond
// Date: 20/04/2016

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(BoxCollider2D))]
public class NextStageTrigger : MonoBehaviour
{
	[SerializeField] private LogoTransition logo;
	[SerializeField] private BlockManager manager;

	/// <summary>
	/// In OnTrigger the next stage can be activated if the player falls through this point
	/// </summary>
	/// <param name="col">col is the collider of the object that this object hits</param>
	protected void OnTriggerEnter2D(Collider2D col)
	{
		if(col.name == "Player")
		{
			TriggerNextStageArt();
			logo.ShowLogo();
			manager.Reset(false);
		}
	}
	/// <summary>
	/// This function triggers the next stage.
	/// </summary>
	private void TriggerNextStageArt ()
	{
		EventManager.TriggerEvent(StaticEventNames.NEXTSTAGE);
	}
}
