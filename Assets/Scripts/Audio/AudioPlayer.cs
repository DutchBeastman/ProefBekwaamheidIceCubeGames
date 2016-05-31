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

		private const string AUDIOEVENT = "audioEvent";

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

		private AudioChannel GetFreeChannel ()
		{
			foreach (AudioChannel channel in channels)
			{
				if (!channel.IsPlaying)
				{
					Debug.Log ("returning empty channel");
					return channel;
				}
			}
			return null;
		}

		protected void OnEnable ()
		{
			EventManager.AddAudioSFXListener (PlaySFXAudio);
			EventManager.AddAudioMusicListener (PlayMusicAudio);
		}

		protected void OnDisable ()
		{
			EventManager.RemoveAudioSFXListener (PlaySFXAudio);
			EventManager.RemoveAudioMusicListener (PlayMusicAudio);
		}

		private void PlaySFXAudio (AudioClip clip)
		{
			PlayAudio(clip, SFXGroup);
		}

		private void PlayMusicAudio(AudioClip clip)
		{
			PlayAudio(clip, musicGroup);
		}

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