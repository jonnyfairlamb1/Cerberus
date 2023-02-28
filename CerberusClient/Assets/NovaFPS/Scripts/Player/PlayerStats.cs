using NovaFPS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamageable {

    [System.Serializable]
    public class Events {
        public UnityEvent OnDeath, OnDamage, OnHeal;
    }

    #region variables

    public float health, maxHealth, shield, maxShield, damageMultiplier, healMultiplier; // More power ups coming soon

    [Tooltip("This image shows damage and heal states visually on your screen, you can change the image" +
        "to any you like, but note that color will be overriden by the script"), SerializeField]
    public Image healthStatesEffect;

    [Tooltip(" Color of healthStatesEffect on different actions such as getting hurted or healed"), SerializeField] private Color damageColor, healColor;

    [Tooltip("Time for the healthStatesEffect to fade out"), SerializeField] private float fadeOutTime;

    [Tooltip("Turn on to apply damage on falling from great height")] public bool takesFallDamage;

    [Tooltip("Minimum time the player has to be airborne to take damage"), SerializeField] private float minFallTime;

    [Tooltip("How the damage will increase on landing if the damage on fall is gonna be applied"), SerializeField] private float fallDamageMultiplier;

    [Tooltip("Slider that will display the health on screen"), SerializeField] public Slider healthSlider;

    [Tooltip("Slider that will display the shield on screen"), SerializeField] public Slider shieldSlider;

    private bool applyDamage;

    [HideInInspector] public bool isDead;

    private float timer = 0;

    private PlayerMovement player;

    private PlayerStats stats;

    public Events events;

    #endregion variables

    private void Start() {
        GetAllReferences();
        // Apply basic settings
        health = maxHealth;
        shield = maxShield;
        damageMultiplier = 1;
        healMultiplier = 1;

        //UI Management
        // Might be replaced in future updates
        if (healthSlider != null) healthSlider.maxValue = maxHealth;
        if (shieldSlider != null) shieldSlider.maxValue = maxShield;

        if (shield == 0) shieldSlider.gameObject.SetActive(false);
    }

    private void Update() {
        Controllable = controllable;

        if (stats.isDead) return; // If player is alive, continue

        ManageUI();

        if (health <= 0) Die(); // Die in case we ran out of health

        // Manage fall damage
        if (!takesFallDamage) return;
        ManageFallDamage();
    }

    /// <summary>
    /// Handle basic UI
    /// </summary>
    private void ManageUI() {
        if (healthSlider != null) healthSlider.value = health;
        if (shieldSlider != null) shieldSlider.value = shield;

        if (healthStatesEffect.color != new Color(healthStatesEffect.color.r,
            healthStatesEffect.color.g,
            healthStatesEffect.color.b, 0)) healthStatesEffect.color -= new Color(0, 0, 0, Time.deltaTime * fadeOutTime);
    }

    /// <summary>
    /// Our Player Stats is IDamageable, which means it can be damaged If so, call this method to
    /// damage the player
    /// </summary>
    public void Damage(float _damage) {
        float damage = _damage;
        events.OnDamage.Invoke(); // Invoke the custom event

        if (damage <= shield)
            shield -= damage;
        else {
            damage = damage - shield;
            shield = 0;
            health -= damage;
        }
        // Effect on damage
        healthStatesEffect.color = damageColor;
    }

    public void Heal(float healAmount_) {
        float healAmount = healAmount_ * healMultiplier;
        // If we are full health do not heal Also checks if we have an initial shield or not
        if (maxShield != 0 && shield == maxShield || maxShield == 0 && health == maxHealth) return;

        events.OnHeal.Invoke(); // Invoke our custom event

        if (health + healAmount > maxHealth) // Check if heal exceeds health
        {
            float remaining = maxHealth - health + healAmount;
            health = maxHealth;

            // Check if we have a shield to be healed
            if (maxShield != 0) {
                if (shield + remaining > maxShield) shield = maxShield; // Then we have to apply the remaining heal to our shield
                else shield += remaining;
            }
        } else health += healAmount; // If not just apply your heal

        // effect on heal
        healthStatesEffect.color = healColor;
    }

    /// <summary>
    /// Perform any actions On death
    /// </summary>
    private void Die() {
        isDead = true;
        events.OnDeath.Invoke(); // Invoke a custom event
    }

    /// <summary>
    /// Basically find everything the script needs to work
    /// </summary>
    private void GetAllReferences() {
        stats = GetComponent<PlayerStats>();
        player = GetComponent<PlayerMovement>();

        PauseMenu.Instance.stats = this;
    }

    /// <summary>
    /// While airborne, if you exceed a certain time, damage on fall will be applied
    /// </summary>
    private void ManageFallDamage() {
        // We obviously wanna start counting when you are airborne
        if (!player.grounded) {
            timer += Time.deltaTime;
            if (timer > minFallTime) applyDamage = true;
        } else // Whenever you fall, apply damage and reset timers for next fall damage
          {
            if (applyDamage) {
                applyDamage = false;
                float damage = timer * fallDamageMultiplier;
                Damage(damage);
            } else
                timer = 0;
        }
    }

    //public bool controllable { get; private set; } = true;

    public bool controllable = true;

    public static bool Controllable { get; private set; }

    public void GrantCrontol() => controllable = true;

    public void LoseCrontol() => controllable = false;

    public void ToggleCrontol() => controllable = !controllable;

    public void CheckIfCanGrantControl() {
        if (PauseMenu.isPaused || isDead) return;
        GrantCrontol();
    }
}