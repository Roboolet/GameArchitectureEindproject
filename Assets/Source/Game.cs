using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    private IUpdateable[] updateables;
    private CameraController cam;

    void Start()
    {
        cam = new CameraController();
        updateables = new IUpdateable[1];
        updateables[0] = cam;
    }

    public void Update()
    {
        for (int i = 0; i < updateables.Length; i++)
        {
            updateables[i].PumpedUpdate();
        }
            
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < updateables.Length; i++)
        {
            updateables[i].PumpedFixedUpdate();
        }
    }

    private void CreateHuman(HumanType _humanType) 
    {

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

public enum HumanType
{
    Player,
    EnemyBasic
}

public enum GameState
{
    Menu,
    Playing,
    Paused
}