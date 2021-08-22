using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static AudioSource AddAudioNoFalloff(GameObject gameObject, AudioClip clip, bool loop, bool playAwake, float vol, float pitch, float minRange = 100, float maxRange = 100)
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

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
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
