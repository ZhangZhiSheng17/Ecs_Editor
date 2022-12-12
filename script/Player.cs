using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Xml;
using System.IO;
using Newtonsoft.Json;

public class Player : MonoBehaviour
{
    public Dictionary<string, List<SkillBase>> skillsList = new Dictionary<string, List<SkillBase>>();

    RuntimeAnimatorController controller;

    public AnimatorOverrideController overrideController;

    public Transform effectsparent;

    AudioSource audioSource;
    //Player

    Animator anim;
    /*    public float animSpeed = 1;

        public float AnimSpeed
        {
            set
            {
                if (anim != null)
                {
                    anim.speed = value;
                }
                animSpeed = value;
            }
            get

            {
                return animSpeed;
            }

        }*/
    public void InitData()
    {
        overrideController = new AnimatorOverrideController();
        controller = Resources.Load<RuntimeAnimatorController>("Player");
        overrideController.runtimeAnimatorController = controller;
        anim.runtimeAnimatorController = overrideController;
        audioSource = gameObject.AddComponent<AudioSource>();
        effectsparent = transform.Find("effectsparent");
        //gameObject.name = path;
        //LoadAllSkill();
    }

    public void play()
    {
        //根据条件判断播放不同的组件  遍历   skillsList  item.player
        _Anim.Play();
        //声音的
        //特效的

    }

    private Skill_Anim _Anim;
    private Skill_Audio _Aduio;
    private Skill_Effects _Effect;

    public void SetData(string skillName)
    {
        List<SkillXml> skillList = GameData.Instance.GetSkillsByRoleName("Teddy");
        //Debug.Log(skillList[0]);
        foreach (var item in skillList)
        {
            if (item.name == skillName)
            {
                foreach (var ite in item.skills)
                {
                    foreach (var it in ite.Value)
                    {
                        if (ite.Key.Equals("动画"))
                        {
                            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameDate/Anim/" + it + ".anim");
                            if (_Anim == null) _Anim = new Skill_Anim(this);
                            _Anim.SetAnimClip(clip);
                            //skillsList[item.name].Add(_Anim);
                        }
                        else if (ite.Key.Equals("音效"))
                        {
                            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameDate/Audio/" + it + ".mp3");
                            if (_Aduio == null) _Aduio = new Skill_Audio(this);
                            _Aduio.SetAnimClip(clip);
                            //skillsList[item.name].Add(_Anim);
                        }
                        else if (ite.Key.Equals("特效"))
                        {
                            GameObject clip = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameDate/Effect/Skill/" + it + ".prefab");
                            if(_Effect == null) _Effect = new Skill_Effects(this);
                            _Effect.SetGameClip(clip);
                            //skillsList[item.name].Add(_Anim);
                        }
                    }
                }
            }
        }

    }


    public static Player Init(string path)
    {

        if (path != null)
        {
            string str = "Assets/aaa/" + path + ".prefab";
            GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(str);
            if (obj != null)
            {
                Player player = Instantiate(obj).GetComponent<Player>();
                player.overrideController = new AnimatorOverrideController();
                player.controller = Resources.Load<RuntimeAnimatorController>("Player");
                player.overrideController.runtimeAnimatorController = player.controller;
                player.anim.runtimeAnimatorController = player.overrideController;
                player.audioSource = player.gameObject.AddComponent<AudioSource>();
                player.effectsparent = player.transform.Find("effectsparent");
                player.gameObject.name = path;
                player.LoadAllSkill();
                return player;
            }
        }
        return null;
    }
    void LoadAllSkill()
    {
        if (File.Exists("Assets/" + gameObject.name + ".txt"))
        {
            string str = File.ReadAllText("Assets/" + gameObject.name + ".txt");
            List<SkillXml> skills = JsonConvert.DeserializeObject<List<SkillXml>>(str);
            foreach (var item in skills)
            {
                skillsList.Add(item.name, new List<SkillBase>());
                foreach (var ite in item.skills)
                {
                    foreach (var it in ite.Value)
                    {
                        if (ite.Key.Equals("动画"))
                        {
                            AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/GameDate/Anim/" + it + ".anim");
                            Skill_Anim _Anim = new Skill_Anim(this);
                            _Anim.SetAnimClip(clip);
                            skillsList[item.name].Add(_Anim);
                        }
                        else if (ite.Key.Equals("音效"))
                        {
                            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/GameDate/Audio/" + it + ".mp3");
                            Skill_Audio _Anim = new Skill_Audio(this);
                            _Anim.SetAnimClip(clip);
                            skillsList[item.name].Add(_Anim);
                        }
                        else if (ite.Key.Equals("特效"))
                        {
                            GameObject clip = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/GameDate/Effect/Skill/" + it + ".prefab");
                            Skill_Effects _Anim = new Skill_Effects(this);
                            _Anim.SetGameClip(clip);
                            skillsList[item.name].Add(_Anim);
                        }
                    }
                }
            }
        }
    }
    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public List<SkillBase> GetSkill(string skillName)
    {
        if (skillsList.ContainsKey(skillName))
        {
            return skillsList[skillName];
        }
        return null;
    }

    public List<SkillBase> AddNewSkill(string newSkillName)
    {
        if (skillsList.ContainsKey(newSkillName))
        {
            return skillsList[newSkillName];
        }
        skillsList.Add(newSkillName, new List<SkillBase>());
        return skillsList[newSkillName];
    }
    public void RevSkill(string newSkillName)
    {
        if (skillsList.ContainsKey(newSkillName))
        {
            skillsList.Remove(newSkillName);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        return;
        List<SkillXml> skills = new List<SkillXml>();
        foreach (var item in skillsList)
        {
            SkillXml skillXml = new SkillXml();
            skillXml.name = item.Key;
            foreach (var ite in item.Value)
            {
                if (ite is Skill_Anim)
                {
                    if (!skillXml.skills.ContainsKey("动画"))
                    {
                        skillXml.skills.Add("动画", new List<string>());
                    }
                    skillXml.skills["动画"].Add(ite.name);
                }
                else if (ite is Skill_Audio)
                {
                    if (!skillXml.skills.ContainsKey("音效"))
                    {
                        skillXml.skills.Add("音效", new List<string>());
                    }
                    skillXml.skills["音效"].Add(ite.name);
                }
                else if (ite is Skill_Effects)
                {
                    if (!skillXml.skills.ContainsKey("特效"))
                    {
                        skillXml.skills.Add("特效", new List<string>());
                    }
                    skillXml.skills["特效"].Add(ite.name);
                }

            }
            skills.Add(skillXml);
        }
        string str = JsonConvert.SerializeObject(skills);
        File.WriteAllText("Assets/" + gameObject.name + ".txt", str);
    }
}
public class SkillXml
{
    public string name;
    public Dictionary<string, List<string>> skills = new Dictionary<string, List<string>>();
}
