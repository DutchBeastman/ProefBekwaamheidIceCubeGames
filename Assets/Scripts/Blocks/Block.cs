//Fabian Verkuijlen
//Created on: 15/04/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Type
{
	RaspBerry = 0,
	BlueBerry,
	BlackBerry,
	RedCurrent,
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
	[SerializeField] private bool killNeighbours;
	[SerializeField] private bool unbreakable;
	
	[HideInInspector] public bool killed;
	[HideInInspector] public bool falling = false;

	[SerializeField] private Sprite[] destroyAnimation;

	private Rigidbody2D rigid2D;
	private SpriteRenderer render;
	private new BoxCollider2D collider;
	public bool pickUp;
	public Type type;

	private Vector2 position;
	/// <summary>
	/// Getter and setter for vector 2 position of the block
	/// </summary>
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
	
	/// <summary>
	/// In awake it sets the Rigidbody2D value. 
	/// </summary>
	private void Awake()
	{
		render = GetComponent<SpriteRenderer>();
		collider = GetComponent<BoxCollider2D>();
		rigid2D = GetComponent<Rigidbody2D>();
		StartCoroutine (FallingCheck ());
	}
	/// <summary>
	/// Gets the neighbours of the block
	/// </summary>
	/// <param name="side">represents each side of the block.</param>
	/// <returns></returns>
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
				break;

			/// up
			case Side.up:
				hits.Add(Physics2D.Raycast(origin + (Vector2.up * distance), Vector2.up, 0.1f));
				break;

			/// down
			case Side.down:
				hits.Add(Physics2D.Raycast(origin + (Vector2.down * distance), Vector2.down, 0.1f));
				break;
		}
		return hits;
	}
	/// <summary>
	/// Checks if the neighbours are falling to see if this block should fall too.
	/// </summary>
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
	/// <summary>
	/// when killed GetKilled will tell other neighbours to fall so it will fill its place and remove himself and his adjecent groups.
	/// </summary>
	public void GetKilled()
	{
		killed = true;
		TellNeighboursToFall();
		KillGroup();
	}
	/// <summary>
	/// this function tells the neighbours to fall.
	/// </summary>
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
	/// <summary>
	/// this funtion will tell the neighbours to stop falling.
	/// </summary>
	private void TellNeighboursToStopFalling ()
	{
		List<RaycastHit2D> hitsYUp = GetNeighbours (Side.up);
		for (int i = 0; i < hitsYUp.Count; i++)
		{
			Block b;
			if (hitsYUp[i].collider != null)
			{
				if (hitsYUp[i].collider.transform.GetComponent<Block> () != null)
				{
					b = hitsYUp[i].transform.GetComponent<Block> ();
					if (!b.killed)
					{
						b.StopFalling ();
					}
				}
			}
		}
	}
	/// <summary>
	/// this function takes care of the group of neighbours it should kill
	/// </summary>
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
			EventManager.TriggerScoreEvent (points);
			StartCoroutine(DestroyTile());
		}
	}
	/// <summary>
	/// this funtion takes care of which neighbour it should kill 
	/// </summary>
	/// <param name="list">the list is a list of hits that it gets after the check of neighbours</param>
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
	/// <summary>
	/// this function takes care of the neighbours to start falling and the block himself
	/// </summary>
	public void StartFalling()
	{
		//CheckNeighboursFalling();
		TellNeighboursToFall();
		Invoke("Fall" , 1.5f);
	}
	/// <summary>
	/// Sets falling true to indicate that the object should fall.
	/// </summary>
	private void Fall()
	{
		falling = true;
	}
	/// <summary>
	/// Stops the object and neighbours of falling and locks them inplace.
	/// </summary>
	public void StopFalling()
	{
		TellNeighboursToStopFalling();
		falling = false;
		rigid2D.isKinematic = true;
	}
	/// <summary>
	/// A check if the object should be falling 
	/// </summary>
	private IEnumerator FallingCheck ()
	{
		if (falling)
		{
			rigid2D.isKinematic = false;
		}
		else
		{
			rigid2D.isKinematic = true;
		}
		yield return new WaitForSeconds(0.2f);
		StartCoroutine(FallingCheck());
	}
	/// <summary>
	/// Execute collision checks 
	/// </summary>
	/// <param name="coll">coll is the collider against which the block collides</param>
	private void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.collider.name != "Player")
		{
			if (coll.collider.GetComponent<Block> ())
			{
				if (coll.collider.GetComponent<Block> ().falling == false)
				{
					if (rigid2D.velocity.y < -0.2f)
					{
						StopFalling ();
						//Invoke ("StopFalling", 0.1f);
					}
				}
			}
		}
		else
		{
			if (pickUp)
			{
				Destroy(gameObject);
				TellNeighboursToFall ();
				EventManager.TriggerEvent (StaticEventNames.GOTPICKUP);
			}
		}
	}
	/// <summary>
	/// Play destroy animation and after that destroying the tile
	/// </summary>
	/// <returns></returns>
	private IEnumerator DestroyTile ()
	{
		collider.enabled = false;
		for (int i = 0; i < destroyAnimation.Length; i++)
		{
			render.sprite = destroyAnimation[i];
			yield return new WaitForSeconds(0.01f);
			if (i == destroyAnimation.Length-1)
			{
				EventManager.TriggerEvent (StaticEventNames.MUNCHPARTICLE);
				EventManager.TriggerAudioSFXEvent (AudioClips.digSound);
				Destroy (gameObject);
			}
		}
	}
}
