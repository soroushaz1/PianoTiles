using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottomLine : MonoBehaviour
{
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        transform.localScale = new Vector3(width, 1f, 1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            SceneManager.LoadScene("GameOver");
        }
        
    }
}
