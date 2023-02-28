using UnityEngine;
using UnityEngine.UI;
using NovaFPS;

public class PowerUp : MonoBehaviour {
    [SerializeField] private bool reappears;

    [SerializeField] protected float reappearTime;

    [SerializeField] private Image Timer;

    [HideInInspector] public bool used;

    protected float timer = 0;

    private void Update() {
        if (!reappears && used) {
            Destroy(this.gameObject);
        }
        if (!used && Timer != null)
            Timer.gameObject.SetActive(false);
        else {
            if (Timer != null) {
                Timer.gameObject.SetActive(true);
            }
            timer -= Time.deltaTime;
        }
        if (Timer == null) return;
        if (Timer.gameObject.activeSelf == true) {
            Timer.fillAmount = (reappearTime - timer) / reappearTime;
        }

        if (timer <= 0 && Timer != null) used = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player" && !used) {
            Interact(other.GetComponent<PlayerStats>());
        }
    }

    public virtual void Interact(PlayerStats player) {
        // Override this
    }
}