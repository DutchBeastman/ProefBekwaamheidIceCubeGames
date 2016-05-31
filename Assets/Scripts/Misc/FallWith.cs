//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;

public class FallWith : MonoBehaviour 
{
	[SerializeField] private GameObject objectToFallWith;
	/// <summary>
	/// updates the object(Camera) to fall with a certain gameobject, in our case this controls the camera to follow the player only on the Y-axis
	/// </summary>
	protected void Update () 
	{
		transform.position = new Vector3(transform.position.x, objectToFallWith.transform.position.y, 0);
	}
}
