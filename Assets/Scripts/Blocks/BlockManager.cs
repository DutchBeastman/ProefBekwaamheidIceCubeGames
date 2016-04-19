using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
	
	[SerializeField]private int fieldWidth;
	[SerializeField]private int fieldHeight;
	[SerializeField]private GameObject[] tiles;
	private List<List<Block>> blocks;
	private List<Vector2> directionsList = new List<Vector2>();

	[SerializeField] private Transform playerPosition;
	private Vector3 nextLinePosition;

	private int nextLine;


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

	protected void Update()
	{
		if(MathUtils.difference(playerPosition.position.y, -nextLinePosition.y) < 15)
		{
			nextLine++;
			CheckLinesNeighbours();
		}
	}

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
	
	private void DetermineNeighbours(int x, int y)
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
}
