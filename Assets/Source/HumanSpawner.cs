using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSpawner
{
    //Spawns a human
    public void Spawn(SpawnInfo[] _spawnInfo, out List<IUpdateable> _updateables)
    {
        _updateables = new List<IUpdateable>();
        for (int i = 0; i < _spawnInfo.Length; i++)
        {
            // create prefab
            GameObject humanPrefab = Resources.Load<GameObject>("Human");
            GameObject instance = GameObject.Instantiate(humanPrefab,
                _spawnInfo[i].transform.position, Quaternion.identity);

            // create Human class instance
            Human humanInstance = new Human();
            humanInstance.gameObject = instance;
            humanInstance.rb = instance.GetComponent<Rigidbody>();
            _updateables.Add(humanInstance);

            switch (_spawnInfo[i].spawnType)
            {
                case HumanType.Player:
                    PlayerController pc = new PlayerController();
                    pc.Avatar = humanInstance;
                    _updateables.Add(pc);
                    break;

                case HumanType.EnemyBasic:
                    EnemyController ec = new EnemyController();
                    ec.Avatar = humanInstance;
                    _updateables.Add(ec);
                    break;
            }
        }
    }
}

public enum HumanType
{
    Player,
    EnemyBasic
}

[System.Serializable]
public struct SpawnInfo
{
    public HumanType spawnType;
    public Transform transform;
}