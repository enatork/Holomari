using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System.Collections.Generic;

public class Environment : Singleton<Environment>
{

    [Tooltip("Minimum number of floor planes required in order to exit scanning/processing mode.")]
    public uint minimumFloors = 1;

    [Tooltip("Optional Material to use when rendering Spatial Mapping meshes after the observer has been stopped.")]
    public Material secondaryMaterial;

    private bool meshesProcessed = false;
    public GameObject katamari;
    public GameObject spawner;
    public GameObject startCanvas;
    public GameObject cursor;
    private bool isSpawned = false;
    // Use this for initialization
    void Start()
    {
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnKatamari()
    {
        if (!isSpawned)
        {
            if (SpatialMappingManager.Instance.IsObserverRunning())
            {
                // Stop the observer.
                SpatialMappingManager.Instance.StopObserver();
            }

            meshesProcessed = false;

            // Call CreatePlanes() to generate planes.
            CreatePlanes();
            isSpawned = true;
            startCanvas.SetActive(false);
            Instantiate(spawner, Camera.main.transform.position + new Vector3(0, 0, 2f), Quaternion.identity);
            StartCoroutine(Katamari());
        }
    }

    private IEnumerator Katamari() {
        WaitForSeconds wait = new WaitForSeconds(1f);
        GameObject k = (GameObject)Instantiate(katamari, Camera.main.transform.position + Vector3.forward, Quaternion.identity);
        GestureManager.Instance.Katamari = k;
        KatamariController kc = k.GetComponent<KatamariController>();
        DirectionIndicator di = k.GetComponent<DirectionIndicator>();
        di.Cursor = cursor;
        kc.cursor = cursor;
        yield return wait;
    }

    private void CreatePlanes()
    {
        // Generate planes based on the spatial map.
        SurfaceMeshesToPlanes surfaceToPlanes = SurfaceMeshesToPlanes.Instance;
        if (surfaceToPlanes != null && surfaceToPlanes.enabled)
        {
            surfaceToPlanes.MakePlanes();
        }
    }

    private void RemoveVertices(IEnumerable<GameObject> boundingObjects)
    {
        RemoveSurfaceVertices removeVerts = RemoveSurfaceVertices.Instance;
        if (removeVerts != null && removeVerts.enabled)
        {
            removeVerts.RemoveSurfaceVerticesWithinBounds(boundingObjects);
        }
    }

    public void UpdateMesh()
    {
        SpatialMappingManager.Instance.StartObserver();
    }

    /// <summary>
    /// Handler for the SurfaceMeshesToPlanes MakePlanesComplete event.
    /// </summary>
    /// <param name="source">Source of the event.</param>
    /// <param name="args">Args for the event.</param>
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        // Collection of floor planes that we can use to set horizontal items on.
        List<GameObject> floors = new List<GameObject>();
        floors = SurfaceMeshesToPlanes.Instance.GetActivePlanes(PlaneTypes.Floor);

        // Check to see if we have enough floors (minimumFloors) to start processing.
        if (floors.Count >= minimumFloors)
        {
            // Reduce our triangle count by removing any triangles
            // from SpatialMapping meshes that intersect with active planes.
            RemoveVertices(SurfaceMeshesToPlanes.Instance.ActivePlanes);

            // After scanning is over, switch to the secondary (occlusion) material.
            SpatialMappingManager.Instance.SetSurfaceMaterial(secondaryMaterial);
        }
        else
        {
            // Re-enter scanning mode so the user can find more surfaces before processing.
            SpatialMappingManager.Instance.StartObserver();

            // Re-process spatial data after scanning completes.
            meshesProcessed = false;
        }
    }
}
