using UnityEngine;
using System.Collections;

public class DeadCollision : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		Block b = coll.GetComponent<Block>();
		if (b != null && b.falling && !b.pickUp)
		{
			EventManager.TriggerEvent(StaticEventNames.LOSTLIFE);
		}
	}
}
