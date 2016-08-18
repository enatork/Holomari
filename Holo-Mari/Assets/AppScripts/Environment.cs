using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class Environment : Singleton<Environment>
{

    public GameObject katamari;
    public GameObject spawner;
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
            SpatialMappingManager.Instance.drawVisualMeshes = false;
            isSpawned = true;
            GameObject k = (GameObject)Instantiate(katamari, Camera.main.transform.position + Vector3.forward, Quaternion.identity);
            GameObject s = (GameObject)Instantiate(spawner, Camera.main.transform.position + new Vector3(0, 1, 1), Quaternion.identity);
            
        }
          
    }
}
