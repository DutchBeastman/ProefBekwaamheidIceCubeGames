using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Nodes
{
	public interface INode
	{
		IEnumerable<INode> Neighbours
		{
			get;
		}
	}
}
