#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public partial class EffectEditor : EditorWindow
{
    #region Variable

    List<GUIContent> _items = null;
    Vector2 _scrollVector;
    bool _isListCreated = false;
    GUIStyle _listBoxStyle = new GUIStyle();
    string _newListInfo = string.Empty;
    string _selectedItemName = string.Empty;
    bool   _isListSelected = false;

    #endregion

    #region Function

    void InitList()
    {
        _isListCreated = false;
        _isListSelected = false;
        _newListInfo = string.Empty;
        _selectedItemName = string.Empty;
    }

    void OnGUIList()
    {
        if (true == _isFileLoaded && false == _isListCreated)
        {
            _items = null;
            _items = new List<GUIContent>();

            foreach(KeyValuePair<string, Dictionary<string, EffectEditorCommon.EffectInfo>> child in _fileInfos)
            {
                _items.Add(new GUIContent(child.Key));
            }

            _isListCreated = true;
            _listBoxStyle.fixedWidth = 250f;
        }

        if (true == _isListCreated)
        {
            GUILayout.BeginArea(new Rect(0, 0, 250, 30));
            _newListInfo = GUI.TextField(new Rect(10, 5, 120, 20), _newListInfo);
            if(true == GUI.Button(new Rect(140, 5, 50, 20), "추가"))
            {
                if ("" != _newListInfo || string.Empty != _newListInfo)
                {
                    if (true == CreateNewEffectInfo(_newListInfo))
                    {
                        _items.Add(new GUIContent(_newListInfo));
                    }
                    else
                    {
                        EditorUtility.DisplayDialog(_newListInfo, "이미 리스트에 있습니다!", "확인");
                    }
                }
                else
                {
                    EditorUtility.DisplayDialog(_newListInfo, "모델 이름을 입력 하십시오!", "확인");
                }
            }

            if(true == GUI.Button(new Rect(200, 5, 50, 20), "삭제"))
            {
                if (true == EditorUtility.DisplayDialog(_newListInfo, "삭제 하시겠습니까?", "확인"))
                {
                    for(int i = 0; i < _items.Count; ++i)
                    {
                        if(_newListInfo == _items[i].text)
                        {
                            DeleteEffectInfo(_newListInfo);
                            _items.Remove(_items[i]);
                            _isListSelected = false;
                            _newListInfo = string.Empty;
                            InitListDetail();
                            break;
                        }
                    }
                }
            }

            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(0, 30, 250, 770));
            GUILayout.BeginHorizontal(GUI.skin.box);
            _scrollVector = GUILayout.BeginScrollView(_scrollVector, _listBoxStyle);

            for (int i = 0; i < _items.Count; ++i)
            {
                if (true == GUILayout.Button(_items[i]))
                {
                    _newListInfo = _items[i].text;
                    _selectedItemName = _newListInfo;
                    _isListSelected = true;
                    InitListDetail();
                }
            }
            GUILayout.EndScrollView();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }

    #endregion
}

#endif