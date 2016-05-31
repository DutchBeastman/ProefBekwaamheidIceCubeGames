// Created by: Jeremy Bond
// Date: 27/05/2016

using UnityEngine;

public class AudioClips : MonoBehaviour 
{
	[SerializeField] private AudioClip[] digSounds;
	[SerializeField] private AudioClip[] buttonPushSounds;
	[SerializeField] private AudioClip walkingSound;
	[SerializeField] private AudioClip LosingLifeSound;
	[SerializeField] private AudioClip blockLandingSound;
	[SerializeField] private AudioClip[] gameMusicTracks;
	public static AudioClip digSound;
	public static AudioClip buttonSound;
	public static AudioClip walkSound; 
	public static AudioClip lostLifeSound;
	public static AudioClip blockLandSound;
	public static AudioClip gameMusicTrack;

	protected void Awake ()
	{
		digSound = digSounds[Random.Range(0, digSounds.Length)];
		buttonSound = buttonPushSounds[Random.Range(0, buttonPushSounds.Length)];
		walkSound = walkingSound;
		lostLifeSound = LosingLifeSound;
		blockLandSound = blockLandingSound;
		gameMusicTrack = gameMusicTracks[Random.Range(0, gameMusicTracks.Length)];
	}
}
