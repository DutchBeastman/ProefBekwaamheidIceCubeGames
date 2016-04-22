// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private BlockManager manager;
	[SerializeField]private float resetDigTime;
	private bool canDig;
	private Rigidbody2D rigid;

	protected void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
		canDig = true;
	}
	protected void FixedUpdate()
	{
		rigid.AddRelativeForce(new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") * Time.deltaTime * 300,-6,6), 0) , ForceMode2D.Force);
		rigid.velocity = new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") * Time.deltaTime * 300 , -6 , 6) , 0);
		Debug.Log(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * 300, 0));
	}
    protected void Update()
    {
		//Getting the axis for spacebar, which in unity is called jump
		if (Input.GetButtonDown("Jump"))
        {
			//Raycasting to see if we hit an other Block collider
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, 0.1f);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, Color.red);
            if (hit.collider != null && hit.collider.GetComponent<Block>() && canDig)
            {
				//Here we remove a block, and set the digging unavailiable and start the reset timer
                Debug.Log("colliders name" + hit.collider.name);
                manager.KillTile(hit.collider.GetComponent<Block>());
				canDig = false;
				Invoke("ResetDigTime" , 0.7f);
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
}
