﻿using UnityEngine;
using System.Collections;

public class Stickable : MonoBehaviour {

    private bool stuck = false;
    public float attractionForce;
    private Rigidbody rb;
    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.localPosition * -attractionForce);
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

    void OnJointBreak(float breakForce)
    {
        Debug.Log("A joint has just been broken!, force: " + breakForce);
        gameObject.tag = "Stickable";
        stuck = false;
        transform.parent = null;
        foreach (Stickable stickable in gameObject.transform.GetComponentsInChildren<Stickable>())
        {
            stickable.gameObject.tag = "Stickable";
        }

    }
}
