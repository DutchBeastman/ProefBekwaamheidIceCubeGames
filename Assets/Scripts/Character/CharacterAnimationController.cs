// Created by: Jeremy Bond
// Date: 15/04/2016

using UnityEngine;
using System.Collections;
using Spine.Unity;

public class CharacterAnimationController : MonoBehaviour
{
	//DigAnimations
	[SpineAnimation, SerializeField] private string digLeftAnimation;
	[SpineAnimation, SerializeField] private string digRightAnimation;
	[SpineAnimation, SerializeField] private string digDownAnimation;
	//MoveAnimations
	[SpineAnimation, SerializeField] private string moveLeftAnimation;
	[SpineAnimation, SerializeField] private string moveRightAnimation;
	//FallingAnimations
	[SpineAnimation, SerializeField] private string startFallingAnimation;
	[SpineAnimation, SerializeField] private string fallingAnimation;
	[SpineAnimation, SerializeField] private string stopFallingAnimation;

	private SkeletonAnimation skeletonAnimation;

	private Spine.AnimationState spineAnimationState;
	private Spine.Skeleton skeleton;


	void Start()
	{
		skeletonAnimation = GetComponent<SkeletonAnimation>();
		spineAnimationState = skeletonAnimation.state;
		skeleton = skeletonAnimation.skeleton;
	}

	public void MoveLeft(bool digging)
	{
		if (digging)
		{
			spineAnimationState.SetAnimation(0, digLeftAnimation, false);
		}
		else
		{
			spineAnimationState.SetAnimation(0, moveLeftAnimation, false);
		}
	}

	public void MoveRight(bool digging)
	{
		if (digging)
		{
			spineAnimationState.SetAnimation(0, digRightAnimation, false);
		}
		else
		{
			spineAnimationState.SetAnimation(0, moveRightAnimation, false);
		}
	}

	public void DigDown()
	{
		spineAnimationState.SetAnimation(0, digDownAnimation, false);
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
