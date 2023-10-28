using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.TeamSpawner.MeshCreator;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Team1Spawn;
    public GameObject Team2Spawn;

    private AreaMeshCreator _team1AreaMesh;
    private AreaMeshCreator _team2AreaMesh;

    void Awake()
    {
        _team1AreaMesh = Team1Spawn.GetComponentInChildren<AreaMeshCreator>();
        _team2AreaMesh = Team2Spawn.GetComponentInChildren<AreaMeshCreator>();
    }

    public void GetSpawnPoint(Player player)
    {

        if (player.TeamId == 1)
            player.SpawnPosition = _team1AreaMesh.GetRandomPointInside();
        else if (player.TeamId == 2)
            player.SpawnPosition = _team2AreaMesh.GetRandomPointInside();
    }
}
