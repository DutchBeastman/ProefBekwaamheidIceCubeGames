//Fabian Verkuijlen
//Created on: 15/04/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Type
{
	Blue = 0,
	Red,
	Purple,
	Yellow,
	HardToGetThrough,
	PickUp,
	Empty
}

public enum Side
{
	sides = 0, up, down
}

public class Block : MonoBehaviour {
	
	[SerializeField] private int lives;
	[SerializeField] public int points;
	[SerializeField] public int damage;
	[SerializeField] private bool killNeighbours;
	[SerializeField] private bool unbreakable;
	
	[HideInInspector] public bool killed;
	[HideInInspector] public bool falling = false;

	private Rigidbody2D rigid2D;
	public bool pickUp;
	public Type type;

	private Vector2 position;
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
	

	private void Awake()
	{
		rigid2D = gameObject.GetComponent<Rigidbody2D>();
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
				hits.Add(Physics2D.Raycast(origin + (Vector2.left * distance), Vector2.left, 0.1f));
				hits.Add(Physics2D.Raycast(origin + (Vector2.right * distance), Vector2.right, 0.1f));
				Debug.DrawRay(origin + (Vector2.left * distance), Vector2.left * 0.1f, Color.green, 2);
				Debug.DrawRay(origin + (Vector2.right * distance), Vector2.right * 0.1f, Color.yellow, 2);
				break;

			/// up
			case Side.up:
				hits.Add(Physics2D.Raycast(origin + (Vector2.up * distance), Vector2.up, 0.1f));
				Debug.DrawRay(origin + (Vector2.up * distance), Vector2.up * 0.1f, Color.blue, 2);
				break;

			/// down
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
		string pointsTrigger = "GetPoints" + points.ToString ();
		EventManager.TriggerEvent(pointsTrigger);
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
				Invoke ("StopFalling", 0.1f);
			}
		}
		else
		{
			if (pickUp)
			{
				Destroy (gameObject);
				EventManager.TriggerEvent (StaticEventNames.GOTPICKUP);
			}
		}
	}
}
