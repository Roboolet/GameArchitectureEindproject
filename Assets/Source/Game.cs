using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    private List<IUpdateable> updateables;
    private CameraController cam;
    private static SpawnInfo[] humanSpawns;
    private HumanSpawner humanSpawner;

    void Start()
    {
        //Instantiates the camera class
        cam = new CameraController();

        //Instantiates the camera class
        humanSpawner = new HumanSpawner();

        //Creates the IUpdateables list
        updateables = new List<IUpdateable>();
        updateables.Add(cam);


    }

    public void Update()
    {
        for (int i = 0; i < updateables.Count; i++)
        {
            updateables[i].PumpedUpdate();
        }
            
    }

    public void FixedUpdate()
    {
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
    Paused
}