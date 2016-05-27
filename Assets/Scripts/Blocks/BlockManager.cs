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
	[SerializeField]private GameObject[] specialBlocks;
	private GameObject[] currentStageTiles;
	private int stageID;
	private List<List<Block>> blocks;

	[SerializeField] private Transform playerPosition;
	private Vector3 nextLinePosition;

	private int nextLine;

	protected void OnEnable ()
	{
		EventManager.AddListener(StaticEventNames.RESTART, StartGame);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener(StaticEventNames.RESTART, StartGame);
	}

	protected void Awake()
	{
		currentStageTiles = farmStageTiles;
		Generation();
	}

	private void StartGame ()
	{
		currentStageTiles = farmStageTiles;
		Reset();
	}

	/// <summary>
	/// Function for the generation
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
				if (percentageCounter < 93)
				{
					GameObject instantiateBlock = (GameObject)Instantiate(currentStageTiles[Random.Range(0 , currentStageTiles.Length)] , new Vector2(transform.position.x + x , transform.position.y - ( fieldHeight - y )) , Quaternion.identity);
					instantiateBlock.GetComponent<Block>().Position = new Vector2(x , y);
					tempList.Add(instantiateBlock.GetComponent<Block>());
				}
				else
				{
					GameObject instantiateBlock = (GameObject)Instantiate(specialBlocks[Random.Range(0 , specialBlocks.Length)] , new Vector2(transform.position.x + x , transform.position.y - ( fieldHeight - y )) , Quaternion.identity);
					instantiateBlock.GetComponent<Block>().Position = new Vector2(x , y);
					tempList.Add(instantiateBlock.GetComponent<Block>());
				}
			}
			blocks.Add(tempList);
		}
	}

	public void Reset()
	{
		stageID ++;
		if (stageID == 2)
		{
			stageID = 0;
		}
		switch (stageID)
		{
			case 0:
			currentStageTiles = farmStageTiles;
				break;
			case 1:
			currentStageTiles = forrestStageTiles;
				break;
		}
		Invoke("Generation", 1f);
		RemoveBlocks();
		nextLine = 0;
		nextLinePosition.y = nextLine;
		blocks = new List<List<Block>>();
	}

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
