using UnityEngine;
using System.Collections;

public class DeadCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.collider.GetComponent<Block>().falling)
		{
			EventManager.TriggerEvent(StaticEventNames.LOSTLIFE);
		}
	}
}
