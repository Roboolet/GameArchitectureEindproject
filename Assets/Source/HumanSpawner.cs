using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner
{
    //Spawns a human
    public void Spawn(SpawnInfo[] _spawnInfo)
    {
        for (int i = 0; i < _spawnInfo.Length; i++)
        {
            CreateHuman(_spawnInfo[i].spawnType);
            //Spawn human at transform
        }
    }

    private void CreateHuman(HumanType _humanType)
    {
        //Creates human
    }

}
public enum HumanType
{
    Player,
    EnemyBasic
}

public struct SpawnInfo
{
    public HumanType spawnType;
    public Transform transform;
}