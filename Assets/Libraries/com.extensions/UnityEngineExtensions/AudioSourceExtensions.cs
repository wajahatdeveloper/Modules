using UnityEngine;


// Class to add extensions to Unity's AudioSource component.
public static class AudioSourceExtensions
{
    // Plays a random clip from the list provided on the audio source.
    public static AudioClip PlayRandom(this AudioSource audioSource, AudioClip[] audioClips)
    {
        if (audioClips != null && audioClips.Length > 0)
        {
            int index = Random.Range(0, audioClips.Length);
            audioSource.clip = audioClips[index];
            audioSource.Play();
            return audioClips[index];
        }

        return null;
    }

    public static void Reset(this AudioSource @this)
    {
        @this.clip = null;
        @this.mute = false;
        @this.playOnAwake = true;
        @this.loop = false;
        @this.priority = 128;
        @this.volume = 1;
        @this.pitch = 1;
        @this.panStereo = 0;
        @this.spatialBlend = 0;
        @this.reverbZoneMix = 1;
        @this.dopplerLevel = 1;
        @this.spread = 0;
        @this.maxDistance = 500;
    }
}