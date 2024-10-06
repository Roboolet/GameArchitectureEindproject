using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    public static GameState State { get; set; }

    private List<IUpdateable> updateables;
    private CameraController cam;
    [SerializeField] private SpawnInfo[] humanSpawns;
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
        Transform tf = GameObject.FindGameObjectWithTag("Player").transform;
        cam.cam.transform.position = tf.position;
        cam.cam.transform.parent = tf;

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


    public void StartButton()
    {

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
    Paused
}