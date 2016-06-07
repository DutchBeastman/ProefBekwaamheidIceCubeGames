//Created By: Jeremy Bond
//Date: 29/03/2016

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

enum LastChangedAudioMixer
{
	Master,
	SFX,
	Music
}

public class Settings : MonoBehaviour
{
	[SerializeField] private AudioMixer mixer;
	[SerializeField] private Slider masterVolume;
	[SerializeField] private Slider SFXVolume;
	[SerializeField] private Slider musicVolume;

	[SerializeField] private AudioClip volumeChangedSound;

	private bool afterSoundPlayed = true;
	private float lastMasterVolume;
	private float lastSFXVolume;
	private float lastMusicVolume;
	private float startingVolume = 0.5f;
	
	private LastChangedAudioMixer lastChangedMixer;
	/// <summary>
	/// In this awake the starting volume values are set
	/// </summary>
	protected void Awake ()
	{
		masterVolume.value = startingVolume;
		SFXVolume.value = startingVolume;
		musicVolume.value = startingVolume;
	}
	/// <summary>
	/// In the update all values are adjusted according to the correct sliders in the settings menu
	/// </summary>
	protected void Update ()
	{
		if (lastMasterVolume != masterVolume.value)
		{
			lastMasterVolume = masterVolume.value; 
			ChangeMixerVolume (masterVolume.value, "MixerMasterVolume", LastChangedAudioMixer.Master);
			ChangeMixerVolume ((masterVolume.value * SFXVolume.value), "MixerSFXVolume", LastChangedAudioMixer.SFX);
			ChangeMixerVolume ((masterVolume.value * musicVolume.value), "MixerMusicVolume", LastChangedAudioMixer.Music);
		}
		else if(lastSFXVolume != SFXVolume.value)
		{
			lastSFXVolume = SFXVolume.value;
			ChangeMixerVolume ((masterVolume.value * SFXVolume.value), "MixerSFXVolume", LastChangedAudioMixer.SFX);
		}
		else if (lastMusicVolume != musicVolume.value)
		{
			lastMusicVolume = musicVolume.value;
			ChangeMixerVolume((masterVolume.value * musicVolume.value), "MixerMusicVolume", LastChangedAudioMixer.Music);
		}
		
		if (!afterSoundPlayed && Input.GetMouseButtonUp(0))
		{
			afterSoundPlayed = true;
			switch (lastChangedMixer)
			{
				case LastChangedAudioMixer.Master:
					EventManager.TriggerAudioSFXEvent (volumeChangedSound);
				break;
				case LastChangedAudioMixer.SFX:
					EventManager.TriggerAudioSFXEvent (volumeChangedSound);
				break;
			}
		}
	}
	/// <summary>
	/// This function changes the Volume of given mixer
	/// </summary>
	/// <param name="mixerVolume">the number of what the volume should become</param>
	/// <param name="mixerName">the name of the mixer</param>
	/// <param name="lastChangedAudioMixer"></param>
	private void ChangeMixerVolume (float mixerVolume, string mixerName, LastChangedAudioMixer lastChangedAudioMixer)
	{
		mixer.SetFloat (mixerName, (-40 + mixerVolume * 40));
		Debug.Log(mixerVolume);
		lastChangedMixer = lastChangedAudioMixer;
		afterSoundPlayed = false;
	}
}
