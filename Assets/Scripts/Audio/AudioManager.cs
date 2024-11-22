using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource managerAudioSrc;
    [SerializeField] AudioSource speaker1AudioSrc;
    [SerializeField] AudioSource speaker2AudioSrc;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Count - 1)];
            managerAudioSrc.clip = clip;
            managerAudioSrc.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Count - 1)];
            speaker1AudioSrc.clip = clip;
            speaker1AudioSrc.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioClip clip = audioClips[Random.Range(0, audioClips.Count - 1)];
            speaker2AudioSrc.clip = clip;
            speaker2AudioSrc.Play();
        }
    }
}
