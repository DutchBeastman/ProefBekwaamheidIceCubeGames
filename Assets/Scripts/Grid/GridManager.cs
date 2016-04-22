using UnityEngine;
using System.Collections;
namespace Grid
{
	public class GridManager : MonoBehaviour
	{
		[SerializeField]
		private Vector2 tileSize = new Vector2(1,1);
		[SerializeField]
		private Vector2 gridSize = new Vector2(6, 40);

		protected void Awake()
		{

		}
	}
}