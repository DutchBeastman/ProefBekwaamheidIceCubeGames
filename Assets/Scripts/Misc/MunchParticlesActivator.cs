// Created by: Jeremy Bond
// Date: 10/06/2016

using UnityEngine;
using System.Collections;

public class MunchParticlesActivator : MonoBehaviour 
{
	[SerializeField] private ParticleSystem system;

	protected void OnEnable () 
	{
		EventManager.AddListener(StaticEventNames.MUNCHPARTICLE, Emit);
	}
	
	protected void OnDisable () 
	{
		EventManager.RemoveListener(StaticEventNames.MUNCHPARTICLE, Emit);
	}

	private void Emit ()
	{
		system.Emit(15);
	}
}
