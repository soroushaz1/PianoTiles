using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsPlacement : MonoBehaviour
{


    public GameObject wallPrefab;

    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        Vector2 rightWallPos = new Vector2(width/2f - (wallPrefab.transform.localScale.x / 4f), 0);
        Vector2 leftWallPos = new Vector2(-width/2f + (wallPrefab.transform.localScale.x / 4f), 0);

        GameObject rightWall = Instantiate(wallPrefab, rightWallPos, Quaternion.identity, gameObject.transform);
        GameObject leftWall = Instantiate(wallPrefab, leftWallPos, Quaternion.identity, gameObject.transform);

        rightWall.transform.localScale = new Vector2(rightWall.transform.localScale.x, height);
        leftWall.transform.localScale = new Vector2(rightWall.transform.localScale.x, height);

    }
}
