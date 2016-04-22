//Fabian Verkuijlen
//Created on: 15/04/2016

using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
	[SerializeField]private int fieldWidth;
	[SerializeField]private int fieldHeight;
	[SerializeField]private GameObject[] tiles;
	private List<List<Block>> blocks;

	[SerializeField] private Transform playerPosition;
	private Vector3 nextLinePosition;

	private int nextLine;

	protected void Awake()
	{
		Generation();
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
				GameObject instantiateBlock = (GameObject)Instantiate(tiles[Random.Range(0 , tiles.Length)] , new Vector2(transform.position.x + x , transform.position.y - (fieldHeight - y) ) , Quaternion.identity);
				instantiateBlock.GetComponent<Block>().Position = new Vector2(x , y);
				tempList.Add(instantiateBlock.GetComponent<Block>());
			}
			blocks.Add(tempList);
		}
	}

	/// <summary>
	/// Function for checking next line for checking neighbours
	/// </summary>
	protected void Update()
	{
		if (blocks != null)
		{
			if (MathUtils.difference(playerPosition.position.y , -nextLinePosition.y) < 15)
			{
				nextLine++;
				CheckLinesNeighbours();
			}
		}
	}
	/// <summary>
	/// Check of the next neighbour
	/// </summary>
	private void CheckLinesNeighbours()
	{
		nextLinePosition.y = nextLine;
		if (nextLine < fieldHeight)
		{
			for (int i = 0; i < fieldWidth; i++)
			{
				DetermineNeighbours(i, fieldHeight - nextLine);
			}
		}
	}
	/// <summary>
	/// Determines the neighbour blocks
	/// </summary>
	/// <param name="x"> X value as in position relative to the list where they are in.</param>
	/// <param name="y"> Y value as in position relative to the list where they are in.</param>
	private void DetermineNeighbours(int x, int y)
	{
		if (blocks[x][y])
		{	
			Block b = blocks[x][y];

			if (x > 0)
			{
				if (blocks[x - 1][y].type == b.type)
				{
					b.neighbourLeft = true;
				}
			}
			if (x < fieldWidth-1)
			{
				if (blocks[x + 1][y].type == b.type)
				{
					b.neighbourRight = true;
				}
			}
			if (y > 0)
			{
				if (blocks[x][y - 1].type == b.type)
				{
					b.neighbourDown = true;
				}
			}
			if (y < fieldHeight-1)
			{
				if (blocks[x][y + 1].type == b.type)
				{
					b.neighbourUp = true;
				}
			}
			b.SetOffset();
		}
	}
	/// <summary>
	/// Funtion to kill of the neighbour blocks
	/// </summary>
	/// <param name="b">in this param the value of Block is given as b</param>
    private void KillNeighbours(Block b)
    {
        if (b.neighbourDown)
        {
            KillTile(blocks[(int)b.Position.x] [(int)b.Position.y - 1]);
        }
        if (b.neighbourLeft)
        {
            KillTile(blocks[(int)(b.Position.x - 1)] [(int)b.Position.y]);
        }
        if (b.neighbourRight)
        {
            KillTile(blocks[(int)(b.Position.x + 1)] [(int)b.Position.y]);
        }
        if (b.neighbourUp)
        {
            KillTile(blocks[(int)b.Position.x] [(int)b.Position.y + 1]);
        }
    }
	/*private void FloatingNeighbours(Block b)
	{
		List<Block> listFloatBlocks = new List<Block>();
		listFloatBlocks.Add(blocks[(int)b.Position.x][(int)b.Position.y + 1]);
		for (int i = 0; i < listFloatBlocks.Count; i++)
		{
			listFloatBlocks[i].Position -= new Vector2(0,-1);
		}
	}*/
	/// <summary>
	/// Funtion to kill of the one tile that needs to be removed, this is used on everyblock that killNeighbours requests
	/// </summary>
	/// <param name="b"> in this param the value of Block is given as b</param>
	public void KillTile(Block b)
    {
        if (!b.killed)
        {
            b.killed = true;
            KillNeighbours(b);
			//blocks[(int)b.Position.x][(int)b.Position.y] = null;
            Destroy(b.gameObject);
        }
	}
	
    public void Reset()
	{
		Invoke("Generation" , 1f);
		for (int x = 0; x < fieldWidth; x++)
		{
			for (int y = 0; y < fieldHeight; y++)
			{
				if (blocks[x][y])
				{
					Destroy(blocks[x][y].gameObject);
				}
			}
		}
		blocks = new List<List<Block>>();
	}
}
