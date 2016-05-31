using UnityEngine;
using System.Collections;

public class DeadCollision : MonoBehaviour {

	private bool canLoseLife = true;

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
	private void EnableLosingLife ()
	{
		canLoseLife = true;
	}
}
