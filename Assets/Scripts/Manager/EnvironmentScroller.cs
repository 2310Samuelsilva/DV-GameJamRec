using System.Collections.Generic;
using NUnit;
using UnityEngine;

public class EnvironmentScroller : MonoBehaviour
{
    [Header("Prefabs in Order (Left to Right)")]
    public List<GameObject> prefabList;

    [Tooltip("How far behind/ahead the tiles should recycle")]
    public float recycleOffset = 30f;

    [Tooltip("Adjusts spacing between tiles (positive = gap, negative = overlap)")]
    public float tilePadding = 0f;

    [Tooltip("Assign manually or leave empty to auto-find by Player tag")]
    public Transform player;

    private List<GameObject> environmentTiles;
    private float tileWidth;

    public static EnvironmentScroller Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        // Try to find the player at runtime if not assigned
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogWarning("EnvironmentScroller: No player assigned or found!");
            }
        }

        // Initialize list from prefabs
        environmentTiles = new List<GameObject>(prefabList);

        // Automatically calculate tile width from first tile
        if (prefabList.Count > 0)
        {
            Renderer r = prefabList[0].GetComponentInChildren<Renderer>();
            if (r != null)
            {
                tileWidth = r.bounds.size.x;
            }
            else
            {
                Debug.LogWarning("EnvironmentScroller: Could not find Renderer on first tile.");
            }
        }
        else
        {
            Debug.LogWarning("EnvironmentScroller: No tiles assigned.");
        }
    }

    void Update()
    {
        if (player == null || environmentTiles.Count == 0 || tileWidth <= 0)
        {
            if (player == null) { player = GameObject.FindGameObjectWithTag("Player").transform; }
            //Debug.LogWarning("EnvironmentScroller: Missing player, tiles, or tile width.");
            return;
        }

        GameObject firstTile = environmentTiles[0];
        GameObject lastTile = environmentTiles[environmentTiles.Count - 1];

        float playerX = player.position.x;

        // Move forward
        if (playerX > lastTile.transform.position.x - tileWidth)
        {
            GameObject recycled = firstTile;
            environmentTiles.RemoveAt(0);
            recycled.transform.position = lastTile.transform.position + new Vector3(tileWidth + tilePadding, 0f, 0f);
            environmentTiles.Add(recycled);
        }
        // Move backward
        else if (playerX < firstTile.transform.position.x)
        {
            GameObject recycled = environmentTiles[environmentTiles.Count - 1];
            environmentTiles.RemoveAt(environmentTiles.Count - 1);
            recycled.transform.position = firstTile.transform.position - new Vector3(tileWidth + tilePadding, 0f, 0f);
            environmentTiles.Insert(0, recycled);
        }
    }

}