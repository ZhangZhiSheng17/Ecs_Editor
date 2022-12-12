using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Effects : SkillBase
{
    public GameObject gameClip;
    Player player;

    ParticleSystem particleSystem;

    GameObject obj;
    public Skill_Effects(Player _player)
    {
        player = _player;

    }

    public void SetGameClip(GameObject _audioClip)
    {
        gameClip = _audioClip;
        if (gameClip.GetComponent<ParticleSystem>())
        {
            obj = GameObject.Instantiate(gameClip, player.effectsparent);
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
        name = _audioClip.name;
    }

    public override void Play()
    {
        base.Play();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

    }
    public override void Init()
    {
        if (gameClip.GetComponent<ParticleSystem>())
        {
            particleSystem = obj.GetComponent<ParticleSystem>();
            particleSystem.Stop();
        }
    }
    public override void Stop()
    {
        base.Play();
        if (particleSystem != null)
            particleSystem.Stop();

    }
}
