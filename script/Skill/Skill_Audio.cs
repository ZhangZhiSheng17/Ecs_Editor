using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Audio : SkillBase
{

    Player player;

    AudioSource audioSource;

    public AudioClip audioClip;

    public Skill_Audio(Player _player)
    {
        player = _player;
        audioSource = player.gameObject.GetComponent<AudioSource>();


    }

    public void SetAnimClip(AudioClip _audioClip)
    {
        audioClip = _audioClip;
        name = audioClip.name;
        audioSource.clip = audioClip;
    }

    public override void Play()
    {
        base.Play();
        audioSource.Play();
    }
    public override void Init()
    {
        audioSource.clip = audioClip;
    }
    public override void Stop()
    {
        base.Play();
        audioSource.Stop();

    }

}
