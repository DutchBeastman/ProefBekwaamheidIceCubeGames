using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Nodes
{
	public class Node : MonoBehaviour, INode
	{
		private Vector2 nodePos;
		private List<INode> neighbourNodes = new List<INode>();
		public IEnumerable<INode> Neighbours
		{
			get
			{
				//Get all neighbours from tile
				return neighbourNodes;
			}
		}
		public Vector2 NodePos
		{
			get
			{
				return nodePos;
			}
			set
			{
				nodePos = value;
			}
		}
		public void AddNeighbour(INode neighbour)
		{
			neighbourNodes.Add(neighbour);
		}
	}
}
