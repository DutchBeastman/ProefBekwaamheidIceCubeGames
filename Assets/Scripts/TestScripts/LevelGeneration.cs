//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;
using System.Collections.Generic;

public class LevelGeneration : MonoBehaviour
{
	private List<GameObject> levelTiles;
	[SerializeField] private GameObject levelOneTiles;
	[SerializeField] private GameObject strongTile;
	[SerializeField] private GameObject unbreakableTile;
	[SerializeField] private GameObject lifeTile;


	private List<GameObject> tileArray;

	protected void Awake ()
	{
		levelTiles = new List<GameObject> ();
		for (int i = 0; i < levelOneTiles.transform.childCount; i++)
		{
			levelTiles.Add (levelOneTiles.transform.GetChild (i).gameObject);
		}
	}

	protected void Start ()
	{
		GenerateLevel ();
	}

	private void GenerateLevel ()
	{
		for (int x = 0; x < 7; x++)
		{
			for (int y = 0; y < 100; y++)
			{
				CreateTile (-x, -y);
			}
		}
	}
	private void CreateTile (float x, float y)
	{
		int levelID = Random.Range (0, levelTiles.Count);
		Instantiate (levelTiles[levelID], new Vector3 (x, y, 0), transform.rotation);
	}
}
