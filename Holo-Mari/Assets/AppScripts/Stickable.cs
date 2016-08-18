using UnityEngine;
using System.Collections;

public class Stickable : MonoBehaviour {

    private bool stuck = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Stick(Transform parent) {
        transform.parent = parent;
        transform.tag = "Katamari";
        Transform child = transform.GetChild(0);
        Rigidbody cRBody = child.GetComponent<Rigidbody>();
        stuck = true;
        FixedJoint joint = gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = parent.GetComponent<Rigidbody>();
        joint.breakForce = 1050f;

    }
}
