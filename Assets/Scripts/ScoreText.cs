using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text text;
    public GameObject tapDetectObj;

    void Start()
    {
        text = GetComponent<Text>();

        
        
    }

    void Update()
    {
        TapDetector tapDetect = tapDetectObj.GetComponent<TapDetector>();
        int score = tapDetect.score;
        text.text = score.ToString();
    }
}
