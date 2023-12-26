using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple struct used to store information about the sounds played by the MMSoundManager
/// </summary>
[Serializable]
public struct SoundManagerSound
{
	/// the ID of the sound 
	public int ID;
	/// the track the sound is being played on
	public SoundManager.SoundManagerTracks Track;
	/// the associated audiosource
	public AudioSource Source;
	/// whether or not this sound will play over multiple scenes
	public bool Persistent;
}