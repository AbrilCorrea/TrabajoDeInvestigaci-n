using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public float minDelay = 5f;
    public float maxDelay = 15f;

    public Vector2 volumeRange = new Vector2(0.5f, 1f);  
    public Vector2 pitchRange = new Vector2(0.9f, 1.2f); 

    private bool playerInside = false;
    private Coroutine playCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            PlayImmediateSound();
            playCoroutine = StartCoroutine(PlayRandomAudios());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            if (playCoroutine != null)
                StopCoroutine(playCoroutine);
        }
    }

    void PlayImmediateSound()
    {
        if (audioClips.Length > 0)
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.PlayOneShot(clip);
        }
    }

    IEnumerator PlayRandomAudios()
    {
        while (playerInside)
        {
            float waitTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(waitTime);

            if (audioClips.Length > 0)
            {
                AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.volume = Random.Range(volumeRange.x, volumeRange.y);
                audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
                audioSource.PlayOneShot(clip);
            }
        }
    }
}