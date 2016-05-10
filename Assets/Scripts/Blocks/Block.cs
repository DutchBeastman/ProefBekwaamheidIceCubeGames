using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Type
{
	Blue = 0, Red, Purple, Yellow,
	// Hard,
	// Air,
	Empty
}

public enum Side
{
	sides = 0, up, down
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

	private Rigidbody2D rigid2D;

	private void Awake()
	{
		rend = gameObject.GetComponent<SpriteRenderer>();
		rigid2D = gameObject.GetComponent<Rigidbody2D>();
		SetOffset();
	}

	private List<RaycastHit2D> GetNeighbours(Side side)
	{
		float distance = transform.localScale.x + 0.1f;
		Vector2 origin = transform.position;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		switch(side)
		{
			case Side.sides:
				hits.Add(Physics2D.Raycast(origin + (Vector2.left * distance), Vector2.left, 0.1f));
				hits.Add(Physics2D.Raycast(origin + (Vector2.right * distance), Vector2.right, 0.1f));
				Debug.DrawRay(origin + (Vector2.left * distance), Vector2.left * 0.1f, Color.green, 2);
				Debug.DrawRay(origin + (Vector2.right * distance), Vector2.right * 0.1f, Color.yellow, 2);
				break;
			case Side.up:
				hits.Add(Physics2D.Raycast(origin + (Vector2.up * distance), Vector2.up, 0.1f));
				Debug.DrawRay(origin + (Vector2.up * distance), Vector2.up * 0.1f, Color.blue, 2);
				break;
			case Side.down:
				hits.Add(Physics2D.Raycast(origin + (Vector2.down * distance), Vector2.down, 0.1f));
				Debug.DrawRay(origin + (Vector2.down * distance), Vector2.down * 0.1f, Color.red, 2);
				break;
		}
		return hits;
	}

	public void CheckNeighboursFalling()
	{
		List<RaycastHit2D> hitsX = GetNeighbours(Side.sides);
		for (int i = 0; i < hitsX.Count; i++)
		{
			if (hitsX[i].collider != null)
			{
				if (hitsX[i].transform.name != gameObject.name)
				{
					Block b;
					if (b = hitsX[i].transform.GetComponent<Block>())
					{
						if (b.type == this.type && !b.falling && !b.killed)
						{
							falling = false;
						}
					}
				}
			}
		}
		List<RaycastHit2D> hitsYDown = GetNeighbours(Side.down);
		for (int i = 0; i < hitsYDown.Count; i++)
		{
			if (hitsYDown[i].collider != null)
			{
				if (hitsYDown[i].transform.name != this.name)
				{
					Block b;
					if (b = hitsYDown[i].transform.GetComponent<Block>())
					{
						if (!b.falling && !b.killed)
						{
							falling = false;
						}
					}
				}
			}
		}
	}

	public void GetKilled()
	{
		killed = true;
		TellNeighboursToFall();
		KillGroup();
	}

	private void TellNeighboursToFall()
	{
		List<RaycastHit2D> hitsYUp = GetNeighbours(Side.up);
		for (int i = 0; i < hitsYUp.Count; i++)
		{
			Block b;
			if(hitsYUp[i].collider != null)
			{
				if (hitsYUp[i].collider.transform.GetComponent<Block>() != null)
				{
					b = hitsYUp[i].transform.GetComponent<Block>();
					if (!b.killed)
					{
						b.StartFalling();
					}
				}
			}
		}
	}

	public void KillGroup()
	{
		killed = true;
		List<RaycastHit2D> hitsX = GetNeighbours(Side.sides);
		for (int i = 0; i < hitsX.Count; i++)
		{
			Block b;
			if (hitsX[i].collider != null)
			{
				if (hitsX[i].transform.GetComponent<Block>())
				{
					b = hitsX[i].transform.GetComponent<Block>();
					if (b.type == this.type && !b.killed)
					{
						b.GetKilled();
					}
				}
			}
		}
		List<RaycastHit2D> hitsYUp = GetNeighbours(Side.up);
		for (int i = 0; i < hitsYUp.Count; i++)
		{
			Block b;
			if (hitsYUp[i].collider != null)
			{
				if (hitsYUp[i].transform.GetComponent<Block>())
				{
					b = hitsYUp[i].transform.GetComponent<Block>();
					if (b.type == this.type && !b.killed)
					{
						b.GetKilled();
					}
				}
			}
		}
		List<RaycastHit2D> hitsYDown = GetNeighbours(Side.down);
		for (int i = 0; i < hitsYDown.Count; i++)
		{
			Block b;
			if (hitsYDown[i].collider != null)
			{
				if (hitsYDown[i].transform.GetComponent<Block>())
				{
					b = hitsYDown[i].transform.GetComponent<Block>();
					if (b.type == this.type && !b.killed)
					{
						b.GetKilled();
					}
				}
			}
		}
		Destroy(gameObject);
	}

	public void StartFalling()
	{
		CheckNeighboursFalling();
		TellNeighboursToFall();
		Invoke("fall" , 0.3f);
	}
	private void fall()
	{
		falling = true;
	}

	public void StopFalling()
	{
		falling = false;
		rigid2D.isKinematic = true;
	}

	private void Update()
	{
		if (falling)
		{
			rigid2D.isKinematic = false;
		}
	}
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.name != "Player")
		{
			if (rigid2D.velocity.y > 0.2f)
			{
				Invoke("StopFalling" , 0.1f);
				Debug.Log(coll.collider);
			}
		}
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
