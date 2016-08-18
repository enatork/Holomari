using UnityEngine;
using System.Collections;

public class KatamariController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
}
