using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int levelWidth;
    public int unitSize;
    public int gameWidth;
    // Start is called before the first frame update
    void Start()
    {
        int minX = -7;
        int maxX = 6;
        int startX = Random.Range(minX, maxX + 1);
        int startWidth = Random.Range(2, 4);
        int endX = startX + startWidth;
        spawnPlatform(startX, -2, startWidth);

        for (int i = 0; i < 5; i += 2)
        {
            //distance between the platform and the wall
            int leftDistance = Mathf.Abs(minX - startX);
            int rightDistance = Mathf.Abs(endX - maxX);
            int x;
            int width;

            Debug.Log(i + " leftDistance = " + leftDistance);
            Debug.Log(i + " rightDistance = " + rightDistance);

            if (rightDistance < 2 || (Random.Range(0, 2) == 0 && leftDistance > 2))
            {
                width = Random.Range(2, leftDistance);
                x = Random.Range(minX, Mathf.Abs(startX - leftDistance) + 1);

            }
            else
            {
                width = Random.Range(2, rightDistance);
                x = Random.Range(endX, Mathf.Abs(endX - rightDistance) + 1);

            }

            spawnPlatform(x, i, width);

            startX = x;
            startWidth = width;
            endX = startX + startWidth;
        }
    }

    void spawnPlatform(int x, int y, int width)
    {
        GameObject platform = Instantiate(platformPrefab, new Vector2(x, y), Quaternion.identity);
        platform.transform.localScale = new Vector2(width, 1);
    }

    // void Start()
    // {
    //     int previousMinX = Random.Range(0, levelWidth - 1);
    //     int previousWidth = Random.Range(2, levelWidth / 2);
    //     int previousMaxX = previousMinX + previousWidth;

    //     for (int i = 0; i < 5; i += 2)
    //     {
    //         int leftDistance = previousMinX;
    //         int rightDistance = Mathf.Abs(previousMaxX - levelWidth);
    //         int x;
    //         int width;
    //         if (rightDistance < 2 || (Random.Range(0, 2) == 0 && leftDistance > 1))
    //         {
    //             width = Random.Range(2, previousMinX + 1);
    //             x = Random.Range(0, (previousMinX - width) + 1);
    //         }
    //         else
    //         {
    //             width = Random.Range(2, (levelWidth - previousMaxX) + 1);
    //             x = Random.Range(previousMinX, levelWidth - 1);
    //         }

    //         spawnPlatform(x, i, width);
    //     }
    // }

    // void spawnPlatform(int x, int y, int width)
    // {
    //     GameObject platform = Instantiate(platformPrefab, new Vector2(convertToGameSize(x), convertToGameSize(y)), Quaternion.identity);
    //     platform.transform.localScale = new Vector2(convertToGameSize(width), 1);
    // }

    // int convertToGameSize(int size)
    // {
    //     return (size * unitSize) - (gameWidth / 2);
    // }
}
