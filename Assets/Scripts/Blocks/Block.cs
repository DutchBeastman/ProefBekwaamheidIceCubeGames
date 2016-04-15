using UnityEngine;
using System.Collections;

public enum Type
{
	Blue = 0, Green, Pink, Yellow,
	// Hard,
	// Air,
	Empty
}
public class Block : MonoBehaviour {

	private Vector2 position;
	public Type type;
	public Vector2 Position
	{
		get
		{
			return position;
		}
		set
		{
			position = value;
		}
	}
}
