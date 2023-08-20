using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int levelWidth;
    public int unitSize;
    public int gameWidth;
    public int maxPlatformSize;
    public int minPlatformSize;
    public int maxDistanceBetweenPlatforms;
    public int heightBetweenPlatforms;

    void Start()
    {
        int previousMinX = Random.Range(0, (levelWidth - minPlatformSize) + 1);
        int previousWidth = Random.Range(minPlatformSize, maxPlatformSize);
        int previousMaxX = previousMinX + previousWidth;
        spawnPlatform(previousMinX, -1, previousWidth);

        for (int i = 2; i < 100; i += heightBetweenPlatforms)
        {
            int leftDistance = previousMinX;
            int rightDistance = Mathf.Abs(previousMaxX - levelWidth);
            int x;
            int width;
            if (rightDistance < minPlatformSize || (Random.Range(0, 2) == 0 && leftDistance >= minPlatformSize))
            {
                Debug.Log("left");
                int maxWidth = Mathf.Min(previousMinX, maxPlatformSize);
                width = Random.Range(minPlatformSize, maxWidth + 1);
                int minX = Mathf.Max(0, previousMinX - maxDistanceBetweenPlatforms - width);
                x = Random.Range(minX, (previousMinX - width) + 1);
            }
            else
            {
                Debug.Log("right");
                int maxWidth = Mathf.Min(levelWidth - previousMaxX, maxPlatformSize);
                int maxX = Mathf.Min(levelWidth - minPlatformSize, previousMaxX + maxDistanceBetweenPlatforms);
                width = Random.Range(minPlatformSize, maxWidth + 1);
                x = Random.Range(previousMaxX, maxX + 1);
            }

            previousMinX = x;
            previousWidth = width;
            previousMaxX = x + width;

            spawnPlatform(x, i, width);
        }
    }

    void spawnPlatform(int x, int y, int width)
    {
        GameObject platform = Instantiate(platformPrefab, new Vector2(convertToGameSize(x), y), Quaternion.identity);
        platform.transform.localScale = new Vector2(width, 1);
    }

    int convertToGameSize(int size)
    {
        return (size * unitSize) - (levelWidth / 2);
    }
}
