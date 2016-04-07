//Created By: Jeremy Bond
//Date: 25/03/2016

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Utils;

public class EventManager : MonoBehaviour
{

	private Dictionary<string, UnityEvent> eventDictionary;
	private Dictionary<string, AudioSFXEvent> audioSFXEventDictionary;
	private Dictionary<string, AudioMusicEvent> audioMusicEventDictionary;

	private static EventManager eventManager;
	private const string AUDIOEVENT = "audioEvent";

	public static EventManager instance
	{
		get
		{
			if (!eventManager)
			{
				eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;

				if (!eventManager)
				{
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				}
				else
				{
					eventManager.Init ();
				}
			}

			return eventManager;
		}
	}

	private void Init ()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<string, UnityEvent> ();
		}
		if (audioSFXEventDictionary == null)
		{
			audioSFXEventDictionary = new Dictionary<string, AudioSFXEvent> ();
		}
		if (audioMusicEventDictionary == null)
		{
			audioMusicEventDictionary = new Dictionary<string, AudioMusicEvent> ();
		}
	}

	public static void AddListener (string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		}
		else
		{
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void AddAudioSFXListener (UnityAction<AudioClip> listener)
	{
		AudioSFXEvent thisEvent = null;
		if (instance.audioSFXEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.AddListener (listener);
		}
		else
		{
			thisEvent = new AudioSFXEvent ();
			thisEvent.AddListener (listener);
			instance.audioSFXEventDictionary.Add (AUDIOEVENT, thisEvent);
		}
	}

	public static void AddAudioMusicListener (UnityAction<AudioClip> listener)
	{
		AudioMusicEvent thisEvent = null;
		if (instance.audioMusicEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.AddListener (listener);
		}
		else
		{
			thisEvent = new AudioMusicEvent ();
			thisEvent.AddListener (listener);
			instance.audioMusicEventDictionary.Add (AUDIOEVENT, thisEvent);
		}
	}

	public static void RemoveListener (string eventName, UnityAction listener)
	{
		if (eventManager == null)
		{
			return;
		}
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void RemoveAudioSFXListener (UnityAction<AudioClip> listener)
	{
		if (eventManager == null)
		{
			return;
		}
		AudioSFXEvent thisEvent = null;
		if (instance.audioSFXEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void RemoveAudioMusicListener (UnityAction<AudioClip> listener)
	{
		if (eventManager == null)
		{
			return;
		}
		AudioMusicEvent thisEvent = null;
		if (instance.audioMusicEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}

	public static void TriggerAudioSFXEvent (AudioClip clip)
	{
		AudioSFXEvent thisEvent = null;
		if (instance.audioSFXEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.Invoke (clip);
		}
	}

	public static void TriggerAudioMusicEvent (AudioClip clip)
	{
		AudioMusicEvent thisEvent = null;
		if (instance.audioMusicEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.Invoke (clip);
		}
	}
}