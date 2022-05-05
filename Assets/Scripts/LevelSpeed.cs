using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpeed : MonoBehaviour
{
    [Range(1,10)]
    public float modifiedScale;
    public GameObject tapDetectObj;

    void Start()
    {
        modifiedScale = 2f;
    }

    void Update()
    {
        //TapDetector tapDetect = tapDetectObj.GetComponent<TapDetector>();
        //int score = tapDetect.score;
        //if (score < 10)
        //    modifiedScale = 1f;
        //else if (score < 20)
        //    modifiedScale = 2f;
        //else if (score < 40)
        //    modifiedScale = 3f;
        //else if (score < 80)
        //    modifiedScale = 4f;
        //else if (score < 120)
        //    modifiedScale = 5f;
        //else
        //{
        //    modifiedScale = 6f;
        //}
        Time.timeScale = modifiedScale;
    }
}
