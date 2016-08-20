using UnityEngine;
using System.Collections;
using System;

public class NucleonSpawner : MonoBehaviour
{

    public float spawnTime;

    public float spawnDistance;

    public Stickable[] nucleonPrefabs;

    float timeSinceLastSpawn;

    public int maxSpawns;
    public int waitToExplodeInSeconds =5;

    int spawnCount;

    bool isDone = false;

    private Stickable[] spawnedNucleons;

    void Awake()
    {
        spawnedNucleons = new Stickable[maxSpawns + 1];
        //Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        if (!isDone)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= spawnTime && spawnCount <= maxSpawns)
            {
                timeSinceLastSpawn -= spawnTime;
                SpawnNucleon();
            }
            if (spawnCount > maxSpawns)
            {
                StartCoroutine("Explode");
                isDone = true;
            }
        }

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(waitToExplodeInSeconds);
        foreach (Stickable nucleon in spawnedNucleons)
        {
            nucleon.attractionForce = nucleon.attractionForce * -1;
        }
        yield return new WaitForSeconds(1f);
        foreach (Stickable nucleon in spawnedNucleons)
        {
            Rigidbody nrb = nucleon.GetComponent<Rigidbody>();
            nrb.useGravity = true;
        }
    }

    void SpawnNucleon()
    {
        Stickable prefab = nucleonPrefabs[UnityEngine.Random.Range(0, nucleonPrefabs.Length)];
        
        
        Vector3 pos = UnityEngine.Random.onUnitSphere * spawnDistance + transform.localPosition;
        
        Stickable spawn = (Stickable)Instantiate(prefab, pos, Quaternion.identity);
        spawn.transform.parent = transform;
        spawnedNucleons[spawnCount] = spawn;
        spawnCount++;
    }
}
