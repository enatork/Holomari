using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
[RequireComponent(typeof(Rigidbody))]
public class KatamariController : MonoBehaviour {

    // Use this for initialization

    private Rigidbody rb;
    private bool isPushing;
    GestureManager gestureManager;
    void Start() {
        rb = GetComponent<Rigidbody>();
        gestureManager = GestureManager.Instance;
    }

    // Update is called once per frame
    void Update() {
        if (isPushing && gestureManager.FocusedObject.tag == "Katamari")
        {
            rb.AddTorque(Camera.main.transform.right * 10000f);
        }
    }

    void OnCollisionEnter(Collision co) {
        if (co.gameObject.tag == "Stickable")
        {
            Stickable gb = co.gameObject.GetComponent<Stickable>();
            gb.Stick(gameObject.transform);
        }
    }

    public void Jump() {
        rb.AddForce(Vector3.up * 20000f);
        rb.AddForce(Camera.main.transform.forward * 10000f);
    }

    public void Push() {
        isPushing = true;
    }

    public void EndPush() {
        isPushing = false;
    }
}
