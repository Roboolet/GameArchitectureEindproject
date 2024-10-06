using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements
{
    //Starts game
    public void Start()
    {

    }

    //Quits game
    public void Quit()
    {
        Application.Quit();
    }

    //Restarts level
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
