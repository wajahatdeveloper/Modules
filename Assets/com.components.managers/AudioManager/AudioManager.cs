using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public enum AudioChannel { Master, Music, Fx };

	[Range(0,1)]
	public float masterVolume = 1;
	[Range(0,1)]
	public float musicVolume = 1f;
	[Range(0,1)]
	public float fxVolume = 1;

	public bool MusicIsLooping = true;

	AudioSource musicSource;
	AudioSource ambientSource;
	AudioSource fxSource;

	SoundLibrary soundLibrary;
	MusicLibrary musicLibrary;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);

		soundLibrary = GetComponent<SoundLibrary>();
		musicLibrary = GetComponent<MusicLibrary>();

		GameObject newfxSource = new GameObject("2D Fx source");
		fxSource = newfxSource.AddComponent<AudioSource>();
		newfxSource.transform.parent = transform;
		fxSource.playOnAwake = false;

		GameObject newMusicSource = new GameObject("Music source");
		musicSource = newMusicSource.AddComponent<AudioSource>();
		newMusicSource.transform.parent = transform;
		musicSource.loop = MusicIsLooping;
		musicSource.playOnAwake = false;

		SetVolume(masterVolume, AudioChannel.Master);
		SetVolume(fxVolume, AudioChannel.Fx);
		SetVolume(musicVolume, AudioChannel.Music);
	}

	public void SetVolume(float volumePercent, AudioChannel channel)
	{
		switch (channel)
		{
			case AudioChannel.Master:
				masterVolume = volumePercent;
				break;
			case AudioChannel.Fx:
				fxVolume = volumePercent;
				break;
			case AudioChannel.Music:
				musicVolume = volumePercent;
				break;
		}

		fxSource.volume = fxVolume * masterVolume;
		musicSource.volume = musicVolume * masterVolume;
	}

	public void PlayMusic(string musicName, float delay)
	{
		musicSource.clip = musicLibrary.GetClipFromName(musicName);
		musicSource.PlayDelayed(delay);
	}

	public IEnumerator PlayMusicFade(string musicName, float duration)
	{
		float startVolume = 0;
		float targetVolume = musicSource.volume;
		float currentTime = 0;

		musicSource.clip = musicLibrary.GetClipFromName(musicName);
		musicSource.Play();

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
			yield return null;
		}

		yield break;
	}

	public void StopMusic()
	{
		musicSource.Stop();
	}

	public IEnumerator StopMusicFade(float duration)
	{
		float currentVolume = musicSource.volume;
		float startVolume = musicSource.volume;
		float targetVolume = 0;
		float currentTime = 0;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			musicSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
			yield return null;
		}
		musicSource.Stop();
		musicSource.volume = currentVolume;

		yield break;
	}

	public void PlaySound(string soundName)
	{
		fxSource.PlayOneShot(soundLibrary.GetClipFromName(soundName), fxVolume * masterVolume);
	}
}
