using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public LayerMask layerMask;

    public float radius = 5.0F;
    public float power = 10.0F;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)){}
                Debug.DrawLine(ray.origin, hit.point, Color.cyan, float.PositiveInfinity);


            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.GetComponentInParent<Player>())
            {
                hit.transform.GetComponentInParent<Player>().Die(power * 2000);
            }

            Vector3 explosionPos = transform.position;


            if (hit.rigidbody)
            {
                hit.rigidbody.AddExplosionForce(power * 2000, explosionPos, radius, 3.0F);
            }
        }
    }
}
