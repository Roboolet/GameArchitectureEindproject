using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElements
{
    public void Start()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
