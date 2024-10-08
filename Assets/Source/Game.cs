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
        humanSpawner = new HumanSpawner();
        OnGameStateChanged += OnStateChanged;

        InitializeGame();
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

    private void InitializeGame()
    {
        //Creates the IUpdateables list
        updateables = new List<IUpdateable>();
        updateables.Add(cam);

        //Instantiates the HumanSpawner class
        humanSpawner.Spawn(humanSpawns, out List<IUpdateable> addedUpdateables);
        updateables.AddRange(addedUpdateables);

        // put camera on player   
        State = GameState.Menu;
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
        // clean up existing players/enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = enemies.Length - 1; i >= 0; i--)
        {
            Destroy(enemies[i]);
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = players.Length - 1; i >= 0; i--)
        {
            Destroy(players[i]);
        }

        // spawn new ones
        InitializeGame();
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