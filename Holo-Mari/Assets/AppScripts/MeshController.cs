using UnityEngine;
using System.Collections;

public class MeshController : MonoBehaviour {

    public GameObject[] models; 
	// Use this for initialization
	void Start () {
        int rand = Random.Range(0, models.Length);
        GameObject model = models[rand];
        GameObject go = (GameObject)Instantiate(model, gameObject.transform.position, Quaternion.identity, gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetModel() {

    }
}
