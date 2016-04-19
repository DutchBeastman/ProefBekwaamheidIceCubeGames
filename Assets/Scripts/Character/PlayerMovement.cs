// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private BlockManager manager;
	private Rigidbody2D rigid;
    private float movement = 0.04f;

	protected void Awake()
	{
		rigid = GetComponent<Rigidbody2D>();
	}
    protected void Update()
    {
		rigid.AddRelativeForce(new Vector2(Input.GetAxis("Horizontal"), 0) * 25,ForceMode2D.Force);
		rigid.velocity = Vector2.zero;
        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, 0.1f);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + Vector2.down.y), Vector2.down, Color.red);
            if (hit.collider != null && hit.collider.GetComponent<Block>())
            {
                Debug.Log("colliders name" + hit.collider.name);
                manager.KillTile(hit.collider.GetComponent<Block>());
            }
        }
    }
}
