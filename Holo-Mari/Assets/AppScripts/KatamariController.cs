using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class KatamariController : MonoBehaviour {

    // Use this for initialization

    private Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision co) {
        if (co.gameObject.tag == "Stickable")
        {
            Stickable gb = co.gameObject.GetComponent<Stickable>();
            gb.Stick(gameObject.transform);
        }
    }

    public void OnSelect() {
        rb.AddTorque(Camera.main.transform.right * 10000f);
        rb.AddForce(Camera.main.transform.forward * 5000f);
    }
}
