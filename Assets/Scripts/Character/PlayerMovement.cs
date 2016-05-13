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
	private Rigidbody2D rigid;
	private DrillDirection drillDir;
	private bool canClimb = true;
	private Vector2 playerPosition;

	protected void Awake()
	{
		playerPosition = new Vector2(0 , 10);
		rigid = GetComponent<Rigidbody2D>();
		canDig = true;
	}
	protected void FixedUpdate()
	{
		playerPosition = new Vector2(Input.GetAxisRaw("Horizontal") , transform.position.y);
		transform.position = playerPosition;
		rigid.isKinematic = true;
		/* Old movement
		rigid.AddRelativeForce(new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") * Time.deltaTime * 300 , -6 , 6) , 0) , ForceMode2D.Force);
		rigid.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") * Time.deltaTime * 300 , -6 , 6) , 0);
		*/

		if (canClimb)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + (Input.GetAxis("Horizontal") / 2), transform.position.y - 0.3f), new Vector2(Input.GetAxis("Horizontal"), 0), 0.1f);
			if (hit.collider != null && hit.collider.name != "Player")
			{
				if (Physics2D.Raycast(new Vector2(transform.position.x + (Input.GetAxis("Horizontal") / 2), transform.position.y + 1), new Vector2(Input.GetAxis("Horizontal"), 0), 0.1f).collider == null)
				{
					rigid.velocity = new Vector2(0, 90);
					canClimb = false;
					StartCoroutine(ClimbTimer());
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
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, Color.red);
            if (hit.collider != null && hit.collider.GetComponent<Block>() && canDig)
            {
				//Here we remove a block, and set the digging unavailiable and start the reset timer
				hit.collider.GetComponent<Block>().GetKilled();
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
	/// <summary>
	/// Is in place for the dig funtion to be able to execute after a certain time.
	/// </summary>
	private void ResetDigTime()
	{
		canDig = true;
	}

	protected IEnumerator ClimbTimer()
	{
		yield return new WaitForSeconds(0.7f);
		canClimb = true;
	}
}
