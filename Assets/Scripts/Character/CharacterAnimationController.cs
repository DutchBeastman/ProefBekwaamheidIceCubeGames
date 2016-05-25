// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
	//DigAnimations
	[SerializeField] private string digLeftAnimation;
	[SerializeField] private string digRightAnimation;
	[SerializeField] private string digDownAnimation;
	//MoveAnimations
	[SerializeField] private string moveLeftAnimation;
	[SerializeField] private string moveRightAnimation;
	//FallingAnimations
	[SerializeField] private string startFallingAnimation;
	[SerializeField] private string fallingAnimation;
	[SerializeField] private string stopFallingAnimation;
	
	void Start()
	{

	}

	public void MoveLeft(bool digging)
	{
		if (digging)
		{
			
		}
		else
		{

		}
	}

	public void MoveRight(bool digging)
	{
		if (digging)
		{

		}
		else
		{

		}
	}

	public void DigDown()
	{

	}

	public void StartFall()
	{

	}

	public void ContinueFall()
	{

	}

	public void Land()
	{

	}

}
