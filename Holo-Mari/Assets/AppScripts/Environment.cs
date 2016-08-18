using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class Environment : Singleton<Environment>
{

    public GameObject katamari;
    private bool isSpawned = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnKatamari()
    {
        if (!isSpawned) {
            isSpawned = true;
            GameObject k = (GameObject)Instantiate(katamari, Camera.main.transform.position + Vector3.forward, Quaternion.identity);
            SpatialMappingManager.Instance.drawVisualMeshes = false;
        }
          
    }
}
