using UnityEngine;
using System.Collections;

public class DeadCollision : MonoBehaviour {

	private bool canLoseLife = true;
	/// <summary>
	/// OnTrigger in this instance makes the player lose a life
	/// </summary>
	/// <param name="coll">coll is the collider which the object collides with</param>
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (canLoseLife)
		{
			Block b = coll.GetComponent<Block>();
			if (b != null && b.falling && !b.pickUp)
			{
				canLoseLife = false;
				EventManager.TriggerEvent(StaticEventNames.LOSTLIFE);
				Invoke("EnableLosingLife", 4f);
			}
		}
	}
	/// <summary>
	/// Enables if you can lose a life
	/// </summary>
	private void EnableLosingLife ()
	{
		canLoseLife = true;
	}
}
