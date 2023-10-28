using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public ushort PlayerId;
    public string SteamName;
    public string SteamId;

    public bool CompletedLoadIn;

    public GameObject PlayerGameObject;

    public Vector3 SpawnPosition;

    public int TeamId = 0;

    public int CurrentHealth;
    public int MaxHealth;
}
