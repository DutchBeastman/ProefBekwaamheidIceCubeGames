//Created By: Jeremy Bond
//Date: 27/03/2016

using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

namespace Utils
{
	public class AudioPlayer : MonoBehaviour
	{
		[SerializeField] private int maxChannels;
		private HashSet<AudioChannel> channels;
		[SerializeField] private AudioMixerGroup SFXGroup;
		[SerializeField] private AudioMixerGroup musicGroup;
		
		/// <summary>
		/// Creates al audio channels
		/// </summary>
		protected void Awake ()
		{
			channels = new HashSet<AudioChannel> ();
			for (int i = 0; i < maxChannels; i++)
			{
				GameObject channel = new GameObject ("channel" + i);
				channel.transform.SetParent (transform);

				channel.AddComponent<AudioSource> ();
				channels.Add (channel.AddComponent<AudioChannel> ());
			}
		}
		/// <summary>
		/// Gets free audio channels
		/// </summary>
		/// <returns>Returns a channel</returns>
		private AudioChannel GetFreeChannel ()
		{
			foreach (AudioChannel channel in channels)
			{
				if (!channel.IsPlaying)
				{
					return channel;
				}
			}
			return null;
		}
		/// <summary>
		/// OnEnable adds all Audio listeners
		/// </summary>
		protected void OnEnable ()
		{
			EventManager.AddAudioSFXListener (PlaySFXAudio);
			EventManager.AddAudioMusicListener (PlayMusicAudio);
		}
		/// <summary>
		/// OnDisable removes all listeners
		/// </summary>
		protected void OnDisable ()
		{
			EventManager.RemoveAudioSFXListener (PlaySFXAudio);
			EventManager.RemoveAudioMusicListener (PlayMusicAudio);
		}
		/// <summary>
		/// Plays an audio clip through the sfx channel
		/// </summary>
		/// <param name="clip">the specific clip of audio you want to play</param>
		private void PlaySFXAudio (AudioClip clip)
		{
			PlayAudio(clip, SFXGroup);
		}

		/// <summary>
		/// Plays an audio clip through the music channel
		/// </summary>
		/// <param name="clip">the specific clip of audio you want to play</param>
		private void PlayMusicAudio(AudioClip clip)
		{
			PlayAudio(clip, musicGroup);
		}
		/// <summary>
		/// Executes the given audio from other play audio functions.
		/// </summary>
		/// <param name="clip">The given audio clip to play</param>
		/// <param name="group">the given group to output the sound to</param>
		private void PlayAudio(AudioClip clip, AudioMixerGroup group)
		{
			AudioChannel channel = GetFreeChannel ();

			if (channel != null)
			{
				if (group == musicGroup)
				{
					channel.Loop = true;
				}
				channel.Play (clip, group);
			}
			else
			{
				Debug.LogWarning ("No free AudioChannels");
			}
		}
	}
}