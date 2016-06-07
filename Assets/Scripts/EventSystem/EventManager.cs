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
	private Dictionary<string, ScoreEvent> scoreEventDictionary;

	private static EventManager eventManager;
	private const string AUDIOEVENT = "audioEvent";
	private const string SCOREEVENT = "scoreEvent";
	/// <summary>
	/// Sets the event manager
	/// </summary>
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
	/// <summary>
	///Initialized the eventmanager 
	/// </summary>
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
		if (scoreEventDictionary == null)
		{
			scoreEventDictionary= new Dictionary<string, ScoreEvent> ();
		}
	}
	/// <summary>
	/// This function makes sure that Listeners could be added
	/// </summary>
	/// <param name="eventName">The name of the event you want to add</param>
	/// <param name="listener">The name of the listener which you want to add</param>
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
	/// <summary>
	/// This function makes sure that Listeners could be added
	/// </summary>
	/// <param name="eventName">The name of the event you want to add</param>
	/// <param name="listener">The name of the listener which you want to add</param>
	public static void AddScoreListener (UnityAction<int> listener)
	{
		ScoreEvent thisEvent = null;
		if (instance.scoreEventDictionary.TryGetValue (SCOREEVENT, out thisEvent))
		{
			thisEvent.AddListener (listener);
		}
		else
		{
			thisEvent = new ScoreEvent ();
			thisEvent.AddListener (listener);
			instance.scoreEventDictionary.Add (SCOREEVENT, thisEvent);
		}
	}
	/// <summary>
	/// This function makes it so you can add and AudioSFX listener
	/// </summary>
	/// <param name="listener">the listener you want to pass through</param>
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
	/// <summary>
	/// This function makes it so you can add and AudioMusic listener
	/// </summary>
	/// <param name="listener">the listener you want to pass through</param>
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
	/// <summary>
	/// This function makes it so that you can remove your listener
	/// </summary>
	/// <param name="eventName">The name of the event you want to remove</param>
	/// <param name="listener">The name of the listener which you want to remove</param>
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
	/// <summary>
	/// This function makes it so you can remove and AudioSFX listener
	/// </summary>
	/// <param name="listener">the listener you want to remove</param>
	public static void RemoveScoreListener (UnityAction<int> listener)
	{
		if (eventManager == null)
		{
			return;
		}
		ScoreEvent thisEvent = null;
		if (instance.scoreEventDictionary.TryGetValue (SCOREEVENT, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}
	/// <summary>
	/// This function makes it so you can remove and AudioSFX listener
	/// </summary>
	/// <param name="listener">the listener you want to remove</param>
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
	/// <summary>
	/// This function makes it so you can remove and AudioMusic listener
	/// </summary>
	/// <param name="listener">the listener you want to remove</param>
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
	/// <summary>
	/// This function makes it that you can have an trigger event to execute all the events added to the listeners
	/// </summary>
	/// <param name="eventName">the event name you want to pass through</param>
	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}
	/// <summary>
	/// Triggers Score event
	/// </summary>
	/// <param name="score">Specific score you want to add</param>
	public static void TriggerScoreEvent (int score)
	{
		ScoreEvent thisEvent = null;
		if (instance.scoreEventDictionary.TryGetValue (SCOREEVENT, out thisEvent))
		{
			thisEvent.Invoke (score);
		}
	}
	/// <summary>
	/// Triggers AudioSFX event
	/// </summary>
	/// <param name="clip">Specific clip you want to trigger</param>
	public static void TriggerAudioSFXEvent (AudioClip clip)
	{
		AudioSFXEvent thisEvent = null;
		if (instance.audioSFXEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.Invoke (clip);
		}
	}
	/// <summary>
	/// Triggers AudioMusic event
	/// </summary>
	/// <param name="clip">Specific clip you want to trigger</param>
	public static void TriggerAudioMusicEvent (AudioClip clip)
	{
		AudioMusicEvent thisEvent = null;
		if (instance.audioMusicEventDictionary.TryGetValue (AUDIOEVENT, out thisEvent))
		{
			thisEvent.Invoke (clip);
		}
	}
}