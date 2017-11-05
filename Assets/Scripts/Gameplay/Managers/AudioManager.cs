﻿using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : ExtendedMonoBehaviour {

    public static AudioManager StaticAudioManager;

    Scene currentScene;
    Dictionary<string, int> SceneDictionary;
    Dictionary<int, string> SceneDictionaryIndexes;
    public List<string> SceneNameList;

    bool isAppQuitting = false;

    [Serializable]
    public class MusicBanksClass
    {
        public GameObject MasterBank;
        public GameObject MasterBankStrings;
        public GameObject GreenBank;
        public GameObject BlueBank;

        // This is only used so we can iterate across all banks if needed
        //[HideInInspector]
        public List<GameObject> ActiveSecondaryBanksList;
        public List<GameObject> MasterBankList;
    }
    public MusicBanksClass MusicBanks;

    [Serializable]
    public class MusicClass
    {
        // Keep to the naming convention used for previous params and eventrefs.
        /*
         * To add a new Parameter, create a ParameterInstance variable and a string variable with the name of the parameter.
         * The ParameterInstance is the actual parameter that will be used to set the value of.  The string variable is to hold the parameter name for ease of changing if needed.
         * You have to make the variables public to access them so use [HideInInspector] to keep them out of the inspector
         */
        [Header("Global Music")]
        [HideInInspector]
        public ParameterInstance Music_ResonateParam;
        [HideInInspector]
        public string Music_ResonateParamString = "Resonance_Active";
        [HideInInspector]
        public ParameterInstance Music_SecondaryParam;
        [HideInInspector]
        public string Music_SecondaryParamString = "LevelMusic_Secondary";
        [HideInInspector]
        public ParameterInstance Music_FadeParam;
        [HideInInspector]
        public string Music_FadeParamString = "Fade";

        [Header("Main Menu Music")]
        [EventRef]
        public string Music_MainMenu;
        //Music_MainMenu.stop(STOP_MODE.ALLOWFADEOUT);  should be used before releasing music after game start.  *Currently in Master Bank, Will eventually make a menu bank

        [Header("Green Zone Music")]
        [EventRef]
        public string Music_GreenTrial;
        
        [Header("Blue Zone Music")]
        [EventRef]
        public string Music_BlueTrial;
        [EventRef]
        public string Music_BlueTrialCave;
        [HideInInspector]
        public ParameterInstance Music_OnLandParam;
        [HideInInspector]
        public string Music_OnLandParamString = "On_Land";

        

        // Use a Dictionary so we can call them by name if needed or loop through them for some reason
        public Dictionary<string, EventInstance> ActiveEventInstanceDictionary;
    }
    public MusicClass Music;

    [Serializable]
    public class AmbientSFXClass
    {
        [Header("Green Zone Ambience")]
        [EventRef]
        public string AmbSFX_RainForest;
        [EventRef]
        public string AmbSFX_Wind;

        [Header("Blue Zone Ambience")]
        [EventRef]
        public string AmbSFX_Underwater;

        public Dictionary<string, EventInstance> ActiveAmbientEventInstanceDictionary;
    }
    public AmbientSFXClass AmbientSFX;

    private void Awake()
    {
        if (StaticAudioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            StaticAudioManager = this;
        }
        else if (StaticAudioManager != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start () {
        //InvokeRepeating("DebugDictionary", 0.0f, 5.0f);
        // Setup the initial sound sets
        InitialSetup();

        UpdateCurrentSceneMusic();
    }

    

    void DebugDictionary()
    {
        Debug.Log("Time: " + Time.time + ", Scene: " + SceneManager.GetActiveScene().name + ", Dictionary Count: " + Music.ActiveEventInstanceDictionary.Count);
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}

    private void OnApplicationQuit()
    {
        isAppQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isAppQuitting)
        {
            Debug.Log("Calling from inside OnDestroy()");
            Invoke(DebugDictionary, 0.0f);
            //foreach (GameObject _go in MusicBanks.ActiveBanksList)
            //{
            //    UnloadBankAndRemoveFromtList(_go);
            //} 
        }
    }

    void InitialSetup()
    {
        Debug.Log("AM: " + gameObject.name);
        if (SceneDictionary == null)
        {
            SceneDictionary = new Dictionary<string, int>(); 
        }
        if (SceneDictionaryIndexes == null)
        {
            SceneDictionaryIndexes = new Dictionary<int, string>(); 
        }
        if (SceneNameList == null)
        {
            SceneNameList = new List<string>(); 
        }
        if (MusicBanks.ActiveSecondaryBanksList == null)
        {
            MusicBanks.ActiveSecondaryBanksList = new List<GameObject>(); 
        }
        if (Music.ActiveEventInstanceDictionary == null)
        {
            Music.ActiveEventInstanceDictionary = new Dictionary<string, EventInstance>(); 
        }
        // Load any Master banks and put them in their own list so we dont accidentally remove them and can use them when needed.  These are banks that should ALWAYS be loaded.
        MusicBanks.MasterBank.GetComponent<StudioBankLoader>().Load();
        MusicBanks.MasterBankStrings.GetComponent<StudioBankLoader>().Load();
        MusicBanks.MasterBankList.Add(MusicBanks.MasterBank);
        MusicBanks.MasterBankList.Add(MusicBanks.MasterBankStrings);
    }

    /// <summary>
    /// Use this to Load Banks so that the List always and only contains the Loaded banks
    /// </summary>
    /// <param name="_go"></param>
    void LoadBankAndAddToList(GameObject _go)
    {
        _go.GetComponent<StudioBankLoader>().Load();
        bool isInList = MusicBanks.ActiveSecondaryBanksList.Any(x => x == _go);
        if(!isInList)
            MusicBanks.ActiveSecondaryBanksList.Add(_go);
    }

    void UnloadBankByObject(GameObject _go)
    {
        _go.GetComponent<StudioBankLoader>().Unload();
        bool isInList = MusicBanks.ActiveSecondaryBanksList.Any(x => x == _go);
        if(isInList)
            MusicBanks.ActiveSecondaryBanksList.Remove(_go);
    }

    void UnloadBankByIndex(int _index)
    {
        MusicBanks.ActiveSecondaryBanksList[_index].GetComponent<StudioBankLoader>().Unload();
        MusicBanks.ActiveSecondaryBanksList.RemoveAt(_index);
    }
    
    void UnloadAllSecondaryBanks()
    {
        foreach(GameObject _go in MusicBanks.ActiveSecondaryBanksList)
        {
            _go.GetComponent<StudioBankLoader>().Unload();
        }
        MusicBanks.ActiveSecondaryBanksList.Clear();
    }

    void CreateEventInstanceAndAddToList(string _musicName)
    {
        EventInstance _inst = RuntimeManager.CreateInstance(_musicName);
        Music.ActiveEventInstanceDictionary.Add(_musicName, _inst);
    }

    void RemoveEventInstanceFromMemory(string _musicName)
    {
        StopInstanceImmediate(_musicName);
        Music.ActiveEventInstanceDictionary[_musicName].release();
        Music.ActiveEventInstanceDictionary.Remove(_musicName);
    }

    void RemoveAllEventInstancesFromMemory()
    {
        List<string> _events = new List<string>();
        foreach(KeyValuePair<string, EventInstance> key in Music.ActiveEventInstanceDictionary)
        {
            _events.Add(key.Key);
            //RemoveEventInstanceFromMemory(key.Key);
        }
        foreach(string e in _events)
        {
            RemoveEventInstanceFromMemory(e);
        }
    }

    void SceneChangeClearMusic()
    {
        RemoveAllEventInstancesFromMemory();
        UnloadAllSecondaryBanks();
    }

    void PlayInstance(string _musicName)
    {
        PLAYBACK_STATE play_state;
        Music.ActiveEventInstanceDictionary[_musicName].getPlaybackState(out play_state);
        if (play_state != PLAYBACK_STATE.PLAYING)
        {
            Music.ActiveEventInstanceDictionary[_musicName].start();
        }
    }

    void StopInstanceImmediate(string _musicName)
    {
        Music.ActiveEventInstanceDictionary[_musicName].stop(STOP_MODE.IMMEDIATE);
    }

    void StopInstanceFadeout(string _musicName)
    {
        Music.ActiveEventInstanceDictionary[_musicName].stop(STOP_MODE.ALLOWFADEOUT);
    }

    //void CreateSceneDictionary()
    //{
    //    SceneList list = Resources.Load<SceneList>("ScenesList") as SceneList;
    //    for (int i = 0; i < list.SceneNames.Length; i++)
    //    {
    //        SceneDictionary.Add(list.SceneNames[i], i);
    //        SceneDictionaryIndexes.Add(i, list.SceneNames[i]);
    //    }

    //    foreach (var kvp in SceneDictionary)
    //        SceneNameList.Add(kvp.Key);
    //}

    public void StopAllMusic()
    {
        WithComponent<StudioEventEmitter>(x => x.Stop());
    }

    public void SetBoolTypeAudioParam(string musicName, string paramName, bool isParamOn)
    {
        EventInstance _evi = null;
        if(!Music.ActiveEventInstanceDictionary.TryGetValue(musicName,out _evi))
        {
            Debug.Log(musicName + " is not loaded");
        }
        else
        {
            ParameterInstance _param = null;
            Music.ActiveEventInstanceDictionary[musicName].getParameter(paramName, out _param);
            if (isParamOn)
                _param.setValue(1);
            else
                _param.setValue(0);

            Debug.Log(musicName + " with param: " + paramName + " was set to + " + isParamOn);
        }
    }

    //public void SetMainThemeToSecondaryParam(bool isParamOn)
    //{
    //    var curScene = SceneManager.GetActiveScene().buildIndex;
    //    switch(curScene)
    //    {
    //        case 1:
    //            Music.ActiveEventInstanceDictionary[Music.Music_GreenTrial].getParameter(Music.Music_SecondaryParamString, out Music.Music_SecondaryParam);
    //            break;
    //        case 2:
    //            Music.ActiveEventInstanceDictionary[Music.Music_BlueTrial].getParameter(Music.Music_SecondaryParamString, out Music.Music_SecondaryParam);
    //            break;
    //    }

    //    if (Music.Music_SecondaryParam != null)
    //    {
    //        if (isParamOn)
    //            Music.Music_SecondaryParam.setValue(1);
    //        else
    //            Music.Music_SecondaryParam.setValue(0); 
    //    }
    //}

    public void SetMainThemeResonateActive(bool isParamOn)
    {
        var curScene = SceneManager.GetActiveScene().buildIndex;
        switch (curScene)
        {
            case 1:
                Music.ActiveEventInstanceDictionary[Music.Music_GreenTrial].getParameter(Music.Music_ResonateParamString, out Music.Music_ResonateParam);
                break;
            case 2:
                Music.ActiveEventInstanceDictionary[Music.Music_BlueTrial].getParameter(Music.Music_ResonateParamString, out Music.Music_ResonateParam);
                break;

        }
        if (Music.Music_ResonateParam != null)
        {
            if (isParamOn)
                Music.Music_ResonateParam.setValue(1);
            else
                Music.Music_ResonateParam.setValue(0); 
        }
    }

    public void UpdateCurrentSceneMusic()
    {
        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int sceneIndex = currentScene.buildIndex;

        switch (sceneIndex)
        {
            // Main Menu
            case 0:
                SceneChangeClearMusic();
                CreateEventInstanceAndAddToList(Music.Music_MainMenu);
                PlayInstance(Music.Music_MainMenu);
                break;
            // Green Trial
            case 1:
                SceneChangeClearMusic();
                LoadBankAndAddToList(MusicBanks.GreenBank);
                CreateEventInstanceAndAddToList(Music.Music_GreenTrial);
                PlayInstance(Music.Music_GreenTrial);

                break;
            // Blue Trial
            case 2:
                SceneChangeClearMusic();

                LoadBankAndAddToList(MusicBanks.BlueBank);
                CreateEventInstanceAndAddToList(Music.Music_BlueTrial);
                PlayInstance(Music.Music_BlueTrial);
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                break;
        }
    }

    void StopSong(string songToStop)
    {
        PLAYBACK_STATE stateStop;
        EventInstance songToStopEI = null;

        if (Music.ActiveEventInstanceDictionary.TryGetValue(songToStop, out songToStopEI))
        {
            Music.ActiveEventInstanceDictionary[songToStop].getPlaybackState(out stateStop);

            if (stateStop == PLAYBACK_STATE.PLAYING)
            {
                ParameterInstance param;
                Music.ActiveEventInstanceDictionary[songToStop].getParameter("Fade", out param);
                if (param != null)
                {
                    param.setValue(1);
                }
                else
                {
                    Music.ActiveEventInstanceDictionary[songToStop].stop(STOP_MODE.ALLOWFADEOUT);
                }
            }
        }
        else
            Debug.Log(songToStop + " is not loaded");
    }

    IEnumerator StartSongCoroutine(string songToStart, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        PLAYBACK_STATE stateStart;
        EventInstance songToStartEI = null;

        if (Music.ActiveEventInstanceDictionary.TryGetValue(songToStart, out songToStartEI))
        {
            Music.ActiveEventInstanceDictionary[songToStart].getPlaybackState(out stateStart);
            ParameterInstance param;
            Music.ActiveEventInstanceDictionary[songToStart].getParameter("Fade", out param);
            if (param != null)
            {
                param.setValue(0);
            }
            if (stateStart == PLAYBACK_STATE.STOPPED)
            {
                Music.ActiveEventInstanceDictionary[songToStart].start();
            }
            else if (stateStart == PLAYBACK_STATE.STOPPING)
            {
                Music.ActiveEventInstanceDictionary[songToStart].stop(STOP_MODE.IMMEDIATE);
                Music.ActiveEventInstanceDictionary[songToStart].start();
            }
            else
                Debug.Log(songToStart + " is already playing");
        }
        else
        {
            Debug.Log(songToStart + " does could not be found in the ActiveEventInstanceDictionary, creating it now.");
            CreateEventInstanceAndAddToList(songToStart);
            PlayInstance(songToStart);
        }
        yield return null;
    }

    /// <summary>
    /// Use this Method to change songs within the same scene
    /// </summary>
    /// <param name="songToStop"></param>
    /// <param name="songToStart"></param>
    public void ChangeSong(string songToStop, string songToStart, float timeToStartSong)
    {
        StopSong(songToStop);
        StartCoroutine(StartSongCoroutine(songToStart, timeToStartSong));
    }
}
