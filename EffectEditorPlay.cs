#if UNITY_EDITOR

using UnityEngine;
using System.Collections;

public partial class EffectEditorPlay : MonoBehaviour
{

    #region Singleton

    private static EffectEditorPlay m_Inst;
    public static EffectEditorPlay PTR
    {
        get
        {
            if (null == m_Inst)
                m_Inst = FindObjectOfType(typeof(EffectEditorPlay)) as EffectEditorPlay;

            return m_Inst;
        }
    }

    #endregion

    #region Define

    const string EFFECT_FORDER_DIR = "Effect/";
    const string HERO_FOLDER_DIR = "Model/Hero/";
    const string NEPPY_FOLDER_DIR = "Model/Neppy/";
    const string SOUND_FOLDER_DIR = "Sound/Battle/Once/";

    Vector3 CREATE_OBJ_POS = new Vector3(10f, 0f, -12f);
    Vector3 CREATE_OBJ_ROT = new Vector3(0f, -90f, 0f);

    #endregion

    #region Variable

    public GameObject TestTarget = null;
    public Transform SoundParent = null;

    private GameObject _testObj = null;
    private GameObject _startEffect = null;
    private GameObject _startSound = null;
    private GameObject _projectileEffect = null;
    private GameObject _damageEffect = null;
    private GameObject _damageSound = null;
    private EffectEditorCommon.EffectInfo _effectInfo = null;
    private float _damageKeyTime = 0f;
    private AniCommon.AniInfo _aniInfo = null;
    private float _attackSoundDelay = 0f;

    #endregion

    #region Function

    void OnDisable()
    {
        DeleteCharacter();
        DeleteAllEffect();
    }

    public void DeleteAll()
    {
        DeleteCharacter();
        DeleteAllEffect();
    }

    void DeleteCharacter()
    {
        if (null != _testObj) DestroyImmediate(_testObj);
        _testObj = null;
    }

    void DeleteAllEffect()
    {
        if (null != _startEffect) DestroyImmediate(_startEffect);
        if (null != _projectileEffect) DestroyImmediate(_projectileEffect);
        if (null != _damageEffect) DestroyImmediate(_damageEffect);
        if (null != _startSound) DestroyImmediate(_startSound);
        if (null != _damageSound) DestroyImmediate(_damageSound);

        _startEffect = null;
        _projectileEffect = null;
        _damageEffect = null;
        _effectInfo = null;
        _startSound = null;
        _damageSound = null;
    }

    public void Play(EffectEditorCommon.EffectInfo info, AniCommon.eType aniType, bool isSelectedHero)
    {
        if (null != LoadAniEditorData.PTR && false == LoadAniEditorData.PTR.IsLoaded)
        {
            GlobalFunc.SetDebugLog("[Error]Ani Data Loading...");
            return;
        }

        if (null != _testObj && _testObj.transform.name != info.Name)
        {
            DeleteCharacter();
        }

        StopCoroutine("IUpdateDamageKey");
        StopCoroutine("IUpdateProjectileDelayTime");
        StopCoroutine("IUpdate");
        DeleteAllEffect();

        _damageKeyTime = 0f;

        AniCommon.eType aniModelType = AniCommon.eType.NONE;
        switch (aniType)
        {
            case AniCommon.eType.HERO: aniModelType = AniCommon.eType.HERO; break;
            case AniCommon.eType.NEPPY: aniModelType = AniCommon.eType.NEPPY; break;
        }
        _aniInfo = LoadAniEditorData.PTR.GetAniInfo(aniModelType, info.Name);

        if (null == _testObj)
        {
            string modeldir = string.Empty;
            switch (aniType)
            {
                case AniCommon.eType.HERO: 
                    modeldir = HERO_FOLDER_DIR;
                    string property = info.Name[info.Name.Length - 2].ToString() + info.Name[info.Name.Length - 1].ToString();
                    switch (property)
                    {
                        case "_f":
                        case "_w":
                        case "_t":
                        case "_l":
                        case "_d":
                            string modelName = string.Empty;
                            for (int i = 0; i < info.Name.Length - 2; ++i) modelName += info.Name[i];
                            modeldir += modelName;
                            break;

                        default:
                            modeldir += info.Name;
                            break;
                    }             
                    break;
                case AniCommon.eType.NEPPY: modeldir = NEPPY_FOLDER_DIR + info.Name; break;
            }

            _testObj = Resources.Load(modeldir, typeof(GameObject)) as GameObject;
            if (null != _testObj)
            {
                _testObj = Instantiate(_testObj, Vector3.zero, Quaternion.identity) as GameObject;
                if (null != _testObj)
                {
                    _testObj.transform.name = info.Name;
                    _testObj.transform.localPosition = CREATE_OBJ_POS;
                    _testObj.transform.localRotation = Quaternion.Euler(CREATE_OBJ_ROT);
                    _testObj.transform.localScale = Vector3.one;

                    //Attach Ani
                    if (null != LoadAniEditorData.PTR)
                    {
                        if (null != _aniInfo)
                        {
                            Animation ani = null;
                            if (true == isSelectedHero)
                            {
                                ani = _testObj.GetComponent<Animation>();
                                MaterialFinder mat = _testObj.GetComponent<MaterialFinder>();
                                if (null != mat)
                                {
                                    Material changeMat = Resources.Load("Model/Hero/Materials/" + info.Name, typeof(Material)) as Material;
                                    if (null != changeMat) mat.SetMaterials(changeMat);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < _testObj.transform.childCount; ++i)
                                {
                                    ani = _testObj.transform.GetChild(i).GetComponent<Animation>();
                                    if (null != ani) break;
                                }
                            }

                            if (null != ani)
                            {
                                AnimationClip clip = LoadAniEditorData.PTR.GetAniClip(aniModelType, _aniInfo.Attack);
                                if (null != clip) ani.AddClip(clip, _aniInfo.Attack);
                                clip = LoadAniEditorData.PTR.GetAniClip(aniModelType, _aniInfo.Skill_1);
                                if (null != clip) ani.AddClip(clip, _aniInfo.Skill_1);
                                clip = LoadAniEditorData.PTR.GetAniClip(aniModelType, _aniInfo.Skill_2);
                                if (null != clip) ani.AddClip(clip, _aniInfo.Skill_2);
                                clip = LoadAniEditorData.PTR.GetAniClip(aniModelType, _aniInfo.Skill_3);
                                if (null != clip) ani.AddClip(clip, _aniInfo.Skill_3);
                                clip = LoadAniEditorData.PTR.GetAniClip(aniModelType, _aniInfo.Skill_4);
                                if (null != clip) ani.AddClip(clip, _aniInfo.Skill_4);
                            }
                        }
                    }
                }
            }
        }

        //create effect
        if (null != _testObj)
        {
            if (null != _aniInfo)
            {
                Animation ani = null;
                if (true == isSelectedHero) ani = _testObj.GetComponent<Animation>();
                else
                {
                    for (int i = 0; i < _testObj.transform.childCount; ++i )
                    {
                        ani = _testObj.transform.GetChild(i).GetComponent<Animation>();
                        if (null != ani) break;
                    }
                }

                if (null != ani)
                {
                    ani.Stop();
                    string clipName = string.Empty;
                    _attackSoundDelay = 0f;
                    switch (info.SkillName)
                    {
                        case "attack": clipName = _aniInfo.Attack; _damageKeyTime = _aniInfo.AttackKeyFrame; _attackSoundDelay = _damageKeyTime; break;
                        case "skill_1": clipName = _aniInfo.Skill_1; _damageKeyTime = _aniInfo.Skill_1_KeyFrame; break;
                        case "skill_2": clipName = _aniInfo.Skill_2; _damageKeyTime = _aniInfo.Skill_2_KeyFrame; break;
                        case "skill_3": clipName = _aniInfo.Skill_3; _damageKeyTime = _aniInfo.Skill_3_KeyFrame; break;
                        case "skill_4": clipName = _aniInfo.Skill_4; _damageKeyTime = _aniInfo.Skill_4_KeyFrame; break;
                    }
                    if (null != ani.GetClip(clipName)) ani.Play(clipName);
                }
            }

            _effectInfo = info;
            CreateStartEffect(info);
            if ((int)EffectCommon.ePropertyType.PROJECTILE == info.PropertyTypeIndex) StartCoroutine("IUpdateProjectileDelayTime");
            else StartCoroutine("IUpdateDamageKey");
        }
    }

    void CreateStartEffect(EffectEditorCommon.EffectInfo info)
    {
        float posX = 0f;
        float posY = 0f;
        float posZ = 0f;
        float scaleX = 1f;
        float scaleY = 1f;
        float scaleZ = 1f;

        _startEffect = Resources.Load(EFFECT_FORDER_DIR + info.EffectName, typeof(GameObject)) as GameObject;
        if (null != _startEffect)
        {
            _startEffect = Instantiate(_startEffect, Vector3.zero, Quaternion.identity) as GameObject;
            if (null != _startEffect)
            {
                Transform startPos = _testObj.transform.FindChild(info.DummyPosType.ToLower());
                if (null == startPos) startPos = _testObj.transform.GetChild(0).FindChild(info.DummyPosType.ToLower());
                if (null != startPos)
                {
                    _startEffect.transform.name = info.EffectName;
                    _startEffect.transform.parent = startPos;
                    if ("root" == startPos.name)
                    {
                        float.TryParse(info.DummyPosX, out posX);
                        float.TryParse(info.DummyPosY, out posY);
                        float.TryParse(info.DummyPosZ, out posZ);

                        _startEffect.transform.localPosition = EffectCommon.EffectMath.ChangeRelativeCoordinate(_startEffect.transform, new Vector3(posX, posY, posZ));
                    }
                    else _startEffect.transform.localPosition = Vector3.zero;

                    float.TryParse(info.DummyScaleX, out scaleX);
                    float.TryParse(info.DummyScaleY, out scaleY);
                    float.TryParse(info.DummyScaleZ, out scaleZ);
                    _startEffect.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

                    _startEffect.transform.localRotation = Quaternion.identity;
                }
            }
        }
        _startSound = Resources.Load(SOUND_FOLDER_DIR + info.EffectSoundName) as GameObject;
        if (null != _startSound)
        {
            _startSound = Instantiate(_startSound, Vector3.zero, Quaternion.identity) as GameObject;
            if (null != _startSound)
            {
                _startSound.transform.parent = SoundParent;
                if (0f != _attackSoundDelay) _startSound.GetComponent<AudioSource>().PlayDelayed(_attackSoundDelay);
                _startSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    void CreateDamageEffect(EffectEditorCommon.EffectInfo info)
    {
        _damageEffect = Resources.Load(EFFECT_FORDER_DIR + info.DamageEffectName, typeof(GameObject)) as GameObject;
        if (null != _damageEffect)
        {
            _damageEffect = Instantiate(_damageEffect, Vector3.zero, Quaternion.identity) as GameObject;
            if (null != _damageEffect)
            {
                AniPlay ani = null;
                switch (info.ResultTypeIndex)
                {
                    case (int)EffectCommon.eResultType.SELF:
                        ani = _testObj.GetComponent<AniPlay>();
                        break;

                    default:
                        ani = TestTarget.GetComponent<AniPlay>();
                        break;
                }
                if (null != ani)
                {
                    float scaleX = 1f; float scaleY = 1f; float scaleZ = 1f;
                    float.TryParse(info.DamageScaleX, out scaleX);
                    float.TryParse(info.DamageScaleY, out scaleY);
                    float.TryParse(info.DamageScaleZ, out scaleZ);

                    _damageEffect.transform.parent = ani.DummyDamage;
                    _damageEffect.transform.localPosition = Vector3.zero;
                    _damageEffect.transform.localRotation = Quaternion.identity;
                    _damageEffect.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                }
                else DestroyImmediate(_damageEffect);
            }
        }

        _damageSound = Resources.Load(SOUND_FOLDER_DIR + info.DamageEffectSoundName) as GameObject;
        if (null != _damageSound)
        {
            _damageSound = Instantiate(_damageSound, Vector3.zero, Quaternion.identity) as GameObject;
            if (null != _damageSound)
            {
                _damageSound.transform.parent = SoundParent;
                _damageSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    IEnumerator IUpdateDamageKey()
    {
        float accumTime = 0f;
        while (true)
        {
            yield return null;
            accumTime = Mathf.Clamp(accumTime + Time.deltaTime, 0f, _damageKeyTime);
            if (_damageKeyTime == accumTime)
            {
                ////feeltest
                //GlobalFunc.SetDebugLog("Damage Key : " + _damageKeyTime.ToString());
                ////~feeltest
                if (null != _effectInfo) CreateDamageEffect(_effectInfo);
                break;
            }
        }
    }

    void Update()
    {
        if (null != _startEffect) _startEffect.SetActive(true);
        if (null != _projectileEffect) _projectileEffect.SetActive(true);
        if (null != _damageEffect) _damageEffect.SetActive(true);
    }

    #endregion
}

#endif