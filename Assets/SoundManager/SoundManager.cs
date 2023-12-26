using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    public enum SoundManagerTracks
    {
        Sfx,
        Music,
        UI,
        Master,
        Other
    }

    [Header("Settings")] [Tooltip("the current sound settings ")]
    public SoundManagerSettingsSO settingsSo;

    [Header("Pool")]
    [Tooltip(
        "the size of the AudioSource pool, a reserve of ready-to-use sources that will get recycled. Should be approximately equal to the maximum amount of sounds that you expect to be playing at once")]
    public int AudioSourcePoolSize = 10;

    [Tooltip(
        "whether or not the pool can expand (create new audiosources on demand). In a perfect world you'd want to avoid this, and have a sufficiently big pool, to avoid costly runtime creations.")]
    public bool PoolCanExpand = true;

    protected SoundManagerAudioPool _pool;
    protected GameObject _tempAudioSourceGameObject;
    protected SoundManagerSound _sound;
    protected List<SoundManagerSound> _sounds;
    protected AudioSource _tempAudioSource;

    #region Initialization

    protected void Awake()
    {
        InitializeSoundManager();
    }


    protected virtual void Start()
    {
        if ((settingsSo != null) && (settingsSo.Settings.AutoLoad))
        {
            settingsSo.LoadSoundSettings();
        }
    }


    protected virtual void InitializeSoundManager()
    {
        if (_pool == null)
        {
            _pool = new SoundManagerAudioPool();
        }

        _sounds = new List<SoundManagerSound>();
        _pool.FillAudioSourcePool(AudioSourcePoolSize, this.transform);
    }

    #endregion

    #region PlaySound

    public virtual AudioSource PlaySound(AudioClip audioClip, SoundManagerPlayOptions options)
    {
        return PlaySound(audioClip, options.soundManagerTrack, options.Location,
            options.Loop, options.Volume, options.ID,
            options.Fade, options.FadeInitialVolume, options.FadeDuration,
            options.Persistent,
            options.RecycleAudioSource, options.AudioGroup,
            options.Pitch, options.PanStereo, options.SpatialBlend,
            options.SoloSingleTrack, options.SoloAllTracks, options.AutoUnSoloOnEnd,
            options.BypassEffects, options.BypassListenerEffects, options.BypassReverbZones, options.Priority,
            options.ReverbZoneMix,
            options.DopplerLevel, options.Spread, options.RolloffMode, options.MinDistance, options.MaxDistance,
            options.DoNotAutoRecycleIfNotDonePlaying, options.PlaybackTime
        );
    }

    public virtual AudioSource PlaySound(AudioClip audioClip, SoundManagerTracks soundManagerTrack, Vector3 location,
        bool loop = false, float volume = 1.0f, int ID = 0,
        bool fade = false, float fadeInitialVolume = 0f, float fadeDuration = 1f,
        bool persistent = false,
        AudioSource recycleAudioSource = null, AudioMixerGroup audioGroup = null,
        float pitch = 1f, float panStereo = 0f, float spatialBlend = 0.0f,
        bool soloSingleTrack = false, bool soloAllTracks = false, bool autoUnSoloOnEnd = false,
        bool bypassEffects = false, bool bypassListenerEffects = false, bool bypassReverbZones = false,
        int priority = 128, float reverbZoneMix = 1f,
        float dopplerLevel = 1f, int spread = 0, AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic,
        float minDistance = 1f, float maxDistance = 500f,
        bool doNotAutoRecycleIfNotDonePlaying = false, float playbackTime = 0f
    )
    {
        if (this == null)
        {
            return null;
        }

        if (!audioClip)
        {
            return null;
        }


        AudioSource audioSource = recycleAudioSource;

        if (audioSource == null)
        {
            audioSource = _pool.GetAvailableAudioSource(PoolCanExpand, this.transform);
            if ((audioSource != null) && (!loop))
            {
                recycleAudioSource = audioSource;

                StartCoroutine(_pool.AutoDisableAudioSource(audioClip.length / Mathf.Abs(pitch), audioSource, audioClip,
                    doNotAutoRecycleIfNotDonePlaying));
            }
        }


        if (audioSource == null)
        {
            _tempAudioSourceGameObject = new GameObject("MMAudio_" + audioClip.name);
            SceneManager.MoveGameObjectToScene(_tempAudioSourceGameObject, this.gameObject.scene);
            audioSource = _tempAudioSourceGameObject.AddComponent<AudioSource>();
        }


        audioSource.transform.position = location;
        audioSource.clip = audioClip;
        audioSource.pitch = pitch;
        audioSource.spatialBlend = spatialBlend;
        audioSource.panStereo = panStereo;
        audioSource.loop = loop;
        audioSource.bypassEffects = bypassEffects;
        audioSource.bypassListenerEffects = bypassListenerEffects;
        audioSource.bypassReverbZones = bypassReverbZones;
        audioSource.priority = priority;
        audioSource.reverbZoneMix = reverbZoneMix;
        audioSource.dopplerLevel = dopplerLevel;
        audioSource.spread = spread;
        audioSource.rolloffMode = rolloffMode;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.time = playbackTime;


        if (settingsSo != null)
        {
            audioSource.outputAudioMixerGroup = settingsSo.MasterAudioMixerGroup;
            switch (soundManagerTrack)
            {
                case SoundManagerTracks.Master:
                    audioSource.outputAudioMixerGroup = settingsSo.MasterAudioMixerGroup;
                    break;
                case SoundManagerTracks.Music:
                    audioSource.outputAudioMixerGroup = settingsSo.MusicAudioMixerGroup;
                    break;
                case SoundManagerTracks.Sfx:
                    audioSource.outputAudioMixerGroup = settingsSo.SfxAudioMixerGroup;
                    break;
                case SoundManagerTracks.UI:
                    audioSource.outputAudioMixerGroup = settingsSo.UIAudioMixerGroup;
                    break;
            }
        }

        if (audioGroup)
        {
            audioSource.outputAudioMixerGroup = audioGroup;
        }

        audioSource.volume = volume;


        audioSource.Play();


        if (!loop && !recycleAudioSource)
        {
            Destroy(_tempAudioSourceGameObject, audioClip.length);
        }


        if (fade)
        {
            FadeSound(audioSource, fadeDuration, fadeInitialVolume, volume);
        }


        if (soloSingleTrack)
        {
            MuteSoundsOnTrack(soundManagerTrack, true, 0f);
            audioSource.mute = false;
            if (autoUnSoloOnEnd)
            {
                MuteSoundsOnTrack(soundManagerTrack, false, audioClip.length);
            }
        }
        else if (soloAllTracks)
        {
            MuteAllSounds();
            audioSource.mute = false;
            if (autoUnSoloOnEnd)
            {
                StartCoroutine(MuteAllSoundsCoroutine(audioClip.length - playbackTime, false));
            }
        }


        _sound.ID = ID;
        _sound.Track = soundManagerTrack;
        _sound.Source = audioSource;
        _sound.Persistent = persistent;


        bool alreadyIn = false;
        for (int i = 0; i < _sounds.Count; i++)
        {
            if (_sounds[i].Source == audioSource)
            {
                _sounds[i] = _sound;
                alreadyIn = true;
            }
        }

        if (!alreadyIn)
        {
            _sounds.Add(_sound);
        }


        return audioSource;
    }

    #endregion

    #region SoundControls

    public virtual void PauseSound(AudioSource source)
    {
        source.Pause();
    }

    public virtual void ResumeSound(AudioSource source)
    {
        source.Play();
    }

    public virtual void StopSound(AudioSource source)
    {
        source.Stop();
    }

    public virtual void FreeSound(AudioSource source)
    {
        source.Stop();
        if (!_pool.FreeSound(source))
        {
            Destroy(source.gameObject);
        }
    }

    #endregion

    #region TrackControls

    public virtual void MuteTrack(SoundManagerTracks track)
    {
        ControlTrack(track, ControlTrackModes.Mute, 0f);
    }

    public virtual void UnmuteTrack(SoundManagerTracks track)
    {
        ControlTrack(track, ControlTrackModes.Unmute, 0f);
    }

    public virtual void SetTrackVolume(SoundManagerTracks track, float volume)
    {
        ControlTrack(track, ControlTrackModes.SetVolume, volume);
    }

    public virtual float GetTrackVolume(SoundManagerTracks track, bool mutedVolume)
    {
        switch (track)
        {
            case SoundManagerTracks.Master:
                return mutedVolume ? settingsSo.Settings.MutedMasterVolume : settingsSo.Settings.MasterVolume;
            case SoundManagerTracks.Music:
                return mutedVolume ? settingsSo.Settings.MutedMusicVolume : settingsSo.Settings.MusicVolume;
            case SoundManagerTracks.Sfx:
                return mutedVolume ? settingsSo.Settings.MutedSfxVolume : settingsSo.Settings.SfxVolume;
            case SoundManagerTracks.UI:
                return mutedVolume ? settingsSo.Settings.MutedUIVolume : settingsSo.Settings.UIVolume;
        }

        return 1f;
    }

    public virtual void PauseTrack(SoundManagerTracks track)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Track == track)
            {
                sound.Source.Pause();
            }
        }
    }

    public virtual void PlayTrack(SoundManagerTracks track)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Track == track)
            {
                sound.Source.Play();
            }
        }
    }

    public virtual void StopTrack(SoundManagerTracks track)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Track == track)
            {
                sound.Source.Stop();
            }
        }
    }

    public virtual bool HasSoundsPlaying(SoundManagerTracks track)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if ((sound.Track == track) && (sound.Source.isPlaying))
            {
                return true;
            }
        }

        return false;
    }

    public virtual void FreeTrack(SoundManagerTracks track)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Track == track)
            {
                sound.Source.Stop();
                sound.Source.gameObject.SetActive(false);
            }
        }
    }


    public virtual void MuteMusic()
    {
        MuteTrack(SoundManagerTracks.Music);
    }


    public virtual void UnmuteMusic()
    {
        UnmuteTrack(SoundManagerTracks.Music);
    }


    public virtual void MuteSfx()
    {
        MuteTrack(SoundManagerTracks.Sfx);
    }


    public virtual void UnmuteSfx()
    {
        UnmuteTrack(SoundManagerTracks.Sfx);
    }


    public virtual void MuteUI()
    {
        MuteTrack(SoundManagerTracks.UI);
    }


    public virtual void UnmuteUI()
    {
        UnmuteTrack(SoundManagerTracks.UI);
    }


    public virtual void MuteMaster()
    {
        MuteTrack(SoundManagerTracks.Master);
    }


    public virtual void UnmuteMaster()
    {
        UnmuteTrack(SoundManagerTracks.Master);
    }


    public virtual void SetVolumeMusic(float newVolume)
    {
        SetTrackVolume(SoundManagerTracks.Music, newVolume);
    }


    public virtual void SetVolumeSfx(float newVolume)
    {
        SetTrackVolume(SoundManagerTracks.Sfx, newVolume);
    }


    public virtual void SetVolumeUI(float newVolume)
    {
        SetTrackVolume(SoundManagerTracks.UI, newVolume);
    }


    public virtual void SetVolumeMaster(float newVolume)
    {
        SetTrackVolume(SoundManagerTracks.Master, newVolume);
    }


    public virtual bool IsMuted(SoundManagerTracks track)
    {
        switch (track)
        {
            case SoundManagerTracks.Master:
                return settingsSo.Settings.MasterOn;
            case SoundManagerTracks.Music:
                return settingsSo.Settings.MusicOn;
            case SoundManagerTracks.Sfx:
                return settingsSo.Settings.SfxOn;
            case SoundManagerTracks.UI:
                return settingsSo.Settings.UIOn;
        }

        return false;
    }


    public enum ControlTrackModes
    {
        Mute,
        Unmute,
        SetVolume
    }

    protected virtual void ControlTrack(SoundManagerTracks track, ControlTrackModes trackMode, float volume = 0.5f)
    {
        string target = "";
        float savedVolume = 0f;

        switch (track)
        {
            case SoundManagerTracks.Master:
                target = settingsSo.Settings.MasterVolumeParameter;
                if (trackMode == ControlTrackModes.Mute)
                {
                    settingsSo.TargetAudioMixer.GetFloat(target, out settingsSo.Settings.MutedMasterVolume);
                    settingsSo.Settings.MasterOn = false;
                }
                else if (trackMode == ControlTrackModes.Unmute)
                {
                    savedVolume = settingsSo.Settings.MutedMasterVolume;
                    settingsSo.Settings.MasterOn = true;
                }

                break;
            case SoundManagerTracks.Music:
                target = settingsSo.Settings.MusicVolumeParameter;
                if (trackMode == ControlTrackModes.Mute)
                {
                    settingsSo.TargetAudioMixer.GetFloat(target, out settingsSo.Settings.MutedMusicVolume);
                    settingsSo.Settings.MusicOn = false;
                }
                else if (trackMode == ControlTrackModes.Unmute)
                {
                    savedVolume = settingsSo.Settings.MutedMusicVolume;
                    settingsSo.Settings.MusicOn = true;
                }

                break;
            case SoundManagerTracks.Sfx:
                target = settingsSo.Settings.SfxVolumeParameter;
                if (trackMode == ControlTrackModes.Mute)
                {
                    settingsSo.TargetAudioMixer.GetFloat(target, out settingsSo.Settings.MutedSfxVolume);
                    settingsSo.Settings.SfxOn = false;
                }
                else if (trackMode == ControlTrackModes.Unmute)
                {
                    savedVolume = settingsSo.Settings.MutedSfxVolume;
                    settingsSo.Settings.SfxOn = true;
                }

                break;
            case SoundManagerTracks.UI:
                target = settingsSo.Settings.UIVolumeParameter;
                if (trackMode == ControlTrackModes.Mute)
                {
                    settingsSo.TargetAudioMixer.GetFloat(target, out settingsSo.Settings.MutedUIVolume);
                    settingsSo.Settings.UIOn = false;
                }
                else if (trackMode == ControlTrackModes.Unmute)
                {
                    savedVolume = settingsSo.Settings.MutedUIVolume;
                    settingsSo.Settings.UIOn = true;
                }

                break;
        }

        switch (trackMode)
        {
            case ControlTrackModes.Mute:
                settingsSo.SetTrackVolume(track, 0f);
                break;
            case ControlTrackModes.Unmute:
                settingsSo.SetTrackVolume(track, settingsSo.MixerVolumeToNormalized(savedVolume));
                break;
            case ControlTrackModes.SetVolume:
                settingsSo.SetTrackVolume(track, volume);
                break;
        }

        settingsSo.GetTrackVolumes();

        if (settingsSo.Settings.AutoSave)
        {
            settingsSo.SaveSoundSettings();
        }
    }

    #endregion

    #region Fades

    public virtual void FadeTrack(SoundManagerTracks track, float duration, float initialVolume = 0f,
        float finalVolume = 1f)
    {
        StartCoroutine(FadeTrackCoroutine(track, duration, initialVolume, finalVolume));
    }


    public virtual void FadeSound(AudioSource source, float duration, float initialVolume, float finalVolume)
    {
        StartCoroutine(FadeCoroutine(source, duration, initialVolume, finalVolume));
    }


    protected virtual IEnumerator FadeTrackCoroutine(SoundManagerTracks track, float duration, float initialVolume,
        float finalVolume)
    {
        float startedAt = Time.unscaledTime;
        while (Time.unscaledTime - startedAt <= duration)
        {
            float elapsedTime = Time.unscaledTime - startedAt;
            float newVolume = MathX.ReMap(elapsedTime, 0f, duration, initialVolume, finalVolume);
            settingsSo.SetTrackVolume(track, newVolume);
            yield return null;
        }

        settingsSo.SetTrackVolume(track, finalVolume);
    }


    protected virtual IEnumerator FadeCoroutine(AudioSource source, float duration, float initialVolume,
        float finalVolume)
    {
        float startedAt = Time.unscaledTime;
        while (Time.unscaledTime - startedAt <= duration)
        {
            float elapsedTime = Time.unscaledTime - startedAt;
            float newVolume = MathX.ReMap(elapsedTime, 0f, duration, initialVolume, finalVolume);
            source.volume = newVolume;
            yield return null;
        }

        source.volume = finalVolume;
    }

    #endregion

    #region Solo

    public virtual void MuteSoundsOnTrack(SoundManagerTracks track, bool mute, float delay = 0f)
    {
        StartCoroutine(MuteSoundsOnTrackCoroutine(track, mute, delay));
    }


    public virtual void MuteAllSounds(bool mute = true)
    {
        StartCoroutine(MuteAllSoundsCoroutine(0f, mute));
    }


    protected virtual IEnumerator MuteSoundsOnTrackCoroutine(SoundManagerTracks track, bool mute, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Track == track)
            {
                sound.Source.mute = mute;
            }
        }
    }


    protected virtual IEnumerator MuteAllSoundsCoroutine(float delay, bool mute = true)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        foreach (SoundManagerSound sound in _sounds)
        {
            sound.Source.mute = mute;
        }
    }

    #endregion

    #region Find

    public virtual AudioSource FindByID(int ID)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.ID == ID)
            {
                return sound.Source;
            }
        }

        return null;
    }


    public virtual AudioSource FindByClip(AudioClip clip)
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Source.clip == clip)
            {
                return sound.Source;
            }
        }

        return null;
    }

    #endregion

    #region AllSoundsControls

    public virtual void PauseAllSounds()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            sound.Source.Pause();
        }
    }


    public virtual void PlayAllSounds()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            sound.Source.Play();
        }
    }


    public virtual void StopAllSounds()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            sound.Source.Stop();
        }
    }


    public virtual void FreeAllSounds()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if (sound.Source != null)
            {
                FreeSound(sound.Source);
            }
        }
    }


    public virtual void FreeAllSoundsButPersistent()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if ((!sound.Persistent) && (sound.Source != null))
            {
                FreeSound(sound.Source);
            }
        }
    }


    public virtual void FreeAllLoopingSounds()
    {
        foreach (SoundManagerSound sound in _sounds)
        {
            if ((sound.Source.loop) && (sound.Source != null))
            {
                FreeSound(sound.Source);
            }
        }
    }

    #endregion

    protected virtual void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode)
    {
        FreeAllSoundsButPersistent();
    }

    public virtual void SaveSettings()
    {
        settingsSo.SaveSoundSettings();
    }


    public virtual void LoadSettings()
    {
        settingsSo.LoadSoundSettings();
    }


    public virtual void ResetSettings()
    {
        settingsSo.ResetSoundSettings();
    }

    protected virtual void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    protected virtual void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}