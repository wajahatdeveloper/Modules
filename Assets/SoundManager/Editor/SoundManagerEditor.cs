using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


	/// <summary>
	/// Custom editor for the MMSoundManager, used to display custom track controls
	/// </summary>
	#if UNITY_EDITOR
	[CustomEditor(typeof(SoundManager))]
	[CanEditMultipleObjects]
	public class SoundManagerEditor : Editor
	{
		public override bool RequiresConstantRepaint()
		{
			return true;
		}
		protected SoundManagerSettingsSO _settingsSO;
		protected SoundManager soundManager;
        
		private static float _masterVolume, _musicVolume, _sfxVolume, _uiVolume;

		protected Color _originalBackgroundColor;
        
		protected Color _saveButtonColor = new Color32(80, 80, 80, 255);
		protected Color _loadButtonColor = new Color32(107, 107, 107, 255);
		protected Color _resetButtonColor = new Color32(120, 120, 120, 255);
        
		protected Color _baseColor = new Color32(150, 150, 150, 255);

		protected Color _masterColorBase = ColorX.ReunoYellow;
		protected Color _masterColorMute;
		protected Color _masterColorUnmute;
		protected Color _masterColorPause;
		protected Color _masterColorStop;
		protected Color _masterColorPlay;
		protected Color _masterColorFree;

		protected Color _musicColorBase = ColorX.Aquamarine;
		protected Color _musicColorMute;
		protected Color _musicColorUnmute;
		protected Color _musicColorPause;
		protected Color _musicColorStop;
		protected Color _musicColorPlay;
		protected Color _musicColorFree;
        
		protected Color _sfxColorBase = ColorX.Coral;
		protected Color _sfxColorMute;
		protected Color _sfxColorUnmute;
		protected Color _sfxColorPause;
		protected Color _sfxColorStop;
		protected Color _sfxColorPlay;
		protected Color _sfxColorFree;

		protected Color _uiColorBase = ColorX.SteelBlue;
		protected Color _uiColorMute;
		protected Color _uiColorUnmute;
		protected Color _uiColorPause;
		protected Color _uiColorStop;
		protected Color _uiColorPlay;
		protected Color _uiColorFree;

		protected ColorX.ColoringMode _coloringMode = ColorX.ColoringMode.Add;

		/// <summary>
		/// On Enable, we initialize our button colors. Why? Because we can.
		/// </summary>
		protected virtual void OnEnable()
		{
			_masterColorMute = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 1f);
			_masterColorUnmute = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 0.9f);
			_masterColorPause = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 0.8f);
			_masterColorStop = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 0.7f);
			_masterColorPlay = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 0.5f);
			_masterColorFree = ColorX.Colorize(_baseColor, _masterColorBase, _coloringMode, 0.4f);
            
			_musicColorMute = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 1f);
			_musicColorUnmute = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 0.9f);
			_musicColorPause = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 0.8f);
			_musicColorStop = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 0.7f);
			_musicColorPlay = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 0.5f);
			_musicColorFree = ColorX.Colorize(_baseColor, _musicColorBase, _coloringMode, 0.4f);
            
			_sfxColorMute = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 1f);
			_sfxColorUnmute = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 0.9f);
			_sfxColorPause = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 0.8f);
			_sfxColorStop = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 0.7f);
			_sfxColorPlay = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 0.5f);
			_sfxColorFree = ColorX.Colorize(_baseColor, _sfxColorBase, _coloringMode, 0.4f);
            
			_uiColorMute = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 1f);
			_uiColorUnmute = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 0.9f);
			_uiColorPause = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 0.8f);
			_uiColorStop = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 0.7f);
			_uiColorPlay = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 0.5f);
			_uiColorFree = ColorX.Colorize(_baseColor, _uiColorBase, _coloringMode, 0.4f);
		}

		/// <summary>
		/// On GUI, draws the base inspector and track controls
		/// </summary>
		public override void OnInspectorGUI()
		{
			_settingsSO = (target as SoundManager).settingsSo;
			soundManager = target as SoundManager;
            
			if (_settingsSO != null)
			{
				_masterVolume = _settingsSO.GetTrackVolume(SoundManager.SoundManagerTracks.Master);
				_musicVolume = _settingsSO.GetTrackVolume(SoundManager.SoundManagerTracks.Music);
				_sfxVolume = _settingsSO.GetTrackVolume(SoundManager.SoundManagerTracks.Sfx);
				_uiVolume = _settingsSO.GetTrackVolume(SoundManager.SoundManagerTracks.UI);    
			}

			serializedObject.Update();
			DrawDefaultInspector();
			serializedObject.ApplyModifiedProperties();

			if ( ((_settingsSO != null) && soundManager.gameObject.activeInHierarchy))
			{
				DrawTrack("Master Track", soundManager.settingsSo.Settings.MasterOn, SoundManager.SoundManagerTracks.Master, _masterColorMute, _masterColorUnmute, _masterColorPause, _masterColorStop, _masterColorPlay, _masterColorFree);
				DrawTrack("Music Track", soundManager.settingsSo.Settings.MusicOn, SoundManager.SoundManagerTracks.Music, _musicColorMute, _musicColorUnmute, _musicColorPause, _musicColorStop, _musicColorPlay, _musicColorFree);
				DrawTrack("SFX Track", soundManager.settingsSo.Settings.SfxOn, SoundManager.SoundManagerTracks.Sfx, _sfxColorMute, _sfxColorUnmute, _sfxColorPause, _sfxColorStop, _sfxColorPlay, _sfxColorFree);
				DrawTrack("UI Track", soundManager.settingsSo.Settings.UIOn, SoundManager.SoundManagerTracks.UI, _uiColorMute, _uiColorUnmute, _uiColorPause, _uiColorStop, _uiColorPlay, _uiColorFree);
				DrawSaveLoadButtons();
			}
		}

		/// <summary>
		/// Draws track controls for the specified track
		/// </summary>
		/// <param name="title"></param>
		/// <param name="mute"></param>
		/// <param name="track"></param>
		/// <param name="muteColor"></param>
		/// <param name="unmuteColor"></param>
		/// <param name="pauseColor"></param>
		/// <param name="stopColor"></param>
		/// <param name="playColor"></param>
		/// <param name="freeColor"></param>
		protected virtual void DrawTrack(string title, bool mute, SoundManager.SoundManagerTracks track, Color muteColor, Color unmuteColor, Color pauseColor, Color stopColor, Color playColor, Color freeColor)
		{
			GUILayout.Space(10);
			GUILayout.Label(title, EditorStyles.boldLabel);
            
			EditorGUI.BeginDisabledGroup(!Application.isPlaying);

			// we draw the volume slider
			EditorGUILayout.BeginHorizontal();
            
			GUILayout.Label("Volume");
            
			float newVolume = 0;
			switch (track)
			{
				case SoundManager.SoundManagerTracks.Master:
					newVolume = EditorGUILayout.Slider(_masterVolume, SoundManagerSettings._minimalVolume, SoundManagerSettings._maxVolume);
					if (newVolume != _masterVolume) { soundManager.settingsSo.SetTrackVolume(SoundManager.SoundManagerTracks.Master, newVolume); }
					break;
				case SoundManager.SoundManagerTracks.Music:
					newVolume = EditorGUILayout.Slider(_musicVolume, SoundManagerSettings._minimalVolume, SoundManagerSettings._maxVolume);
					if (newVolume != _musicVolume) { soundManager.settingsSo.SetTrackVolume(SoundManager.SoundManagerTracks.Music, newVolume); }
					break;
				case SoundManager.SoundManagerTracks.Sfx:
					newVolume = EditorGUILayout.Slider(_sfxVolume, SoundManagerSettings._minimalVolume, SoundManagerSettings._maxVolume);
					if (newVolume != _sfxVolume) { soundManager.settingsSo.SetTrackVolume(SoundManager.SoundManagerTracks.Sfx, newVolume); }
					break;
				case SoundManager.SoundManagerTracks.UI:
					newVolume = EditorGUILayout.Slider(_uiVolume, SoundManagerSettings._minimalVolume, SoundManagerSettings._maxVolume);
					if (newVolume != _uiVolume) { soundManager.settingsSo.SetTrackVolume(SoundManager.SoundManagerTracks.UI, newVolume); }
					break;
			}
			EditorGUILayout.EndHorizontal();

			// we draw the buttons
			EditorGUILayout.BeginHorizontal();
			{
				if (mute)
				{
					DrawColoredButton("Mute", muteColor, track, soundManager.MuteTrack, EditorStyles.miniButtonLeft);    
				}
				else
				{
					DrawColoredButton("Unmute", unmuteColor, track, soundManager.UnmuteTrack, EditorStyles.miniButtonMid);    
				}
				DrawColoredButton("Pause", pauseColor, track, soundManager.PauseTrack, EditorStyles.miniButtonMid);
				DrawColoredButton("Stop", stopColor, track, soundManager.StopTrack, EditorStyles.miniButtonMid);
				DrawColoredButton("Play", playColor, track, soundManager.PlayTrack, EditorStyles.miniButtonMid);
				DrawColoredButton("Free", freeColor, track, soundManager.FreeTrack, EditorStyles.miniButtonRight);
                
			}
			EditorGUILayout.EndHorizontal();
            
			EditorGUI.EndDisabledGroup();
		}
        
		/// <summary>
		/// Draws save related buttons
		/// </summary>
		protected virtual void DrawSaveLoadButtons()
		{
			EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            
			GUILayout.Space(10);
			GUILayout.Label("Settings", EditorStyles.boldLabel);
            
			EditorGUILayout.BeginHorizontal();
            
			DrawColoredButton("Save", _saveButtonColor, _settingsSO.SaveSoundSettings, EditorStyles.miniButtonLeft);
			DrawColoredButton("Load", _loadButtonColor, _settingsSO.LoadSoundSettings, EditorStyles.miniButtonMid);
			DrawColoredButton("Reset", _resetButtonColor, _settingsSO.ResetSoundSettings, EditorStyles.miniButtonRight);
            
			EditorGUILayout.EndHorizontal();
            
			EditorGUI.EndDisabledGroup();
		}

		/// <summary>
		/// Draws a button
		/// </summary>
		/// <param name="buttonLabel"></param>
		/// <param name="buttonColor"></param>
		/// <param name="track"></param>
		/// <param name="action"></param>
		/// <param name="styles"></param>
		public virtual void DrawColoredButton(string buttonLabel, Color buttonColor, SoundManager.SoundManagerTracks track, System.Action<SoundManager.SoundManagerTracks> action, GUIStyle styles)
		{
			_originalBackgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = buttonColor;
			if (GUILayout.Button(buttonLabel, styles))
			{
				action.Invoke(track);
			}
			GUI.backgroundColor = _originalBackgroundColor;
		}

		/// <summary>
		/// Draws a button
		/// </summary>
		/// <param name="buttonLabel"></param>
		/// <param name="buttonColor"></param>
		/// <param name="action"></param>
		/// <param name="styles"></param>
		protected virtual void DrawColoredButton(string buttonLabel, Color buttonColor, Action action, GUIStyle styles)
		{
			_originalBackgroundColor = GUI.backgroundColor;
			GUI.backgroundColor = buttonColor;
			if (GUILayout.Button(buttonLabel, styles))
			{
				action.Invoke();
			}
			GUI.backgroundColor = _originalBackgroundColor;
		}
        
	}
	#endif