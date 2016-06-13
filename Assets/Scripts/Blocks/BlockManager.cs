//Fabian Verkuijlen
//Created on: 15/04/2016

using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
	[SerializeField] private int fieldWidth;
	[SerializeField] private int fieldHeight;
	[SerializeField] private GameObject[] farmStageTiles;
	[SerializeField] private GameObject[] forrestStageTiles;
	[SerializeField] private GameObject[] farmSpecialBlocks;
	[SerializeField] private GameObject[] forrestSpecialBlocks;
	[SerializeField] private GameObject FarmPigUp;
	[SerializeField] private GameObject forrestPigUp;
	private GameObject pigup;
	private GameObject[] specialBlocks;
	private GameObject[] currentStageTiles;
	private int stageID;
	private List<List<Block>> blocks;

	private int normalPercentage = 90;

	[SerializeField] private Transform playerPosition;
	private Vector3 nextLinePosition;

	private int nextLine;
	/// <summary>
	/// Adds the listeners on enable
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.RESTART, RestartGame);
	}
	/// <summary>
	/// removes the listeners on disable
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.RESTART, RestartGame);
	}
	/// <summary>
	/// Sets the tiles and generates them.
	/// </summary>
	protected void Awake()
	{
		SetOriginTiles ();
		Generation ();
	}
	/// <summary>
	/// Sets the correct tiles so that the right tiles will be selected for generation
	/// </summary>
	private void SetOriginTiles ()
	{
		currentStageTiles = farmStageTiles;
		specialBlocks = farmSpecialBlocks;
		pigup = FarmPigUp;
	}
	/// <summary>
	/// on reset it resets the tiles
	/// </summary>
	private void RestartGame ()
	{
		SetOriginTiles ();
		Reset (true);
	}

	/// <summary>
	/// Generates all the blocks into the game
	/// </summary>
	private void Generation()
	{
		blocks = new List<List<Block>>();
		for (int x = 0; x < fieldWidth; x++)
		{
			List<Block> tempList = new List<Block>();
			for (int y = 0; y < fieldHeight; y++)
			{
				int percentageCounter = Random.Range(0 , 100);
				if (percentageCounter < normalPercentage)
				{
					if (percentageCounter < 3)
					{
						//pig up
						GameObject instantiateBlock = (GameObject) Instantiate (pigup, new Vector2 (transform.position.x + x, transform.position.y - (fieldHeight - y)), Quaternion.identity);
						instantiateBlock.GetComponent<Block> ().Position = new Vector2 (x, y);
						tempList.Add (instantiateBlock.GetComponent<Block> ());
					}
					else
					{
						//normal tile
						GameObject instantiateBlock = (GameObject) Instantiate (currentStageTiles[Random.Range (0, currentStageTiles.Length)], new Vector2 (transform.position.x + x, transform.position.y - (fieldHeight - y)), Quaternion.identity);
						instantiateBlock.GetComponent<Block> ().Position = new Vector2 (x, y);
						tempList.Add (instantiateBlock.GetComponent<Block> ());
					}
				}
				else
				{
					//special tile
					GameObject instantiateBlock = (GameObject) Instantiate (specialBlocks[Random.Range (0, specialBlocks.Length)], new Vector2 (transform.position.x + x, transform.position.y - (fieldHeight - y)), Quaternion.identity);
					instantiateBlock.GetComponent<Block> ().Position = new Vector2 (x, y);
					tempList.Add (instantiateBlock.GetComponent<Block> ());
				}
			}
			blocks.Add(tempList);
		}
	}
	/// <summary>
	/// Sets and holds track of stage ID for the right type of block inspawning
	/// </summary>
	private void UpdateStageID ()
	{
		stageID++;
		if (stageID == 2)
		{
			stageID = 0;
			normalPercentage -= 7;
		}
		switch (stageID)
		{
			case 0:
				currentStageTiles = farmStageTiles;
				specialBlocks = farmSpecialBlocks;
				pigup = FarmPigUp;
			break;
			case 1:
				currentStageTiles = forrestStageTiles;
				specialBlocks = forrestSpecialBlocks;
				pigup = forrestPigUp;
			break;
		}
	}
	/// <summary>
	/// resets stage and generation
	/// </summary>
	/// <param name="restart"></param>
	public void Reset (bool restart)
	{
		if (!restart)
		{
			UpdateStageID ();
		}
		Invoke("Generation", 1f);
		RemoveBlocks();
		nextLine = 0;
		nextLinePosition.y = nextLine;
		blocks = new List<List<Block>>();
	}
	/// <summary>
	/// Removes blocks at certain position.
	/// </summary>
	private void RemoveBlocks ()
	{
		for (int x = 0; x < fieldWidth; x++)
		{
			for (int y = 0; y < fieldHeight; y++)
			{
				if (blocks != null && blocks[x][y])
				{
					Destroy(blocks[x][y].gameObject);
				}
			}
		}
	}
}
