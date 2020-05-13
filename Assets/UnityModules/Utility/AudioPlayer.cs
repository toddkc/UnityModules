using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private List<AudioSource> audioSourceList = new List<AudioSource>();
    private Transform playerTransform;
    public static AudioPlayer instance;

    [Header("Master Volume")]
    [SerializeField] float defaultVolume = 0.4f;
    [Header("Win Sound")]
    [SerializeField] AudioClip winClip = default;
    [SerializeField] float winClipVolume = 0f;
    [Header("Ball Pickup Sound")]
    [SerializeField] AudioClip ballPickupClip = default;
    [SerializeField] float ballPickupVolume = 0f;
    [Header("Gravity Flip Sound")]
    [SerializeField] AudioClip gravityFlipClip = default;
    [SerializeField] float gravityFlipVolume = 0f;

    private void Awake()
    {
        instance = this;
        playerTransform = Camera.main.transform;
    }

    private void Start()
    {
        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        audioSourceList = new List<AudioSource>();
        for (int i = 0; i < 20; i++)
        {
            var obj = new GameObject("AudioSource" + (i + 1));
            AudioSource src = obj.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;
            src.volume = defaultVolume;
            src.spatialBlend = 1;
            obj.transform.parent = transform;
            obj.transform.localPosition = Vector3.zero;
            audioSourceList.Add(src);
        }
    }

    public static void PlayWinSound()
    {
        if (instance.winClip) PlaySound(instance.winClip, instance.playerTransform.position, instance.winClipVolume);
    }
    public static void PlayBallPickupSound()
    {
        if (instance.ballPickupClip) PlaySound(instance.ballPickupClip, instance.playerTransform.position, instance.ballPickupVolume);
    }
    public static void PlayFlipSound()
    {
        if (instance.gravityFlipClip) PlaySound(instance.gravityFlipClip, instance.playerTransform.position, instance.gravityFlipVolume);
    }

    public static void PlaySound(AudioClip clip, Vector3 pos, float volume = 0f)
    {
        if (instance != null) instance._PlaySound(clip, pos, volume);
    }

    public void _PlaySound(AudioClip clip, Vector3 pos, float volume = 0f)
    {
        if (clip == null) return;
        int idx = GetUnusedAudioSource();
        audioSourceList[idx].transform.position = pos;
        audioSourceList[idx].clip = clip;
        audioSourceList[idx].volume = volume == 0 ? defaultVolume : volume;
        audioSourceList[idx].Play();
    }

    private int GetUnusedAudioSource()
    {
        for (int i = 0; i < audioSourceList.Count; i++) { if (!audioSourceList[i].isPlaying) return i; }
        return 0;
    }
}
