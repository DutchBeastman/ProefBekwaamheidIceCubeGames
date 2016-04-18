using UnityEngine;
using System.Collections;

public enum Type
{
	Blue = 0, Red, Purple, Yellow,
	// Hard,
	// Air,
	Empty
}
public class Block : MonoBehaviour {

	private Vector2 position;
	public Type type;
    public bool killed;
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

	//Testing Purposes
	/*[HideInInspector]*/ public bool neighbourUp;
	/*[HideInInspector]*/ public bool neighbourRight;
	/*[HideInInspector]*/ public bool neighbourDown;
	/*[HideInInspector]*/ public bool neighbourLeft;
	private float xOffset;
	private float yOffset; 
	private int neighbourCount;

	private float quarter = 0.25f;
	private float half = 0.5f;
	private float threeQuarter = 0.75f;


	private SpriteRenderer rend;

	private void Awake()
	{
		rend = gameObject.GetComponent<SpriteRenderer>();
		SetOffset();
	}

	public void SetOffset()
	{
		if (neighbourUp)
		{
			neighbourCount++;
		}
		if (neighbourRight)
		{
			neighbourCount++;
		}
		if (neighbourDown)
		{
			neighbourCount++;
		}
		if (neighbourLeft)
		{
			neighbourCount++;
		}
		switch (neighbourCount)
		{
			case 0:
				yOffset = 0;
				xOffset = threeQuarter;
				break;
			case 1:
				yOffset = threeQuarter;
				xOffset = 0;
				if (neighbourUp)
				{
					xOffset = quarter;
				}
				else if (neighbourLeft)
				{
					xOffset = half;
				}
				else if(neighbourRight)
				{
					xOffset = threeQuarter;
				}
				break;
			case 2:
				yOffset = 0;
				xOffset = 0;
				if(neighbourDown && neighbourUp)
				{
					xOffset = quarter;
				}
				else if(!neighbourLeft || !neighbourRight)
				{
					yOffset = quarter;
					if (neighbourDown)
					{
						if (neighbourLeft)
						{
							xOffset = 0;
						}
						else
						{
							xOffset = quarter;
						}
					}
					else
					{
						if (neighbourLeft)
						{
							xOffset = half;
						}
						else
						{
							xOffset = threeQuarter;
						}
					}
				}
				break;
			case 3:
				yOffset = half;
				xOffset = 0;
				if (!neighbourUp)
				{
					xOffset = quarter;
				}
				else if(!neighbourLeft)
				{
					xOffset = half;
				}
				else if (!neighbourRight)
				{
					xOffset = threeQuarter;
				}
				break;
			case 4:
				xOffset = half;
				yOffset = 0;
				break;
		}
		rend.material.mainTextureOffset = new Vector2(xOffset, yOffset);
	}
}
