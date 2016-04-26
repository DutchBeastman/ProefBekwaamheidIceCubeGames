// Created by: Fabian Verkuijlen
// Date: 21/04/2016
using UnityEngine;
using System.Collections;

namespace Grid
{
	public class GridPoint : MonoBehaviour
	{
		private Vector2 gridPos;
		private Block block;
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
		public Block Block
		{
			get
			{
				return block;
			}
			set
			{
				block = value;
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