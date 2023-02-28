using System.Collections.Generic;

public class PlayerData {
    public string steamName;
    public string steamID;
    public int clientId;
    public int playerLevel;
    public int battlePassLevel;
    public int currency;

    public List<WeaponLoadouts> weaponLoadouts = new List<WeaponLoadouts>();
}