// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;

public enum DrillDirection
{
	down = 0, right, left, up
}

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private BlockManager manager;
	[SerializeField] private float resetDigTime;

	private bool canDig;
	private bool canClimb = true;

	private Rigidbody2D rigid;
	private DrillDirection drillDir;
	private Vector2 playerPosition;
	private bool died = false;
	private bool gameOver = false;
	private bool canMove = true;
	private Vector3 originScale;
	private bool inGameTutorialMove = true;
	private bool inGameTutorialDig = true;
	/// <summary>
	/// The awake sets the playerposition, gets the rigidbody and sets the scale.
	/// </summary>
	protected void Awake ()
	{
		playerPosition = new Vector2 (0, 15);
		rigid = GetComponent<Rigidbody2D> ();
		canDig = true;
		originScale = transform.localScale;
	}
	/// <summary>
	/// OnEnable adds all listeners 
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.NEXTSTAGE, NextStage);
		EventManager.AddListener (StaticEventNames.RESTART, Restart);
		EventManager.AddListener (StaticEventNames.ENDGAME, GameOver);
		EventManager.AddListener (StaticEventNames.LOSTLIFE, LostLife);
	}
	/// <summary>
	/// OnDisable removes all listeners
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.NEXTSTAGE, NextStage);
		EventManager.RemoveListener (StaticEventNames.RESTART, Restart);
		EventManager.RemoveListener (StaticEventNames.ENDGAME, GameOver);
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, LostLife);
	}
	/// <summary>
	/// Regulates all movement and checks for the movement.
	/// </summary>
	protected void Update ()
	{
		Move ();

		//Getting the axis for spacebar, which in unity is called jump
		if (Input.GetButtonDown ("Jump"))
		{
			if (!inGameTutorialMove && inGameTutorialDig)
			{
				inGameTutorialDig = false;
				EventManager.TriggerEvent (StaticEventNames.TUTORIALDIGSTEP);
			}
			//Raycasting to see if we hit an other Block collider
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, 0.1f);
			switch (drillDir)
			{
				case DrillDirection.up:
				hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y + Vector2.up.y), Vector2.up, 0.1f);
				break;
				case DrillDirection.right:
				hit = Physics2D.Raycast (new Vector2 (transform.position.x + Vector2.right.x, transform.position.y), Vector2.right, 0.1f);
				break;
				case DrillDirection.left:
				hit = Physics2D.Raycast (new Vector2 (transform.position.x + Vector2.left.x, transform.position.y), Vector2.left, 0.1f);
				break;
			}
			if (hit.collider != null && hit.collider.GetComponent<Block> () && canDig && !died)
			{
				//Here we remove a block, and set the digging unavailiable and start the reset timer
				hit.collider.GetComponent<Block> ().GetKilled ();
				EventManager.TriggerEvent(StaticEventNames.MUNCHPARTICLE);
				EventManager.TriggerAudioSFXEvent (AudioClips.digSound);
				canDig = false;
				Invoke ("ResetDigTime", 0.4f);
			}
		}
		if (!died)
		{
			if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow))
			{
				drillDir = DrillDirection.up;
				RotatePlayer (180);
			}
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow))
			{
				drillDir = DrillDirection.down;
				RotatePlayer (0);
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow))
			{
				drillDir = DrillDirection.left;
				RotatePlayer (270);
			}
			if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow))
			{
				drillDir = DrillDirection.right;
				RotatePlayer (90);
			}
		}
	}
	/// <summary>
	/// Rotate the player at given rotation.
	/// </summary>
	/// <param name="rotation">given rotation in degrees</param>
	private void RotatePlayer (int rotation)
	{
		transform.eulerAngles = new Vector3 (0, 0, rotation);
	}
	/// <summary>
	/// Move contains the snapping movement of the player and checks if you can move to that position
	/// </summary>
	private void Move ()
	{
		if (canMove && !died)
		{
			if (Mathf.RoundToInt (Input.GetAxis ("Horizontal")) != 0)
			{
				if (inGameTutorialMove)
				{
					inGameTutorialMove = false;
					EventManager.TriggerEvent (StaticEventNames.TUTORIALMOVEMENTSTEP);
				}

				Vector3 movement = new Vector3 (Mathf.RoundToInt (Input.GetAxis ("Horizontal")), 0, 0);
				RaycastHit2D[] hits1 = Physics2D.RaycastAll (transform.position + movement, movement, 0.1f);
				RaycastHit2D[] hits2 = Physics2D.RaycastAll (transform.position + movement + (Vector3.up / 2), movement, 0.1f);
				bool move = true;

				if (hits1 != null || hits2 != null)
				{
					move = CheckRaycastsOnFreeSpace (hits1);
					if (move)
					{
						move = CheckRaycastsOnFreeSpace (hits2);
					}
				}

				if (move)
				{
					playerPosition = new Vector2 (transform.position.x + Mathf.RoundToInt (Input.GetAxis ("Horizontal")), transform.position.y);
					transform.position = playerPosition;
					canMove = false;
					Invoke ("ResetMovementTimer", 0.25f);
				}
				else
				{
					Invoke ("CheckForClimbing", 0.3f);
				}
			}
		}
	}
	/// <summary>
	/// Check all given RaycastHits for empty space.
	/// </summary>
	/// <param name="hits">Given RaycastHits array.</param>
	/// <returns>Return true if there is an emtpy space, otherwise returns false</returns>
	private bool CheckRaycastsOnFreeSpace (RaycastHit2D[] hits)
	{
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.name != gameObject.name && hit.collider.name != "LifeTile(Clone)")
			{
				return false;
			}
		}
		return true;
	}
	/// <summary>
	/// Check for climbing up a tile.
	/// </summary>
	private void CheckForClimbing ()
	{
		if (canClimb && !died)
		{
			RaycastHit2D hit = Physics2D.Raycast (new Vector2 (transform.position.x + (Input.GetAxis ("Horizontal") / 2), transform.position.y - 0.3f), new Vector2 (Input.GetAxis ("Horizontal") * 0.1f, 0), 0.1f);
			if (hit.collider != null && hit.collider.name != "Player")
			{
				RaycastHit2D hit2 = Physics2D.Raycast (new Vector2 (transform.position.x + (Input.GetAxis ("Horizontal") / 2), transform.position.y + 1), new Vector2 (Input.GetAxis ("Horizontal"), 0), 0.1f);
				if (hit2.collider == null || hit2.collider.name == "LifeTile(Clone)")
				{
					EnableKinematic ();
					transform.localPosition += new Vector3 (0, transform.localScale.y * 1.1f, 0);
					DisableKinematic ();
					canClimb = false;
					Invoke ("ClimbTimer", 1.5f);
				}
			}
		}
	}
	/// <summary>
	/// Resets the dig boolean, this function gets invoked somewhere to create an delay
	/// </summary>
	private void ResetDigTime ()
	{
		canDig = true;
	}
	/// <summary>
	/// Resets the movement boolean, this function gets invoked somewhere to create an delay
	/// </summary>
	private void ResetMovementTimer ()
	{
		canMove = true;
	}
	/// <summary>
	/// Resets the climb boolean, this function gets invoked somewhere to create an delay
	/// </summary>
	protected void ClimbTimer ()
	{
		canClimb = true;
	}
	/// <summary>
	/// Resets the player position and value's
	/// </summary>
	private void Restart ()
	{
		gameOver = false;
		EnableMovement ();
		NextStage ();
	}
	/// <summary>
	/// Sets the player position and locks him there for 2 seconds
	/// </summary>
	private void NextStage ()
	{
		transform.position = new Vector3 (transform.position.x, 10, 0);
		rigid.isKinematic = true;
		Invoke ("DisableKinematic", 2);
		died = false;
	}
	/// <summary>
	/// GameOver disables movement when the player goes games over
	/// </summary>
	private void GameOver ()
	{
		gameOver = true;
		DisableMovement ();
	}
	/// <summary>
	/// Disables the movement of the player
	/// </summary>
	private void DisableMovement ()
	{
		died = true;
	}
	/// <summary>
	/// Enables the movement of the player
	/// </summary>
	private void EnableMovement ()
	{
		if (!gameOver)
		{
			died = false;
			transform.GetComponent<CircleCollider2D> ().isTrigger = false;
			DisableKinematic ();
			gameObject.transform.localScale = originScale;
		}
	}
	/// <summary>
	/// Indicates if the player lost an life and disables his movement.
	/// </summary>
	private void LostLife ()
	{
		DisableMovement ();

		drillDir = DrillDirection.down;
		RotatePlayer(0);

		EventManager.TriggerAudioSFXEvent (AudioClips.lostLifeSound);
		StartCoroutine (ShrinkPlayer ());

		transform.GetComponent<CircleCollider2D> ().isTrigger = true;
		Invoke ("EnableKinematic", 0.2f);

		KillAllBlocksAbove ();

		Invoke ("EnableMovement", 4f);
	}
	/// <summary>
	/// Kills all blocks above the player
	/// </summary>
	private void KillAllBlocksAbove ()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position + Vector3.up, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
		hits = Physics2D.RaycastAll (transform.position + Vector3.left, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
		hits = Physics2D.RaycastAll (transform.position + Vector3.right, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
	}
	/// <summary>
	/// Empties the array of raycast hits 
	/// </summary>
	/// <param name="hits">the given RaycastHit2D array</param>
	private void EmptyHitsArray (RaycastHit2D[] hits)
	{
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.name != "LifeTile(Clone)")
			{
				Destroy (hit.collider.gameObject);
			}
		}
	}
	/// <summary>
	/// Shrinks the player after he dies.
	/// </summary>
	/// <returns>returns a WaitForSeconds</returns>
	IEnumerator ShrinkPlayer ()
	{
		for (float y = 1; y > 0.3f; y -= 0.1f)
		{
			transform.localScale = new Vector3 (originScale.x, y, 0);
			yield return new WaitForSeconds (0.03f);
		}
	}
	/// <summary>
	/// Disables Kinematic in the rigidbody
	/// </summary>
	private void DisableKinematic ()
	{
		rigid.isKinematic = false;
	}
	/// <summary>
	/// Enables Kinematic in the rigidbody
	/// </summary>
	private void EnableKinematic ()
	{
		rigid.isKinematic = true;
	}
}
