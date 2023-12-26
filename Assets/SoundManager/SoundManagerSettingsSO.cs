﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// A class to save sound settings (music on or off, sfx on or off)
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "Audio/SoundManagerSettings")]
public class SoundManagerSettingsSO : ScriptableObject
{
	[Header("Audio Mixer")]
	/// the audio mixer to use when playing sounds
	[Tooltip("the audio mixer to use when playing sounds")]
	public AudioMixer TargetAudioMixer;
	/// the master group
	[Tooltip("the master group")]
	public AudioMixerGroup MasterAudioMixerGroup;
	/// the group on which to play all music sounds
	[Tooltip("the group on which to play all music sounds")]
	public AudioMixerGroup MusicAudioMixerGroup;
	/// the group on which to play all sound effects
	[Tooltip("the group on which to play all sound effects")]
	public AudioMixerGroup SfxAudioMixerGroup;
	/// the group on which to play all UI sounds
	[Tooltip("the group on which to play all UI sounds")]
	public AudioMixerGroup UIAudioMixerGroup;
	/// the multiplier to apply when converting normalized volume values to audio mixer values
	[Tooltip("the multiplier to apply when converting normalized volume values to audio mixer values")]
	public float MixerValuesMultiplier = 20;

	[Header("Settings Unfold")]
	/// the full settings for this SoundManager
	[Tooltip("the full settings for this SoundManager")]
	public SoundManagerSettings Settings;

	protected const string _saveFolderName = "SoundManager/";
	protected const string _saveFileName = "sound.settings";

	#region SaveAndLoad

	/// <summary>
	/// Saves the sound settings to file
	/// </summary>
	public virtual void SaveSoundSettings()
	{
		///* MMSaveLoadManager.Save(this.Settings, _saveFileName, _saveFolderName);
	}

	/// <summary>
	/// Loads the sound settings from file (if found)
	/// </summary>
	public virtual void LoadSoundSettings()
	{
		if (Settings.OverrideMixerSettings)
		{
			/*SoundManagerSettings settings =
				(SoundManagerSettings) MMSaveLoadManager.Load(typeof(SoundManagerSettings), _saveFileName,
					_saveFolderName);
			if (settings != null)
			{
				this.Settings = settings;
				ApplyTrackVolumes();
			}*/
		}
	}

	/// <summary>
	/// Resets the sound settings by destroying the save file
	/// </summary>
	public virtual void ResetSoundSettings()
	{
		//* MMSaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
	}

	#endregion

	#region Volume

	/// <summary>
	/// sets the volume of the selected track to the value passed in parameters
	/// </summary>
	/// <param name="track"></param>
	/// <param name="volume"></param>
	public virtual void SetTrackVolume(SoundManager.SoundManagerTracks track, float volume)
	{
		if (volume <= 0f)
		{
			volume = SoundManagerSettings._minimalVolume;
		}

		switch (track)
		{
			case SoundManager.SoundManagerTracks.Master:
				TargetAudioMixer.SetFloat(Settings.MasterVolumeParameter, NormalizedToMixerVolume(volume));
				Settings.MasterVolume = volume;
				break;
			case SoundManager.SoundManagerTracks.Music:
				TargetAudioMixer.SetFloat(Settings.MusicVolumeParameter, NormalizedToMixerVolume(volume));
				Settings.MusicVolume = volume;
				break;
			case SoundManager.SoundManagerTracks.Sfx:
				TargetAudioMixer.SetFloat(Settings.SfxVolumeParameter, NormalizedToMixerVolume(volume));
				Settings.SfxVolume = volume;
				break;
			case SoundManager.SoundManagerTracks.UI:
				TargetAudioMixer.SetFloat(Settings.UIVolumeParameter, NormalizedToMixerVolume(volume));
				Settings.UIVolume = volume;
				break;
		}

		if (Settings.AutoSave)
		{
			SaveSoundSettings();
		}
	}

	/// <summary>
	/// Returns the volume of the specified track
	/// </summary>
	/// <param name="track"></param>
	/// <returns></returns>
	public virtual float GetTrackVolume(SoundManager.SoundManagerTracks track)
	{
		float volume = 1f;
		switch (track)
		{
			case SoundManager.SoundManagerTracks.Master:
				TargetAudioMixer.GetFloat(Settings.MasterVolumeParameter, out volume);
				break;
			case SoundManager.SoundManagerTracks.Music:
				TargetAudioMixer.GetFloat(Settings.MusicVolumeParameter, out volume);
				break;
			case SoundManager.SoundManagerTracks.Sfx:
				TargetAudioMixer.GetFloat(Settings.SfxVolumeParameter, out volume);
				break;
			case SoundManager.SoundManagerTracks.UI:
				TargetAudioMixer.GetFloat(Settings.UIVolumeParameter, out volume);
				break;
		}

		return MixerVolumeToNormalized(volume);
	}

	/// <summary>
	/// assigns the volume of each track to the settings values
	/// </summary>
	public virtual void GetTrackVolumes()
	{
		Settings.MasterVolume = GetTrackVolume(SoundManager.SoundManagerTracks.Master);
		Settings.MusicVolume = GetTrackVolume(SoundManager.SoundManagerTracks.Music);
		Settings.SfxVolume = GetTrackVolume(SoundManager.SoundManagerTracks.Sfx);
		Settings.UIVolume = GetTrackVolume(SoundManager.SoundManagerTracks.UI);
	}

	/// <summary>
	/// applies volume to all tracks and saves if needed
	/// </summary>
	protected virtual void ApplyTrackVolumes()
	{
		if (Settings.OverrideMixerSettings)
		{
			TargetAudioMixer.SetFloat(Settings.MasterVolumeParameter, NormalizedToMixerVolume(Settings.MasterVolume));
			TargetAudioMixer.SetFloat(Settings.MusicVolumeParameter, NormalizedToMixerVolume(Settings.MusicVolume));
			TargetAudioMixer.SetFloat(Settings.SfxVolumeParameter, NormalizedToMixerVolume(Settings.SfxVolume));
			TargetAudioMixer.SetFloat(Settings.UIVolumeParameter, NormalizedToMixerVolume(Settings.UIVolume));

			if (!Settings.MasterOn) { TargetAudioMixer.SetFloat(Settings.MasterVolumeParameter, -80f); }
			if (!Settings.MusicOn) { TargetAudioMixer.SetFloat(Settings.MusicVolumeParameter, -80f); }
			if (!Settings.SfxOn) { TargetAudioMixer.SetFloat(Settings.SfxVolumeParameter, -80f); }
			if (!Settings.UIOn) { TargetAudioMixer.SetFloat(Settings.UIVolumeParameter, -80f); }

			if (Settings.AutoSave)
			{
				SaveSoundSettings();
			}
		}
	}

	/// <summary>
	/// Converts a normalized volume to the mixer group db scale
	/// </summary>
	/// <param name="normalizedVolume"></param>
	/// <returns></returns>
	public virtual float NormalizedToMixerVolume(float normalizedVolume)
	{
		return Mathf.Log10(normalizedVolume) * MixerValuesMultiplier;
	}

	/// <summary>
	/// Converts mixer volume to a normalized value
	/// </summary>
	/// <param name="mixerVolume"></param>
	/// <returns></returns>
	public virtual float MixerVolumeToNormalized(float mixerVolume)
	{
		return (float)Math.Pow(10, (mixerVolume / MixerValuesMultiplier));
	}

	#endregion Volume
}