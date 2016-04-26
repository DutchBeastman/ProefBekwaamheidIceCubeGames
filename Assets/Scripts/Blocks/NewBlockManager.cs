using UnityEngine;
using System.Collections;
using Grid;

public class NewBlockManager : MonoBehaviour
{
	[SerializeField]private GameObject[] tiles;
    [SerializeField]private GridManager gridManager;
	[SerializeField] private Transform playerPosition;

	protected void Start()
	{
		Debug.Log(gridManager.ListOfGrid);

		Generation();
	}
    private void Generation()
    {
		for (int x = 0; x < gridManager.ListOfGrid.Count; x++)
		{
			for (int y = 0; y < gridManager.ListOfGrid[x].Count; y++)
			{
				GameObject blocks = (GameObject)Instantiate(tiles[Random.Range(0 , tiles.Length)] , gridManager.ListOfGrid[x][y].transform.position, Quaternion.identity);
			}
        }
    }

	private void CheckNextLinesNeighbours()
	{
		int currentLine = 0;
		if (MathUtils.difference(-currentLine , playerPosition.position.y) < 10)
		{
			for (int y = 0; y < gridManager.ListOfGrid.Count; y++)
			{

			}
		}
	}

	private void CheckAllNeighbours(Block b)
	{
		b.neighbourLeft = CheckLeftSide(b) ? true : false;
		b.neighbourRight = CheckRightSide(b) ? true : false;
		b.neighbourDown = CheckBottom(b) ? true : false;
		b.neighbourUp = CheckUpper(b) ? true : false;
	}

	private Block CheckLeftSide(Block b)
	{
		return gridManager.ListOfGrid[(int)b.Position.x][(int)b.Position.y].GetComponent <Block> () ;
	}

	private Block CheckRightSide(Block b)
	{
		return gridManager.ListOfGrid[(int)b.Position.x][(int)b.Position.y].GetComponent<Block>();
	}

	private Block CheckBottom(Block b)
	{
		return gridManager.ListOfGrid[(int)b.Position.x][(int)b.Position.y].GetComponent<Block>();
	}

	private Block CheckUpper(Block b)
	{
		return gridManager.ListOfGrid[(int)b.Position.x][(int)b.Position.y].GetComponent<Block>();
	}
}
