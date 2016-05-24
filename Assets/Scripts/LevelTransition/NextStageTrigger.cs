// Created by: Jeremy Bond
// Date: 20/04/2016

using UnityEngine;
using System.Collections;

[RequireComponent( typeof(BoxCollider2D))]
public class NextStageTrigger : MonoBehaviour
{
	[SerializeField] private LogoTransition logo;
	[SerializeField] private BlockManager manager;

	protected void OnTriggerEnter2D(Collider2D col)
	{
		if(col.name == "Player")
		{
			TriggerNextStageArt();
			logo.ShowLogo();
			manager.Reset();
		}
	}

	private void TriggerNextStageArt ()
	{
		EventManager.TriggerEvent("NextStage");
	}
}
