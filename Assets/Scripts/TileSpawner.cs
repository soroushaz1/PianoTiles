using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;

    public float Timer = 0f;

    public Transform tileTrans;

    public Row[] rows = new Row[100];

    public Vector2 tilePos = Vector2.zero;

    public int score;

    public GameObject slider;

    void Start()
    {
        score = Score.score;

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        tileTrans = tilePrefab.GetComponent<Transform>();
        tileTrans.localScale = new Vector2((width - 1.5f) / 4f, height/4f);

        for (int i = 0; i < 100; i++)
        {
            rows[i] = new Row();
        }

        LoadAll();
    }

    private void Update()
    {
        Timer -= Time.fixedDeltaTime;
        int i = 0;
        string timeString = Timer.ToString();

        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.up, .1f);

                if (hit.collider != null)
                {

                    if (hit.collider.tag == "Tile")
                    {

                        Destroy(hit.collider.gameObject);
                        score++;
                        Timer = 0.1f;

                    }
                }
            }
            if (Input.GetTouch(i).phase == TouchPhase.Stationary && Timer < .01f)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.up, .1f);

                if (hit.collider != null)
                {

                    if (hit.collider.tag == "Tile")
                    {

                        Destroy(hit.collider.gameObject);
                        score++;
                    }
                }
            }

            ++i;

        }
    }
    void FixedUpdate()
    {
        //Timer -= Time.fixedDeltaTime;
        //if (Timer <= .01f)
        //{
        //    float height = Camera.main.orthographicSize * 2;
        //    float width = height * Screen.width / Screen.height;


        //    int xPos = Random.Range(-2, 2);
        //    Vector2 tilePos = new Vector2(xPos*tileTrans.localScale.x + tileTrans.localScale.x/2f, transform.position.y);

        //    Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);

        //    Timer = 1f;
        //}
        
    }

    void LoadAll()
    {
        int i = 0;
        string[] lines = System.IO.File.ReadAllLines(@"E:\Unity Documents\text.txt");
        

        foreach (string item in lines)
        {
            
            string[] parts = item.Split(' ');
            
            if (parts[0] == "True")
            {
                rows[i].A = true;
            }
            if (parts[1] == "True")
            {
                rows[i].B = true;
            }
            if (parts[2] == "True")
            {
                rows[i].C = true;
            }
            if (parts[3] == "True")
            {
                rows[i].D = true;
            }
            if (parts[4] == "0")
            {
                rows[i].scale = 0;
            }
            if (parts[4] == "1")
            {
                rows[i].scale = 1;
            }
            if (parts[4] == "2")
            {
                rows[i].scale = 2;
            }
            if (parts[4] == "3")
            {
                rows[i].scale = 3;
            }

            i++;

        }

        SpawnTile();
        //for (int j = 0; j < 100; j++)
        //{
        //    Debug.Log(rows[j].A.ToString() + rows[j].B.ToString() + rows[j].C.ToString() + rows[j].D.ToString() + rows[j].scale.ToString());
        //}



    }

    void SpawnTile()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        tileTrans = tilePrefab.GetComponent<Transform>();
        tileTrans.localScale = new Vector2((width - 1.5f) / 4f, height / 4f);

        var array = new int[] { -2, -1, 0, 1 };
        for (int row = 0; row < 100; row++)
        {
            

            if (rows[row].A && rows[row].scale == 1)
            {
                tilePos = new Vector2(array[0] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + row * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
            }
                
            if (rows[row].B && rows[row].scale == 1)
            {
                tilePos = new Vector2(array[1] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + row * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
            }
                
            if (rows[row].C && rows[row].scale == 1)
            {
                tilePos = new Vector2(array[2] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + row * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
            }
                
            if (rows[row].D && rows[row].scale == 1)
            {
                tilePos = new Vector2(array[3] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + row * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
            }
                

            
            

            
        }
    }
}
