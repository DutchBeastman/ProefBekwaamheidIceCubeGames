// Created by: Jeremy Bond
// Date: 20/04/2016

using UnityEngine;
using System.Collections;

public class FinalTile : MonoBehaviour
{
	[SerializeField] private BlockManager manager;
	[SerializeField] private LogoTransition logo;

	protected void OnDestroy()
	{
		logo.ShowLogo();
		//manager.Reset();
	}
}
