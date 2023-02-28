using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMultiplier : PowerUp {

    [Header("CUSTOM"), SerializeField]
    private float damageMultiplierAddition;

    public override void Interact(PlayerStats player) {
        base.Interact(player);
        player.damageMultiplier += damageMultiplierAddition;
        Destroy(this.gameObject);
    }
}