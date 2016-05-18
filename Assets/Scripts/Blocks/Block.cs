//Fabian Verkuijlen
//Created on: 15/04/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Type
{
	Blue = 0, Red, Purple, Yellow, HardToGetThrough,
	// Hard,
	// Air,
	Empty
}

public enum Side
{
	sides = 0, up, down
}

public class Block : MonoBehaviour {

	[SerializeField] private Sprite[] tileSprites = new Sprite[16];
	[SerializeField] private int lives;
	[SerializeField] public int points;
	[SerializeField] public int damage;
	[SerializeField] private bool killNeighbours;
	[SerializeField] private bool unbreakable;

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

	//Testing Purpose
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
		//Invoke("SetOffset", 0.1f);
	}

	private List<RaycastHit2D> GetNeighbours(Side side)
	{
		float distance = transform.localScale.x + 0.1f;
		Vector2 origin = transform.position;

		List<RaycastHit2D> hits = new List<RaycastHit2D>();
		switch(side)
		{
			/// sides
			case Side.sides:
				/// left
				hits.Add(Physics2D.Raycast(origin + (Vector2.left * distance), Vector2.left, 0.1f));
				/*
				if (hits[hits.Count - 1].collider != null && hits[hits.Count - 1].collider.GetComponent<Block> ())
				{
					if (hits[hits.Count - 1].collider.GetComponent<Block> ().type == type)
					{
						neighbourLeft = true;
					}
				}*/
				/// right
				hits.Add(Physics2D.Raycast(origin + (Vector2.right * distance), Vector2.right, 0.1f));
				/*if (hits[hits.Count - 1].collider != null && hits[hits.Count - 1].collider.GetComponent<Block> ())
				{
					if (hits[hits.Count - 1].collider.GetComponent<Block> ().type == type)
					{
						neighbourRight = true;
					}
				}*/
				Debug.DrawRay(origin + (Vector2.left * distance), Vector2.left * 0.1f, Color.green, 2);
				Debug.DrawRay(origin + (Vector2.right * distance), Vector2.right * 0.1f, Color.yellow, 2);
				break;

			/// up
			case Side.up:
				hits.Add(Physics2D.Raycast(origin + (Vector2.up * distance), Vector2.up, 0.1f));
				/*if (hits[hits.Count - 1].collider != null && hits[hits.Count - 1].collider.GetComponent<Block> ())
				{

					if (hits[hits.Count - 1].collider.GetComponent<Block> ().type == type)
					{
						neighbourUp = true;
					}
				}*/
				Debug.DrawRay(origin + (Vector2.up * distance), Vector2.up * 0.1f, Color.blue, 2);
				break;

			/// down
			case Side.down:
				hits.Add(Physics2D.Raycast(origin + (Vector2.down * distance), Vector2.down, 0.1f));
				/*if (hits[hits.Count - 1].collider != null && hits[hits.Count - 1].collider.GetComponent<Block> ())
				{

					if (hits[hits.Count - 1].collider.GetComponent<Block> ().type == type)
					{
						neighbourDown = true;
					}
				}*/
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
				if (hitsX[i].transform.name != this.name)
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
		EventManager.TriggerEvent("GetPoints" + points);
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

		if (killNeighbours)
		{
			List<RaycastHit2D> hitsX = GetNeighbours(Side.sides);
			KillNeighbour (hitsX);
			List<RaycastHit2D> hitsYUp = GetNeighbours(Side.up);
			KillNeighbour (hitsYUp);
			List<RaycastHit2D> hitsYDown = GetNeighbours(Side.down);
			KillNeighbour (hitsYDown);
		}

		lives --;

		if(lives == 0 && !unbreakable)
		{
			Destroy (gameObject);
		}
	}

	private void KillNeighbour (List<RaycastHit2D> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			Block b;
			if (list[i].collider != null)
			{
				if (list[i].transform.GetComponent<Block> ())
				{
					b = list[i].transform.GetComponent<Block> ();
					if (b.type == this.type && !b.killed)
					{
						b.GetKilled ();
					}
				}
			}
		}
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
			}
		}
	}

	/// Old connecting tiles art to each other code
	/// Not used due art issues
	/*
    public void SetOffset()
	{
		List<RaycastHit2D> l;
		l = GetNeighbours (Side.down);
		l = GetNeighbours (Side.up);
		l = GetNeighbours (Side.sides);
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

		int t = (int)xOffset + (int)(4-(yOffset)) * 4;
		rend.sprite = tileSprites[(t - 1)];
		Debug.Log(t-1);
		//rend.material.mainTextureOffset = new Vector2(xOffset, yOffset);
	}
	*/
}
