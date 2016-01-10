#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class EffectEditor : EditorWindow
{
    #region Variable

    Dictionary<string, Dictionary<string, EffectEditorCommon.EffectInfo>> _fileInfos;
    bool _isFileLoaded = false;
    bool _isFileHeroClicked = false;
    bool _isFileNeppyClicked = false;
    string _fileDir = string.Empty;

    #endregion

    #region Function
    
    void InitFileIO()
    {
        if(null != _fileInfos) _fileInfos.Clear();
        _fileInfos = null;
        _isFileHeroClicked = false;
        _isFileNeppyClicked = false;
        _fileDir = string.Empty;
        _isFileLoaded = false;
    }

    void OnGUIFileIO()
    {
        if(false == _isFileLoaded)
        {
            if(false == _isFileHeroClicked && false == _isFileNeppyClicked)
            {
                if (true == GUI.Button(new Rect(200, 100, 400, 150), "영웅 정보 불러오기"))
                {
                    _isFileHeroClicked = true;
                    _isFileNeppyClicked = false;
                }

                if (true == GUI.Button(new Rect(200, 500, 400, 150), "네삐 정보 불러오기"))
                {
                    _isFileNeppyClicked = true;
                    _isFileHeroClicked = false;
                }
            }

            if(true == _isFileHeroClicked || true == _isFileNeppyClicked)
            {
                _fileDir = System.IO.Path.Combine(Application.dataPath, "Resources/EditData/Effect");
                DirectoryInfo di = new DirectoryInfo(_fileDir);
                if (di.Exists == false)
                    di.Create();

                if (true == _isFileHeroClicked) _fileDir += EffectEditorCommon.FileDir.HeroInfo;
                else _fileDir += EffectEditorCommon.FileDir.NeppyInfo;

                using (StreamReader sr = new StreamReader(_fileDir, System.Text.Encoding.UTF8))
                {
                    string prevName = string.Empty;
                    _fileInfos = null;
                    _fileInfos = new Dictionary<string, Dictionary<string, EffectEditorCommon.EffectInfo>>();
                    Dictionary<string, EffectEditorCommon.EffectInfo> info = new Dictionary<string, EffectEditorCommon.EffectInfo>();
                    EffectEditorCommon.EffectInfo detailInfo = null;

                    string readData = sr.ReadToEnd();
                    string[] readDataList = Regex.Split(readData, "\n");
                    bool _isDiffVersion = false;
                    
                    for (int i = 0; i < readDataList.Length; ++i)
                    {
                        string[] list = Regex.Split(readDataList[i], "\t");
                        for (int j = 0; j < list.Length; ++j)
                        {
                            string replace = list[j];
                            list[j] = replace.Replace("\r", "");
                        }

                        if (i == 0)
                        {
                            float ver = 0f;
                            if ("" != list[0]) float.TryParse(list[0], out ver);

                            if(0f != ver)
                            {
                                if (_version > ver)
                                {
                                    _isDiffVersion = true;
                                }
                                else
                                {
                                    _version = ver;
                                    _isDiffVersion = false;
                                }
                            }
                        }
                        else if(i > 0 )
                        {
                            if (prevName == string.Empty)
                            {
                                if (true == _isDiffVersion) prevName = list[(int)EffectEditorCommon.ePrevFileOrder.NAME];
                                else prevName = list[(int)EffectEditorCommon.eCurFileOrder.NAME];
                            }
                            else if (prevName != list[(int)EffectEditorCommon.eCurFileOrder.NAME])
                            {
                                _fileInfos.Add(prevName, info);
                                if (true == _isDiffVersion) prevName = list[(int)EffectEditorCommon.ePrevFileOrder.NAME];
                                else prevName = list[(int)EffectEditorCommon.eCurFileOrder.NAME];
                                info = new Dictionary<string, EffectEditorCommon.EffectInfo>();
                            }

                            if (string.Empty == list[(int)EffectEditorCommon.eCurFileOrder.NAME] || "" == list[(int)EffectEditorCommon.eCurFileOrder.NAME])
                            {
                                info = null;
                                break;
                            }

                            detailInfo = new EffectEditorCommon.EffectInfo();

                            if(true == _isDiffVersion)
                            {
                                int.TryParse(list[(int)EffectEditorCommon.ePrevFileOrder.DUMMY_POS_TYPE_INDEX], out detailInfo.DummyPosTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.ePrevFileOrder.PROPERTY_TYPE_INDEX], out detailInfo.PropertyTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.ePrevFileOrder.RESULT_TYPE_INDEX], out detailInfo.ResultTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.ePrevFileOrder.START_TYPE_INDEX], out detailInfo.StartTypeIndex);

                                detailInfo.Name = list[(int)EffectEditorCommon.ePrevFileOrder.NAME];
                                detailInfo.EffectName = list[(int)EffectEditorCommon.ePrevFileOrder.EFFECT_NAME];
                                detailInfo.SkillName = list[(int)EffectEditorCommon.ePrevFileOrder.SKILL_NAME];
                                detailInfo.DamageEffectName = list[(int)EffectEditorCommon.ePrevFileOrder.DAMAGE_EFFECT_NAME];
                                detailInfo.DummyPosType = list[(int)EffectEditorCommon.ePrevFileOrder.DUMMY_POS_TYPE];
                                detailInfo.DummyPosX = list[(int)EffectEditorCommon.ePrevFileOrder.DUMMY_POS_X];
                                detailInfo.DummyPosY = list[(int)EffectEditorCommon.ePrevFileOrder.DUMMY_POS_Y];
                                detailInfo.DummyPosZ = list[(int)EffectEditorCommon.ePrevFileOrder.DUMMY_POS_Z];
                                detailInfo.StartType = list[(int)EffectEditorCommon.ePrevFileOrder.START_TYPE];
                                detailInfo.StartLoopTime = list[(int)EffectEditorCommon.ePrevFileOrder.START_LOOP_TIME];
                                detailInfo.PropertyType = list[(int)EffectEditorCommon.ePrevFileOrder.PROPERTY_TYPE];
                                detailInfo.ProjectileName = list[(int)EffectEditorCommon.ePrevFileOrder.PROJECTILE_NAME];
                                detailInfo.ProjectilePosX = list[(int)EffectEditorCommon.ePrevFileOrder.PROJECTILE_POS_X];
                                detailInfo.ProjectilePosY = list[(int)EffectEditorCommon.ePrevFileOrder.PROJECTILE_POS_Y];
                                detailInfo.ProjectilePosZ = list[(int)EffectEditorCommon.ePrevFileOrder.PROJECTILE_POS_Z];
                                detailInfo.BezierPosX = list[(int)EffectEditorCommon.ePrevFileOrder.BEZIER_X];
                                detailInfo.BezierPosY = list[(int)EffectEditorCommon.ePrevFileOrder.BEZIER_Y];
                                detailInfo.BezierPosZ = list[(int)EffectEditorCommon.ePrevFileOrder.BEZIER_Z];
                                detailInfo.ResultType = list[(int)EffectEditorCommon.ePrevFileOrder.RESULT_TYPE];
                                detailInfo.EffectSoundName = list[(int)EffectEditorCommon.ePrevFileOrder.EFFECT_SOUND_NAME];
                                detailInfo.DamageEffectSoundName = list[(int)EffectEditorCommon.ePrevFileOrder.DAMAGE_EFFECT_SOUND_NAME];
                                detailInfo.ProjectileDelayTime = list[(int)EffectEditorCommon.ePrevFileOrder.PROJECTILE_DELAY_TIME];
                                detailInfo.DamageScaleX = "1";
                                detailInfo.DamageScaleY = "1";
                                detailInfo.DamageScaleZ = "1";
                                detailInfo.DummyScaleX = "1";
                                detailInfo.DummyScaleY = "1";
                                detailInfo.DummyScaleZ = "1";
                            }
                            else
                            {
                                int.TryParse(list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_POS_TYPE_INDEX], out detailInfo.DummyPosTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.eCurFileOrder.PROPERTY_TYPE_INDEX], out detailInfo.PropertyTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.eCurFileOrder.RESULT_TYPE_INDEX], out detailInfo.ResultTypeIndex);
                                int.TryParse(list[(int)EffectEditorCommon.eCurFileOrder.START_TYPE_INDEX], out detailInfo.StartTypeIndex);

                                detailInfo.Name = list[(int)EffectEditorCommon.eCurFileOrder.NAME];
                                detailInfo.EffectName = list[(int)EffectEditorCommon.eCurFileOrder.EFFECT_NAME];
                                detailInfo.SkillName = list[(int)EffectEditorCommon.eCurFileOrder.SKILL_NAME];
                                detailInfo.DamageEffectName = list[(int)EffectEditorCommon.eCurFileOrder.DAMAGE_EFFECT_NAME];
                                detailInfo.DummyPosType = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_POS_TYPE];
                                detailInfo.DummyPosX = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_POS_X];
                                detailInfo.DummyPosY = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_POS_Y];
                                detailInfo.DummyPosZ = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_POS_Z];
                                detailInfo.StartType = list[(int)EffectEditorCommon.eCurFileOrder.START_TYPE];
                                detailInfo.StartLoopTime = list[(int)EffectEditorCommon.eCurFileOrder.START_LOOP_TIME];
                                detailInfo.PropertyType = list[(int)EffectEditorCommon.eCurFileOrder.PROPERTY_TYPE];
                                detailInfo.ProjectileName = list[(int)EffectEditorCommon.eCurFileOrder.PROJECTILE_NAME];
                                detailInfo.ProjectilePosX = list[(int)EffectEditorCommon.eCurFileOrder.PROJECTILE_POS_X];
                                detailInfo.ProjectilePosY = list[(int)EffectEditorCommon.eCurFileOrder.PROJECTILE_POS_Y];
                                detailInfo.ProjectilePosZ = list[(int)EffectEditorCommon.eCurFileOrder.PROJECTILE_POS_Z];
                                detailInfo.BezierPosX = list[(int)EffectEditorCommon.eCurFileOrder.BEZIER_X];
                                detailInfo.BezierPosY = list[(int)EffectEditorCommon.eCurFileOrder.BEZIER_Y];
                                detailInfo.BezierPosZ = list[(int)EffectEditorCommon.eCurFileOrder.BEZIER_Z];
                                detailInfo.ResultType = list[(int)EffectEditorCommon.eCurFileOrder.RESULT_TYPE];
                                detailInfo.EffectSoundName = list[(int)EffectEditorCommon.eCurFileOrder.EFFECT_SOUND_NAME];
                                detailInfo.DamageEffectSoundName = list[(int)EffectEditorCommon.eCurFileOrder.DAMAGE_EFFECT_SOUND_NAME];
                                detailInfo.ProjectileDelayTime = list[(int)EffectEditorCommon.eCurFileOrder.PROJECTILE_DELAY_TIME];
                                detailInfo.DamageScaleX = list[(int)EffectEditorCommon.eCurFileOrder.DAMAGE_SCALE_X];
                                detailInfo.DamageScaleY = list[(int)EffectEditorCommon.eCurFileOrder.DAMAGE_SCALE_Y];
                                detailInfo.DamageScaleZ = list[(int)EffectEditorCommon.eCurFileOrder.DAMAGE_SCALE_Z];
                                detailInfo.DummyScaleX = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_SCALE_X];
                                detailInfo.DummyScaleY = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_SCALE_Y];
                                detailInfo.DummyScaleZ = list[(int)EffectEditorCommon.eCurFileOrder.DUMMY_SCALE_Z];
                            }
                            
                            info.Add(detailInfo.SkillName, detailInfo);
                        }
                    }

                    sr.Close();
                }

                _isFileLoaded = true;
            }
        }
        else if (true == _isFileLoaded && null != _fileInfos && 0 < _fileInfos.Count)
        {
            GUI.Label(new Rect(270, 5, 130, 20), "총 개수 <" + _items.Count.ToString() + ">");

            //저장
            if (true == GUI.Button(new Rect(670, 5, 70, 20), "저장"))
            {
                Save();
            }
        }

        if (true == _isFileLoaded)
        {
            //메인 화면으로
            if (true == GUI.Button(new Rect(580, 5, 70, 20), "홈"))
            {
                Init();
            }

            //현재 버전
            GUI.Label(new Rect(280, 770, 200, 25), "현재 버전 : " + _version.ToString());
        }
    }

    void Save(bool isActivePopUP = true)
    {
        if(0 < _fileInfos.Count)
        {
            using (StreamWriter sw = new StreamWriter(_fileDir))
            {
                sw.WriteLine(_version.ToString());
                foreach (KeyValuePair<string, Dictionary<string, EffectEditorCommon.EffectInfo>> child in _fileInfos)
                {
                    foreach (KeyValuePair<string, EffectEditorCommon.EffectInfo> childInchild in _fileInfos[child.Key])
                    {
                        sw.WriteLine(
                            _fileInfos[child.Key][childInchild.Key].Name + "\t" +
                            _fileInfos[child.Key][childInchild.Key].SkillName + "\t" +
                            _fileInfos[child.Key][childInchild.Key].EffectName + "\t" +
                            _fileInfos[child.Key][childInchild.Key].EffectSoundName + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DummyPosType + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DummyPosTypeIndex.ToString() + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].DummyPosX) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].DummyPosY) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].DummyPosZ) + "\t" +
                            _fileInfos[child.Key][childInchild.Key].StartType + "\t" +
                            _fileInfos[child.Key][childInchild.Key].StartTypeIndex.ToString() + "\t" +
                            _fileInfos[child.Key][childInchild.Key].StartLoopTime + "\t" +
                            _fileInfos[child.Key][childInchild.Key].PropertyType + "\t" +
                            _fileInfos[child.Key][childInchild.Key].PropertyTypeIndex.ToString() + "\t" +
                            _fileInfos[child.Key][childInchild.Key].ProjectileName + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].ProjectilePosX) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].ProjectilePosY) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].ProjectilePosZ) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].BezierPosX) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].BezierPosY) + "\t" +
                            CheckStringEmpty(_fileInfos[child.Key][childInchild.Key].BezierPosZ) + "\t" +
                            _fileInfos[child.Key][childInchild.Key].ResultType + "\t" +
                            _fileInfos[child.Key][childInchild.Key].ResultTypeIndex.ToString() + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DamageEffectName + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DamageEffectSoundName + "\t" +
                            _fileInfos[child.Key][childInchild.Key].ProjectileDelayTime + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DummyScaleX + "\t" + 
                            _fileInfos[child.Key][childInchild.Key].DummyScaleY + "\t" + 
                            _fileInfos[child.Key][childInchild.Key].DummyScaleZ + "\t" + 
                            _fileInfos[child.Key][childInchild.Key].DamageScaleX + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DamageScaleY + "\t" +
                            _fileInfos[child.Key][childInchild.Key].DamageScaleZ
                            );
                    }
                }
                sw.Close();
            }

            if(true == isActivePopUP) EditorUtility.DisplayDialog("저장 완료", _fileDir, "확인");
        }
        else if(0 == _fileInfos.Count)
        {
            using (StreamWriter sw = new StreamWriter(_fileDir))
            {
                sw.WriteLine("");
                sw.Close();
            }
        }
    }

    string CheckStringEmpty(string str)
    {
        if ("" == str) return "0";
        else return str;
    }

    bool CreateNewEffectInfo(string name)
    {
        bool result = false;
        if(false == _fileInfos.ContainsKey(name))
        {
            Dictionary<string, EffectEditorCommon.EffectInfo> info = new Dictionary<string, EffectEditorCommon.EffectInfo>();
            EffectEditorCommon.EffectInfo detailInfo = null;
            for(int i = 0; i < 5; ++i)
            {
                detailInfo = new EffectEditorCommon.EffectInfo();
                detailInfo.Name = name;
                if (i == 0) detailInfo.SkillName = "attack";
                else detailInfo.SkillName = "skill_" + i.ToString();
                detailInfo.EffectName = string.Empty;
                detailInfo.DummyPosType = EffectEditorCommon.EffectTypes.DummyPosType[0];
                detailInfo.DummyPosX = "0";
                detailInfo.DummyPosY = "0";
                detailInfo.DummyPosZ = "0";
                detailInfo.StartType = EffectEditorCommon.EffectTypes.StartType[0];
                detailInfo.StartLoopTime = "0";
                detailInfo.PropertyType = EffectEditorCommon.EffectTypes.PropertyType[0];
                detailInfo.ProjectileName = string.Empty;
                detailInfo.ProjectilePosX = "0";
                detailInfo.ProjectilePosY = "0";
                detailInfo.ProjectilePosZ = "0";
                detailInfo.BezierPosX = "0";
                detailInfo.BezierPosY = "0";
                detailInfo.BezierPosZ = "0";
                detailInfo.EffectSoundName = string.Empty;
                detailInfo.DamageEffectSoundName = string.Empty;
                detailInfo.ResultType = EffectEditorCommon.EffectTypes.ResultType[0];
                detailInfo.DamageEffectName = string.Empty;
                detailInfo.DummyPosTypeIndex = 0;
                detailInfo.PropertyTypeIndex = 0;
                detailInfo.ResultTypeIndex = 0;
                detailInfo.StartTypeIndex = 0;
                detailInfo.ProjectileDelayTime = "0";
                detailInfo.DummyPosX = "1";
                detailInfo.DummyPosY = "1";
                detailInfo.DummyPosZ = "1";
                detailInfo.DamageScaleX = "1";
                detailInfo.DamageScaleY = "1";
                detailInfo.DamageScaleZ = "1";

                info.Add(detailInfo.SkillName, detailInfo);
            }
            _fileInfos.Add(name, info);
            result = true;
        }
        return result;
    }

    void DeleteEffectInfo(string name)
    {
        if(true == _fileInfos.ContainsKey(name))
        {
            _fileInfos.Remove(name);
            Save(false);
        }
    }

    #endregion
}

#endif