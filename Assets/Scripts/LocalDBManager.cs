using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LocalDBManager : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static LocalDBManager _instance;
    public static LocalDBManager Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(LocalDBManager)) as LocalDBManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = ".json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filepath = Application.persistentDataPath + GameDataFileName;

        if(File.Exists(filepath))
        {
            Debug.Log("불러오기 성공!");
            string FromJsonData = File.ReadAllText(filepath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");

            _gameData = new GameData();
        }
    }

    public int SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
        return 0;
    }
    
    public void QuestClear(int round)
    {
        if(round == 1)
        {
            _gameData.stage_1 = true;
        }
        else if(round == 2)
        {
            _gameData.stage_2 = true;
        }
        SaveGameData();
    }

    public bool isQuestCleared(int round)
    {
        if (_gameData == null)
        {
            LoadGameData();
        }
        if (round == 1)
        {
            return _gameData.stage_1;
        }
        else if (round == 2)
        {
            return _gameData.stage_2;
        }
        else return false;
    }

    public void Reset()
    {
        _gameData.isSaved = false;
        _gameData.isFinn = false;
        _gameData.stage_1 = false;
        _gameData.stage_2 = false;
        _gameData.BGMSound = 0;
        _gameData.EffectSound = 0;
        SaveGameData();
    }

    public bool returnIsSaved()
    {
        return _gameData.isSaved;
    }
    public void isSave()
    {
        _gameData.isSaved = true;
    }
    private void OnApplicationQuit()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
