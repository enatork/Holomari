using UnityEngine;
using System.Collections;

public class Nucleon : MonoBehaviour {

    public float attractionForce;

    Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.AddForce(transform.localPosition * -attractionForce);
    }

    public void remove() {
        Destroy(gameObject);
    }
}
