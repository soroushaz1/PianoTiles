using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public GameObject myCamera;

    public AudioSource audioSource;
    void Start()
    {
        audioSource.Play();
       

    }
    void Update()
    {
        LevelSpeed levelSpeed = myCamera.GetComponent<LevelSpeed>();
        float modifiedScale = levelSpeed.modifiedScale;

        //audioSource.pitch = modifiedScale/2f;
    }
}