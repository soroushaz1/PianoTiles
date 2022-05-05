using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TapDetector : MonoBehaviour
{
    public int score;

    void Start()
    {
        score = Score.score;
    }

    void Update()
    {
        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                if (hit.collider != null)
                {

                    if (hit.collider.tag == "Tile")
                    {
                        float hitX = hit.collider.gameObject.transform.position.x;
                        if (hitX < -3f && hitX > -4f)
                            Debug.Log("1 0 0 0");
                        else if (hitX < -1f && hitX > -2f)
                            Debug.Log("0 1 0 0");
                        else if (hitX < 2f && hitX > 1)
                            Debug.Log("0 0 1 0");
                        else if (hitX < 4f && hitX > 3)
                            Debug.Log("0 0 0 1");

                        Destroy(hit.collider.gameObject);

                        //score ++;
                        //Debug.Log(score);
                    }
                    else
                    {
                        //SceneManager.LoadScene("GameOver");
                    }
                }
            }
            ++i;
        }
    }
}
