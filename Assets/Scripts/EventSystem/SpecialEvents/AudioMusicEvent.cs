//Created By: Jeremy Bond
//Date: 27/03/2016

using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
	/// <summary>
	/// AudioMusic event derives from unity audio clip so we can use this in our Event system to play music audio clips. 
	/// </summary>
	public class AudioMusicEvent : UnityEvent<AudioClip>
	{

	}
}