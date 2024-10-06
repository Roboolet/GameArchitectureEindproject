using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    private static GameState _State;
    public static GameState State
    {
        get { return _State; }
        set
        {
            _State = value;
            OnGameStateChanged?.Invoke(value);
        }
    }
    public static Action<GameState> OnGameStateChanged;

    [SerializeField] private SpawnInfo[] humanSpawns;
    [SerializeField] private GameObject startCanvas, gameOverCanvas, winCanvas;

    private List<IUpdateable> updateables;
    private CameraController cam;
    private HumanSpawner humanSpawner;

    void Start()
    {
        //Instantiates the camera class
        cam = new CameraController();

        //Creates the IUpdateables list
        updateables = new List<IUpdateable>();
        updateables.Add(cam);

        //Instantiates the HumanSpawner class
        humanSpawner = new HumanSpawner();
        humanSpawner.Spawn(humanSpawns, out List<IUpdateable> addedUpdateables);
        updateables.AddRange(addedUpdateables);

        // put camera on player   
        OnGameStateChanged += OnStateChanged;
        State = GameState.Menu;

    }

    public void Update()
    {
        if (State != GameState.Playing) { return; }
        for (int i = 0; i < updateables.Count; i++)
        {
            updateables[i].PumpedUpdate();
        }
            
    }

    public void FixedUpdate()
    {
        if (State != GameState.Playing) { return; }
        for (int i = 0; i < updateables.Count; i++)
        {
            updateables[i].PumpedFixedUpdate();
        }
    }

    private void OnStateChanged(GameState _newState)
    {
        switch (_newState)
        {
            case GameState.Playing:
                Transform tf = GameObject.FindGameObjectWithTag("Player").transform;
                cam.cam.transform.position = tf.position;
                cam.cam.transform.parent = tf;

                startCanvas.SetActive(false);
                gameOverCanvas.SetActive(false);
                winCanvas.SetActive(false);
                break;

            case GameState.Paused:
            case GameState.Dead:
                cam.cam.transform.position = Vector3.up * 10;
                cam.cam.transform.parent = null;
                gameOverCanvas.SetActive(true);
                break;

            case GameState.Menu:
                startCanvas.SetActive(true);
                gameOverCanvas.SetActive(false);
                winCanvas.SetActive(false);
                break;

            case GameState.Win:
                cam.cam.transform.position = Vector3.up * 10;
                cam.cam.transform.parent = null;

                winCanvas.SetActive(true);
                break;
        }
    }

    public void StartButton()
    {
        State = GameState.Playing;
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum GameState
{
    Menu,
    Playing,
    Dead,
    Paused,
    Win
}