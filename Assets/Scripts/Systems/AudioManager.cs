using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource musicSource;
    public AudioSource sfxSourcePrefab;
    public int poolSize = 10;

    private Queue<AudioSource> sfxPool = new Queue<AudioSource>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource sfx = Instantiate(sfxSourcePrefab, transform);
            sfx.playOnAwake = false;
            sfxPool.Enqueue(sfx);
        }
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource source = sfxPool.Dequeue();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        StartCoroutine(ReturnToPoolAfterPlay(source));
    }

    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if (music == null) return;
        musicSource.clip = music;
        musicSource.loop = loop;
        musicSource.Play();
    }

    private System.Collections.IEnumerator ReturnToPoolAfterPlay(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length);
        sfxPool.Enqueue(source);
    }
}
