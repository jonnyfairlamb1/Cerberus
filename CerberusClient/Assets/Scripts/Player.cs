using TMPro;
using UnityEngine;

public class Player : MonoBehaviour {
    public string steamName;
    public string steamId;
    public int playerId;
    public int teamId;

    public GameObject _characterHead;
    public GameObject _characterHeadPrefab;
    public GameObject _characterLeftHand;
    public GameObject _characterLeftLeg;
    public GameObject _characterBody;
    public GameObject _characterRightHand;
    public GameObject _characterRightLeg;

    private bool isDead = false;

    public void UpdateTextFields(TMP_Text[] textfields) {
        for (int i = 0; i < textfields.Length; i++) {
            if (textfields[i].name == "PlayerNameLabelTxt") textfields[i].text = steamName;
        }
    }

    public void Die(float power)
    {
        var ragdollController = GetComponent<RagdollController>();
        if (isDead)
            return;

        isDead = true;
        
        var head = Instantiate(_characterHeadPrefab, transform.position, transform.rotation);
        head.SetActive(true);
        _characterHead.GetComponent<SkinnedMeshRenderer>().enabled = false;


        //set rigidbody as active and give force
        var rb = _characterHeadPrefab.GetComponent<Rigidbody>();
        ragdollController.EnableRagdoll(true);
        rb.AddExplosionForce(power, _characterHeadPrefab.transform.position, 2f);
    }
}