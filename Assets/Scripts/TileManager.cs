using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public ProgressManager progressManager;

    private void Start()
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(true);
        }
    }

    private void Update()
    {
        float progress = progressManager.GetProgress();
        DisableTilesBasedOnProgress(progress);
    }

    private void DisableTilesBasedOnProgress(float progress)
    {
        int tilesToDisable = 0;

        if (progress >= 95) tilesToDisable = 80;
        else if (progress >= 90) tilesToDisable = 70;
        else if (progress >= 80) tilesToDisable = 55;
        else if (progress >= 70) tilesToDisable = 50;
        else if (progress >= 60) tilesToDisable = 40;
        else if (progress >= 50) tilesToDisable = 35;
        else if (progress >= 40) tilesToDisable = 30;
        else if (progress >= 30) tilesToDisable = 20;
        else if (progress >= 20) tilesToDisable = 10;
        else if (progress >= 10) tilesToDisable = 5;

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
}
