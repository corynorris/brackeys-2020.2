using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    public static Vector3 GetRandomDir()
    {
        return UnityEngine.Random.insideUnitCircle.normalized;
    }

    public static AudioSource AddAudioNoFalloff(GameObject gameObject, AudioClip clip, bool loop, bool playAwake, float vol, float pitch, float minRange = 1000, float maxRange = 1000)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        newAudio.pitch = pitch;
        newAudio.spatialBlend = 1f;
        newAudio.rolloffMode = AudioRolloffMode.Linear;
        newAudio.minDistance = minRange;
        newAudio.maxDistance = maxRange;

        return newAudio;
    }

    public static void spawnAudio(GameObject gameObject, AudioClip clip, float volume = 1)
    {
        GameObject audioObject = new GameObject();
        audioObject.transform.position = gameObject.transform.position;        
        AddAudioNoFalloff(audioObject, clip, false, true, volume, 1);
        audioObject.GetComponent<AudioSource>().Play();
        GameObject.Destroy(audioObject,clip.length);
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        if (targetVolume == 0)
        {
            audioSource.Stop();
        }

        yield break;
    }
}
