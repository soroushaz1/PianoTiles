using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text text;
    public GameObject tileSpawnerObj;

    void Start()
    {
        text = GetComponent<Text>();

        
        
    }

    void Update()
    {
        TileSpawner tileSpawner = tileSpawnerObj.GetComponent<TileSpawner>();
        int score = tileSpawner.score;
        text.text = score.ToString();
    }
}
