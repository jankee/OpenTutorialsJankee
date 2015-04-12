using UnityEngine;
using System;
using System.Collections.Generic;

public sealed class GameData
{
    //싱글톤 인스턴스를 저장.
    private static volatile GameData uniqueInstance;
    private static object _lock = new System.Object();

    //생성자
    private GameData() { }

    public GamePlayManager gamePlayManager;

    //외부에서 접근할 수 있도록 함.
    public static GameData Instance
    {
        get
        {
            if (uniqueInstance == null)
            {
                //lock으로 지정된 블록안의 코드를 하나의 쓰레드만 접근하도록 한다.
                lock (_lock)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new GameData();
                    }
                }
            }
            return uniqueInstance;
        }
    }
}
