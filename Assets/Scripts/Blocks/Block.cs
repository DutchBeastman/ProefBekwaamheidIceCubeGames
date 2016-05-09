using UnityEngine;
using System.Collections;
using System;

public enum Type
{
	Blue = 0, Red, Purple, Yellow,
	// Hard,
	// Air,
	Empty
}
public class Block : MonoBehaviour {

	[SerializeField]private Sprite[] tileSprites = new Sprite[16];
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

	private float quarter = 2f;
	private float half = 3f;
	private float threeQuarter = 4f;


	private bool falling = false;

	private SpriteRenderer rend;

	private void Awake()
	{
		rend = gameObject.GetComponent<SpriteRenderer>();
		SetOffset();
	}

	private RaycastHit2D[] GetNeighbours(bool XAxis)
	{
		float distance = transform.localScale.x + 0.1f;
		Vector2 originX = transform.position;
		originX += Vector2.left * distance;
		Vector2 originY = transform.position;
		originY += Vector2.up * distance;

		if (XAxis)
		{
			//Debug.DrawRay(originX, Vector2.right * distance * 2, Color.green, 2);
			RaycastHit2D[] hitsX = Physics2D.RaycastAll(originX, Vector2.right, distance * 2);
			Debug.Log(hitsX.Length + " X count");// WRONG NUMBER
			return hitsX;
		}
		else
		{
			//Debug.DrawRay(originY, Vector2.down * distance * 2, Color.red, 2);
			RaycastHit2D[] hitsY = Physics2D.RaycastAll(originY, Vector2.down, distance * 2);
			Debug.Log(hitsY.Length + " Y count");// WRONG NUMBER
			return hitsY;
		}
	}

	public void CheckNeighboursFalling()
	{
		RaycastHit2D[] hitsX = GetNeighbours(true);
		for (int i = 0; i < hitsX.Length; i++)
		{
			if(hitsX[i].transform.name != this.name)
			{
				Block b;
				if (b = hitsX[i].transform.GetComponent<Block>())
				{
					if (b.type == this.type && !b.falling)
					{
						falling = false;
					}
				}
			}
		}
		RaycastHit2D[] hitsY = GetNeighbours(false);
		for (int i = 0; i < hitsY.Length; i++)
		{
			if (hitsY[i].transform.name != this.name)
			{
				Block b;
				if (b = hitsY[i].transform.GetComponent<Block>())
				{
					if (!b.falling)
					{
						falling = false;
					}
				}
			}
		}
	}

	public void GetKilled()
	{
		Debug.Log("log getting killed");
		TellNeighboursToFall();
		KillGroup();
	}

	private void TellNeighboursToFall()
	{
		RaycastHit2D[] hitsY = GetNeighbours(false);
		for (int i = 0; i < hitsY.Length; i++)
		{
			Block b;
			if (b = hitsY[i].transform.GetComponent<Block>())
			{
				b.StartFalling();
			}
		}
	}

	public void KillGroup()
	{
		killed = true;
		RaycastHit2D[] hitsX = GetNeighbours(true);
		Debug.Log("Length of xAxis neighbours " + hitsX.Length);
		for (int i = 0; i < hitsX.Length; i++)
		{
			Debug.Log(i);
			Block b;
			if (b = hitsX[i].transform.GetComponent<Block>())
			{
				Debug.Log(i);
				if (b.type == this.type && !b.killed)
				{
					b.GetKilled();
					Debug.Log("neighbour block on y axis");
				}
			}
		}
		RaycastHit2D[] hitsY = GetNeighbours(false);
		Debug.Log("Length of yAxis neighbours " + hitsY.Length);
		for (int i = 0; i < hitsY.Length; i++)
		{
			Block b;
			if (b = hitsY[i].transform.GetComponent<Block>())
			{
				if (b.type == this.type && !b.killed)
				{
					b.GetKilled();
					Debug.Log("neighbour block on x axis");
				}
			}
		}
		Destroy(gameObject);
	}

	public void StartFalling()
	{
		falling = true;
		StartCoroutine(Falling());
	}

	public void StopFalling()
	{
		falling = false;
		StopCoroutine(Falling());
	}

	private IEnumerator Falling()
	{
		transform.Translate(0, 0.01f, 0);
		yield return new WaitForSeconds(0.1f);
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
				yOffset = 1;
				xOffset = threeQuarter;
				break;
			case 1:
				yOffset = threeQuarter;
				xOffset = 1;
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
				yOffset = 1;
				xOffset = 1;
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
							xOffset = 1;
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
				xOffset = 1;
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
				yOffset = 1;
				break;
		}

		int t = (int)xOffset + (int)(yOffset -1 ) * 4;
		rend.sprite = tileSprites[t - 1];
		//rend.material.mainTextureOffset = new Vector2(xOffset, yOffset);
	}
}
