//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;

public class FallWith : MonoBehaviour 
{
	[SerializeField] private GameObject objectToFallWith;

	protected void Update () 
	{
		transform.position = new Vector3(transform.position.x, objectToFallWith.transform.position.y, 0);
	}
}
