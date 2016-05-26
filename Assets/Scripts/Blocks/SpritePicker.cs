// Created by: Jeremy Bond
// Date: 26/05/2016

using UnityEngine;

public class SpritePicker : MonoBehaviour 
{
	[SerializeField] private Sprite[] sprites;

	protected void Awake () 
	{
		GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
	}
}
