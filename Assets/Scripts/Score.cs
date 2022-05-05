using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;

    public static int score;

    private void Awake()
    {
        instance = this;
    }
}
