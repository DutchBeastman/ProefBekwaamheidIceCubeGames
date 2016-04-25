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
			for (int i = 0; i < gridSize.y; i++)
			{
				for (int j = 0; j < gridSize.x; j++)
				{
					GameObject gridInstance = (GameObject)Instantiate(prefab ,new Vector3(tileSize.x + i,tileSize.y + -j, 0), Quaternion.identity);
					listOfGrid[j].Add(gridInstance);
					gridInstance.name = "GridInstance: " + (j+1);
				}	
			}
		}
	}
}
