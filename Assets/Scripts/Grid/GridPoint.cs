using UnityEngine;
using System.Collections;

namespace Grid
{
	public class GridPoint : MonoBehaviour
	{
		private Vector2 gridPos;
		private Type type;
		private bool empty;

		public Vector2 GridPos
		{
			get
			{
				return gridPos;
			}
			set
			{
				gridPos = value;
			}
		}
		public Type PieceType
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}
		public bool Empty
		{
			get
			{
				return empty;
			}
			set
			{
				empty = value;
			}
		}
	}
}