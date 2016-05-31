//Created By: Jeremy Bond
//Date: 

using UnityEngine;

public static class MathUtils 
{
	/// <summary>
	/// Calculates the difference between float a and float b
	/// </summary>
	/// <param name="a">the first value to compare </param>
	/// <param name="b">the second value to compare to</param>
	/// <returns></returns>
	public static float Difference (float a, float b)
	{
		return Mathf.Abs (a - b);
	}
}
