using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{

	[SerializeField]private GameObject[] tiles;
	[SerializeField]private Dictionary<Block, Vector2> listOfBlocks;
	private List<Vector2> directionsList = new List<Vector2>();
	[SerializeField]private int fieldX;
	[SerializeField]private int fieldY;

	protected void Awake()
	{
		directionsList.Add(new Vector2(+1 , 0));
		directionsList.Add(new Vector2(-1 , 0));
		directionsList.Add(new Vector2(0 , -1));
		directionsList.Add(new Vector2(0 , +1));
		Generation();
	}
	private void Generation()
	{
		for (int x = 0; x < fieldX; x++)
		{
			for (int y = 0; y < fieldY; y++)
			{
				GameObject instatiateBlock = (GameObject)Instantiate(tiles[Random.Range(0 , tiles.Length)] , new Vector2(transform.position.x + x , transform.position.y - y ) , Quaternion.identity);
				Block block = instatiateBlock.GetComponent<Block>();
				//listOfBlocks.Add(block , new Vector2(x , y));
			}

		}
	}
	private void DetermineNeighbours(Block current)
	{

	}
}
