using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    public List<GameObject> prefabList;            
    public Transform cameraTransform;                       
    private float tileWidth = 20f;
    public float recycleDistance = -25f;      

    private Queue<GameObject> environmentQueue;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        // Build queue from list
        environmentQueue = new Queue<GameObject>(prefabList);

        // Automatically calculate tile width from the first prefab
        if (prefabList.Count > 0)
        {
            Renderer renderer = prefabList[0].GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                tileWidth = renderer.bounds.size.x;
            }
            else
            {
                Debug.LogWarning("Could not find Renderer on first prefab to determine tile width.");
            }
        }
    }

    void Update()
    {
        GameObject firstTile = environmentQueue.Peek();
        
        // Check if it’s off screen
        if (cameraTransform.position.x - firstTile.transform.position.x > tileWidth + Mathf.Abs(recycleDistance))
        {
            Debug.Log("Tile is off screen");
            // Remove from front
            GameObject tileToRecycle = environmentQueue.Dequeue();

            GameObject lastTile = GetLastTile();
            Vector3 newPosition = lastTile.transform.position + new Vector3(tileWidth, 0, 0);
            tileToRecycle.transform.position = newPosition;

            // Re-add to end
            environmentQueue.Enqueue(tileToRecycle);
        }
    }

    private GameObject GetLastTile()
    {
        GameObject last = null;
        foreach (var obj in environmentQueue)
        {
            last = obj;
        }
        return last;
    }
}