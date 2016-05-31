//Created By: Jeremy Bond
//Date: 27/03/2016

using UnityEngine;
using UnityEngine.Audio;

namespace Utils
{
	public class AudioChannel : MonoBehaviour
	{
		/// <summary>
		/// Getter and setter for Volume. 
		/// </summary>
		public float Volume
		{
			set
			{
				audioSource.volume = Mathf.Clamp01 (value);
			}
			get
			{
				return audioSource.volume;
			}
		}
		/// <summary>
		/// Getter and setter for Pitch. 
		/// </summary>
		public float Pitch
		{
			set
			{
				audioSource.pitch = Mathf.Clamp01 (value);
			}
			get
			{
				return audioSource.pitch;
			}
		}
		/// <summary>
		/// Getter and setter for StereoPan
		/// </summary>
		public float StereoPan
		{
			set
			{
				audioSource.panStereo = Mathf.Clamp01 (value);
			}
			get
			{
				return audioSource.panStereo;
			}
		}
		/// <summary>
		/// Getter and setter for spatialBlend
		/// </summary>
		public float SpatialBlend
		{
			set
			{
				audioSource.spatialBlend = Mathf.Clamp01 (value);
			}
			get
			{
				return audioSource.spatialBlend;
			}
		}
		/// <summary>
		/// Getter and setter for ReverbZoneMix
		/// </summary>
		public float ReverbZoneMix
		{
			set
			{
				audioSource.reverbZoneMix = Mathf.Clamp (value, 0, 1.1f);
			}
			get
			{
				return audioSource.reverbZoneMix;
			}
		}
		/// <summary>
		/// Getter and setter for mute
		/// </summary>
		public bool Mute
		{
			set
			{
				audioSource.mute = value;
			}
			get
			{
				return audioSource.mute;
			}
		}
		/// <summary>
		/// Getter and setter for loop
		/// </summary>
		public bool Loop
		{
			set
			{
				audioSource.loop = value;
			}
			get
			{
				return audioSource.loop;
			}
		}
		/// <summary>
		/// Getter for if the audiosource is playing
		/// </summary>
		public bool IsPlaying
		{
			get
			{
				return audioSource.isPlaying && !paused;
			}
		}

		private AudioSource audioSource;
		private bool paused;
		/// <summary>
		/// In Awake AudioSource is set and the play on awake set false
		/// </summary>
		protected void Awake ()
		{
			audioSource = GetComponent<AudioSource> ();
			audioSource.playOnAwake = false;
		}
		/// <summary>
		/// The play function plays an audio clip into and audio group
		/// </summary>
		/// <param name="audioObject">The audio object(AudioClip) that you want to play</param>
		/// <param name="group">the group that you want to output the sound to</param>
		internal void Play (AudioClip audioObject, AudioMixerGroup group)
		{
			audioSource.clip = audioObject;
			audioSource.outputAudioMixerGroup = group;
			audioSource.Play ();
		}
		/// <summary>
		/// In Pause the sound can be paused
		/// </summary>
		public void Pause ()
		{
			audioSource.Pause ();
			paused = true;
		}
		/// <summary>
		/// In UnPause the sound can be unpaused
		/// </summary>
		public void UnPause ()
		{
			audioSource.UnPause ();
			paused = false;
		}
		/// <summary>
		/// In Stop the audioSource gets stopped
		/// </summary>
		public void Stop ()
		{
			audioSource.Stop ();
		}
	}


}