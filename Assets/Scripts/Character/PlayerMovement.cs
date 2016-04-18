// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private BlockManager manager;
    private float movement = 0.04f;

    protected void Update()
    {
        transform.Translate(Mathf.Clamp(Input.GetAxis("Horizontal"), -movement, movement), 0, 0);
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
