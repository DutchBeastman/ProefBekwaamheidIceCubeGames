// Created by: Jeremy Bond
// Date: 21/04/2016

using UnityEngine;

public class GamePartsManager : MonoBehaviour
{
	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject levelCreation;

	protected void Start()
	{
		EventManager.AddListener(StaticEventNames.ENABLEMAINMENU, ActivateMainMenu);
		EventManager.AddListener(StaticEventNames.ENABLEGAMECREATION, ActivateGameCreation);
		ActivateMainMenu();
	}

	public void ActivateMainMenu()
	{
		levelCreation.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void ActivateGameCreation()
	{
		mainMenu.SetActive(false);
		levelCreation.SetActive(true);
	}
}
