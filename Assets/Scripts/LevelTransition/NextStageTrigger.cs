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
			logo.ShowLogo();
			col.transform.position = new Vector3(col.transform.position.x, 35, 0);
			manager.Reset();
		}
	}
}
