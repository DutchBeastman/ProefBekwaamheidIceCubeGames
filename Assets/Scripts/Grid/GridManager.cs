// Created by: Fabian Verkuijlen
// Date: 21/04/2016

using UnityEngine;
using System.Collections.Generic;
namespace Grid
{
	public class GridManager : MonoBehaviour
	{
		[SerializeField]
		private Vector2 tileSize = new Vector2(1,1);
		[SerializeField]
		private Vector2 gridSize = new Vector2(6, 40);
		[SerializeField]
		private GameObject prefab;
		
		private List<List<GameObject>> listOfGrid;

        public List<List<GameObject>> ListOfGrid
        {
            get
            {
                return listOfGrid;
            }
            set
            {
                listOfGrid = value;
            }
        }

        protected void Awake()
		{
			listOfGrid = new List<List<GameObject>>();
			for (int x = 0; x < gridSize.x; x++)
			{
				List<GameObject> newList = new List<GameObject>();
				listOfGrid.Add(newList);
				for (int y = 0; y < gridSize.y; y++)
				{
					GameObject gridInstance = (GameObject)Instantiate(prefab ,new Vector3(tileSize.x + x,tileSize.y + -y, 0), Quaternion.identity);
					gridInstance.GetComponent<GridPoint>().GridPos = new Vector2(x , y);
					listOfGrid[x].Add(gridInstance);
					gridInstance.name = "GridInstance: " + (y+1);
				}	
			}
		}
	}
}
