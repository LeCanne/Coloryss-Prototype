using System.Collections;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private static AudioHandler _instance;
    public static AudioHandler Instance
    {
        get
        {
            if ((object)_instance == null)
            {
                GameObject go = new GameObject("AudioHandler");
                go.AddComponent<AudioHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

   

    public void SpawnClip(AudioClip audio, float volume, Vector3 atLocation)
    {
        GameObject go = new GameObject("SOUND : " + audio.name);
        AudioSource newAudio = go.AddComponent<AudioSource>();
        newAudio.volume = volume;
        newAudio.clip = audio;

        StartCoroutine(AudioSourceManagement(newAudio));
    }

    IEnumerator AudioSourceManagement(AudioSource audioObjectManagement)
    {
        audioObjectManagement.Play();
        while (audioObjectManagement.isPlaying)
        {
            yield return null; 
        }
        Destroy(audioObjectManagement.gameObject);

    }
}
