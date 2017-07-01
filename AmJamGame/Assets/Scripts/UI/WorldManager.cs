using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;       //Allows us to use Lists. 

public class WorldManager : Singleton<WorldManager>
{
    [SerializeField]
    private LevelHolder levelHolder;
    private string levelHolderPath = "mainLevelHolder";
    [SerializeField]
    private AudioLibrary audioLibrary;
    private string audioLibraryPath = "mainAudioLibrary";

    public SoundManager soundManager;
    public CameraControl cameraControl;

    private int currentLevel = 0;                                  //Current level number
    
    void Awake()
    {
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(this.gameObject);

        InitLevelHolder();
        InitAudioLibrary();
        InitSoundManager();
    }

    private void Start()
    {

      //  soundManager.PlayAmbient();
    }



    void InitSoundManager()
    {
        this.soundManager = SoundManager.Instance;
    }

    void InitLevelHolder()
    {
        if (levelHolder == null)
        {
            levelHolder = Resources.Load(levelHolderPath) as LevelHolder;
        }
    }

    void InitAudioLibrary()
    {
        if (audioLibrary == null)
        {
            audioLibrary = Resources.Load(audioLibraryPath) as AudioLibrary;
        }
    }

    void InitCameraControl()
    {
        if(cameraControl == null)
        {
            cameraControl = Camera.main.GetComponent<CameraControl>();
        }
    }

    //Initializes the game for each level.
    public void InitGame()
    {
        InitLevelHolder();
        levelHolder.LoadLevel(currentLevel);

    }

    public void LoadNextLevel()
    {
        InitLevelHolder();
        currentLevel++;
        levelHolder.LoadLevel(currentLevel);

    }

    public AudioLibrary GetAudioLibrary()
    {
        InitAudioLibrary();
        return audioLibrary;
    }

    public void ScreenShake()
    {
        InitCameraControl();
        cameraControl.TestShake();
    }
}


    