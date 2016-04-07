//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;

public class TabToKill : MonoBehaviour 
{
	private void OnCollisionEnter2D (Collision2D col)
	{
		if(col.collider.name == "Character")
		{
			col.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			Destroy (gameObject);
			Debug.Log ("collided with player");
		}
	}
}
