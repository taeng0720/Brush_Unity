using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public ProgressManager progressManager;

    private readonly Dictionary<float, int> progressToDisableMap = new Dictionary<float, int>
    {
        { 95, 80 },
        { 90, 70 },
        { 80, 55 },
        { 70, 50 },
        { 60, 40 },
        { 50, 35 },
        { 40, 30 },
        { 30, 20 },
        { 20, 10 },
        { 10, 5 }
    };

    private void Start()
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(true);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "village")
        {
            float progress = progressManager.GetProgress();
            DisableTilesBasedOnProgress(progress);
        }

    }

    private void DisableTilesBasedOnProgress(float progress)
    {
        int tilesToDisable = GetTilesToDisable(progress);

        List<GameObject> inactiveTiles = new List<GameObject>();
        foreach (GameObject tile in tiles)
        {
            if (!tile.activeSelf)
            {
                inactiveTiles.Add(tile);
            }
        }

        while (inactiveTiles.Count < tilesToDisable)
        {
            int randomIndex = Random.Range(0, tiles.Length);
            GameObject tile = tiles[randomIndex];
            if (tile.activeSelf)
            {
                tile.SetActive(false);
                inactiveTiles.Add(tile);
            }
        }
    }

    private int GetTilesToDisable(float progress)
    {
        foreach (var entry in progressToDisableMap)
        {
            if (progress >= entry.Key)
            {
                return entry.Value;
            }
        }
        return 0;
    }
}
