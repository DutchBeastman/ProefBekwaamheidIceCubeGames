//Created By: Jeremy Bond
//Date: 07/04/2016

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	private float startX; 

	protected void Awake ()
	{
		startX = transform.position.x;
	}

	protected void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A))
		{
			if (MathUtils.difference (startX, transform.position.x) < 3 || transform.position.x > startX)
			{
				transform.position += Vector3.left;
			}
		}
		if(Input.GetKeyDown (KeyCode.D))
		{
			if (MathUtils.difference (startX, transform.position.x) < 3 || transform.position.x < startX)
			{
				transform.position += Vector3.right;
			}
		}
	}
}
