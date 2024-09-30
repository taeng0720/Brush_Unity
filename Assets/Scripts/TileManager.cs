using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileManager : MonoBehaviour
{
    public string tileTag = "Plane";
    public ProgressManager progressManager;

    private GameObject[] tiles;
    private readonly Dictionary<float, int> progressToDisableMap = new Dictionary<float, int>
    {
        { 95, 80 }, { 90, 70 }, { 80, 55 }, { 70, 50 },
        { 60, 40 }, { 50, 35 }, { 40, 30 }, { 30, 20 },
        { 20, 10 }, { 10, 5 }
    };

    private void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag(tileTag);
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
        int disabledCount = 0;

        while (disabledCount < tilesToDisable)
        {
            int randomIndex = Random.Range(0, tiles.Length);
            GameObject tile = tiles[randomIndex];
            if (tile.activeSelf)
            {
                tile.SetActive(false);
                disabledCount++;
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
