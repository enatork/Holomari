using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
[RequireComponent(typeof(Rigidbody))]
public class KatamariController : MonoBehaviour {

    // Use this for initialization

    private Rigidbody rb;
    private bool isPushing;
    float timer = 0;
    GestureManager gestureManager;
    public GameObject cursor;
    void Start() {
        rb = GetComponent<Rigidbody>();
        gestureManager = GestureManager.Instance;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (timer < .5f && isPushing) {
            rb.AddRelativeTorque(Vector3.right * 10000f);
            //rb.AddTorque((cursor.transform.position - transform.position) * 10000f);
            
            timer += Time.deltaTime;
        }
        if (timer > .5f) {
            timer = 0;
            isPushing = false;
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
        //isPushing = true;
       
        rb.AddForce(Vector3.up * 20000f);
        rb.AddForce(Camera.main.transform.forward * 10000f);
    }

    public void Push() {
        transform.LookAt(cursor.transform);
        isPushing = true;
        timer = 0;
       
    }

    public void EndPush() {
        isPushing = false;
    }
}
