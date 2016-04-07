//Created By: Jeremy Bond
//Date: 29/03/2016

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Overlay : MonoBehaviour 
{
	private static Image overlay;

	protected void Awake () 
	{
		overlay = GetComponent<Image>(); 
		FadeOut();
	}

	public static void FadeIn ()
	{
		if(overlay != null)
		{
			overlay.CrossFadeAlpha (1, 1, false);
		}
		else
		{
			Debug.LogWarning ("No overlay image appointed for the fade in");
		}
	}

	public static void FadeOut ()
	{
		if (overlay != null)
		{
			overlay.CrossFadeAlpha (0, 1, false);
		}
		else
		{
			Debug.LogWarning("No overlay image appointed for the fade out");
		}
	}

}
