// Created by: Jeremy Bond
// Date: 10/06/2016

using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour 
{
	[SerializeField] private Sprite normalImage;
	[SerializeField] private Sprite[] eatAnimation;
	private SpriteRenderer render;

	/// <summary>
	/// Gives empty variables values.
	/// </summary>
	protected void Awake ()
	{
		render = GetComponent<SpriteRenderer> ();
	}
	/// <summary>
	/// Adds listeners.
	/// </summary>
	protected void OnEnable ()
	{
		EventManager.AddListener("PlayEatAnim", EatAnimTriggered);
	}
	/// <summary>
	/// Removes listeners.
	/// </summary>
	protected void OnDisable ()
	{
		EventManager.RemoveListener ("PlayEatAnim", EatAnimTriggered);
	}
	/// <summary>
	/// Function triggered when eat anim is called.
	/// </summary>
	private void EatAnimTriggered ()
	{
		StartCoroutine(PlayAnimation(eatAnimation));
	}
	/// <summary>
	/// Play destroy animation and after that destroying the tile
	/// </summary>
	/// <returns></returns>
	private IEnumerator PlayAnimation (Sprite[] animation)
	{
		for (int i = 0; i < animation.Length; i++)
		{
			render.sprite = animation[i];
			yield return new WaitForSeconds (0.01f);
			if (i == animation.Length - 1)
			{
				render.sprite = normalImage;
			}
		}
	}
}
