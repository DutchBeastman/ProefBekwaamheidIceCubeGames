// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
	private SpriteRenderer sprRend;

	[SerializeField] private Sprite[] idle;
	//DigAnimations
	[SerializeField] private string digLeftAnimation;
	[SerializeField] private string digRightAnimation;
	[SerializeField] private string digDownAnimation;
	//MoveAnimations
	[SerializeField] private string moveLeftAnimation;
	[SerializeField] private string moveRightAnimation;
	//FallingAnimations
	[SerializeField] private Sprite[] fallSprites;
	[SerializeField] private Sprite[] startFallingSprites;
	[SerializeField] private Sprite[] stopFallingSprite;

	[SerializeField] private Sprite playerSprite;

	void Start()
	{
		playerSprite = idle[0];
		sprRend = GetComponent<SpriteRenderer>();
	}

	private void AnimationState()
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
