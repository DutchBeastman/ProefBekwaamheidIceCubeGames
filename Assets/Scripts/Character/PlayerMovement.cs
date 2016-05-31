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
    [SerializeField]private BlockManager manager;
	[SerializeField]private float resetDigTime;

	private bool canDig;
	private bool canClimb = true;

	private Rigidbody2D rigid;
	private DrillDirection drillDir;
	private Vector2 playerPosition;
	private bool died = false;
	private bool gameOver = false;
	private bool canMove = true;
	private Vector3 originScale;

	protected void Awake()
	{
		playerPosition = new Vector2(0 , 15);
		rigid = GetComponent<Rigidbody2D>();
		canDig = true;
		originScale = transform.localScale;
	}

	protected void OnEnable ()
	{
		EventManager.AddListener (StaticEventNames.NEXTSTAGE, NextStage);
		EventManager.AddListener (StaticEventNames.RESTART, NextStage);
		EventManager.AddListener (StaticEventNames.ENDGAME, GameOver);
		EventManager.AddListener (StaticEventNames.LOSTLIFE, LostLife);
	}

	protected void OnDisable ()
	{
		EventManager.RemoveListener (StaticEventNames.NEXTSTAGE, NextStage);
		EventManager.RemoveListener (StaticEventNames.RESTART, NextStage);
		EventManager.RemoveListener (StaticEventNames.ENDGAME, GameOver);
		EventManager.RemoveListener (StaticEventNames.LOSTLIFE, LostLife);
	}

	protected void FixedUpdate()
	{
		Move();
		if (canClimb && !died)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + (Input.GetAxis("Horizontal") / 2), transform.position.y - 0.3f), new Vector2(Input.GetAxis("Horizontal") * 0.1f, 0), 0.1f);
			if (hit.collider != null && hit.collider.name != "Player")
			{
				if (Physics2D.Raycast(new Vector2(transform.position.x + (Input.GetAxis("Horizontal") / 2), transform.position.y + 1), new Vector2(Input.GetAxis("Horizontal"), 0), 0.1f).collider == null)
				{
					EnableKinematic();
					transform.localPosition += new Vector3(0 , transform.localScale.y * 1.1f , 0);
					DisableKinematic();
					canClimb = false;
					Invoke("ClimbTimer" , 1.5f);
				}
			}
		}
	}

    protected void Update()
    {
		//Getting the axis for spacebar, which in unity is called jump
		if (Input.GetButtonDown("Jump"))
        {
			//Raycasting to see if we hit an other Block collider
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, 0.1f);
			switch (drillDir)
			{
				case DrillDirection.up:
					hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + Vector2.up.y), Vector2.up, 0.1f);
					break;
				case DrillDirection.right:
					hit = Physics2D.Raycast(new Vector2(transform.position.x + Vector2.right.x, transform.position.y), Vector2.right, 0.1f);
					break;
				case DrillDirection.left:
					hit = Physics2D.Raycast(new Vector2(transform.position.x + Vector2.left.x, transform.position.y), Vector2.left, 0.1f);
					break;
			}
            if (hit.collider != null && hit.collider.GetComponent<Block>() && canDig && !died)
            {
				//Here we remove a block, and set the digging unavailiable and start the reset timer
				hit.collider.GetComponent<Block>().GetKilled();
				EventManager.TriggerAudioSFXEvent(AudioClips.digSound);
				canDig = false;
				Invoke("ResetDigTime" , 0.4f);
            }
        }
		if (Input.GetKeyDown(KeyCode.W))
		{
			drillDir = DrillDirection.up;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			drillDir = DrillDirection.down;
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			drillDir = DrillDirection.left;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			drillDir = DrillDirection.right;
		}
    }

	private void Move()
	{
		if (canMove && !died)
		{
			if (Mathf.RoundToInt(Input.GetAxis("Horizontal")) != 0)
			{

				Vector3 movement = new Vector3(Mathf.RoundToInt(Input.GetAxis ("Horizontal")), 0, 0);
				RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position + movement, movement,0.1f);

				bool move = true;

				if (hits != null)
				{
					foreach (RaycastHit2D ray in hits)
					{
						if (ray.collider.name == "WallLeft" || ray.collider.name == "WallRight")
						{
							if (ray.collider.name != gameObject.name && ray.collider.name != "LifeTile(Clone)")
							{
								move = false;
							}
						}
					}
				}

				if (move)
				{
					playerPosition = new Vector2 (transform.position.x + Mathf.RoundToInt (Input.GetAxis ("Horizontal")), transform.position.y);
					transform.position = playerPosition;
					canMove = false;
					Invoke ("ResetMovementTimer", 0.25f);
				}
			}
		}
	}

	/// <summary>
	/// Is in place for the dig funtion to be able to execute after a certain time.
	/// </summary>
	private void ResetDigTime()
	{
		canDig = true;
	}
	private void ResetMovementTimer()
	{
		canMove = true;
	}
	protected void ClimbTimer()
	{
		canClimb = true;
	}

	private void NextStage ()
	{
		transform.position = new Vector3(transform.position.x, 10, 0);
		rigid.isKinematic = true;
		Invoke("DisableKinematic", 2);
		died = false;
	}

	private void GameOver ()
	{
		gameOver = true;
		DisableMovement();
	}

	private void DisableMovement ()
	{
		died = true;
	}
	
	private void EnableMovement ()
	{
		if (!gameOver)
		{
			died = false;
			transform.GetComponent<CircleCollider2D>().isTrigger = false;
			DisableKinematic ();
			gameObject.transform.localScale = originScale;
		}
	}

	private void LostLife ()
	{
		DisableMovement ();
		EventManager.TriggerAudioSFXEvent(AudioClips.lostLifeSound);
		StartCoroutine(ShrinkPlayer());
		transform.GetComponent<CircleCollider2D> ().isTrigger = true;
		Invoke("EnableKinematic", 0.2f);
		KillAllBlocksAbove ();
		Invoke ("EnableMovement", 4f);
	}

	private void KillAllBlocksAbove ()
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + Vector3.up, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
		hits = Physics2D.RaycastAll (transform.position + Vector3.left, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
		hits = Physics2D.RaycastAll (transform.position + Vector3.right, transform.position + Vector3.up * 100);
		EmptyHitsArray (hits);
	}

	private void EmptyHitsArray (RaycastHit2D[] hits)
	{
		foreach (RaycastHit2D hit in hits)
		{
			Destroy (hit.collider.gameObject);
		}
	}

	IEnumerator ShrinkPlayer ()
	{
		for (float y = 1; y > 0.3f; y -= 0.1f)
		{
			transform.localScale = new Vector3(originScale.x, y, 0);
			yield return new WaitForSeconds(0.03f);
		}
	}

	private void DisableKinematic ()
	{
		rigid.isKinematic = false;
	}
	private void EnableKinematic()
	{
		rigid.isKinematic = true;
	}
}
