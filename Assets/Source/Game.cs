using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Game : MonoBehaviour
{

    private IUpdateable[] updatables;

    void Start()
    {
        
    }

    public void Update()
    {
        for (int i = 0; i < updatables.Length; i++)
        {
            updatables[i].PumpedUpdate();
        }
            
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < updatables.Length; i++)
        {
            updatables[i].PumpedFixedUpdate();
        }
    }

    private void CreateHuman(HumanType _humanType) 
    {

    }
}

public enum HumanType
{
    Player,
    EnemyBasic
}