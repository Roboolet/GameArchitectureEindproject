using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateable
{
    //Provides substitute for lack of update functions

    public void PumpedUpdate();

    public void PumpedFixedUpdate();
}
