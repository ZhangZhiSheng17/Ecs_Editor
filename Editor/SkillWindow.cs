using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillWindow : EditorWindow
{
    Player player;
    List<SkillBase> skills;

    float currSpeed = 1;
    public void SetInitSkill(List<SkillBase> _skills, Player _player)
    {
        player = _player;
        // player.AnimSpeed = 1;
        currSpeed = 1;
        skills = _skills;
    }

    string[] skillComponent = new string[] { "null", "动画", "声音", "特效" };
    int skillComponentIndex = 0;
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("播放"))
        {
            foreach (var item in skills)
            {
                item.Play();
            }
        }
        if (GUILayout.Button("停止"))
        {
            foreach (var item in skills)
            {
                item.Stop();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("速度");
        float speed = EditorGUILayout.Slider(currSpeed, 0, 5);
        if (speed != currSpeed)
        {
            currSpeed = speed;
            Time.timeScale = currSpeed;
        }

        GUILayout.BeginHorizontal();
        skillComponentIndex = EditorGUILayout.Popup(skillComponentIndex, skillComponent);
        if (GUILayout.Button("添加"))
        {
            switch (skillComponentIndex)
            {
                case 1:
                    skills.Add(new Skill_Anim(player));
                    break;
                case 2:
                    skills.Add(new Skill_Audio(player));
                    break;
                case 3:
                    skills.Add(new Skill_Effects(player));
                    break;
            }
        }
        GUILayout.EndHorizontal();

        ScrollViewPos = GUILayout.BeginScrollView(ScrollViewPos, false, true);
        foreach (var item in skills)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(item.name);
            if (GUILayout.Button("删除"))
            {
                skills.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            if (item is Skill_Anim)
            {
                ShowSkill_Anim(item as Skill_Anim);
            }
            else if (item is Skill_Audio)
            {
                Skill_Audio(item as Skill_Audio);
            }
            else if (item is Skill_Effects)
            {
                Skill_Effects(item as Skill_Effects);
            }
            GUILayout.Space(0.5f);
        }
        GUILayout.EndScrollView();

    }
    Vector2 ScrollViewPos = new Vector2(0, 0);
    void ShowSkill_Anim(Skill_Anim _Anim)
    {
        AnimationClip animClip = EditorGUILayout.ObjectField(_Anim.animClip, typeof(AnimationClip), false) as AnimationClip;
        if (_Anim.animClip != animClip)
        {
            _Anim.SetAnimClip(animClip);
        }
    }

    void Skill_Audio(Skill_Audio _Audio)
    {
        AudioClip audioClip = EditorGUILayout.ObjectField(_Audio.audioClip, typeof(AudioClip), false) as AudioClip;
        if (_Audio.audioClip != audioClip)
        {
            _Audio.SetAnimClip(audioClip);
        }
    }

    void Skill_Effects(Skill_Effects _Effects)
    {
        GameObject gameClip = EditorGUILayout.ObjectField(_Effects.gameClip, typeof(GameObject), false) as GameObject;
        if (_Effects.gameClip != gameClip)
        {
            _Effects.SetGameClip(gameClip);
        }
    }
}
