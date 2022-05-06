using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileData : MonoBehaviour
{
    public int rowIndex;
    public int columnIndex;
}

public class Row
{
    public bool A, B, C, D;
    public int scale = 0;
}

public class Subtitle : MonoBehaviour
{
    public GameObject tilePrefab;

    public float Timer = 1f;

    public Transform tileTrans;

    public Row[] rows = new Row[100];

    public Vector3 pivot;

    void Start()
    {
        pivot = tilePrefab.transform.GetChild(1).transform.position;

        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        tileTrans = tilePrefab.GetComponent<Transform>();
        tileTrans.localScale = new Vector2((width - 1.5f) / 4f, height / 4f);

        var array = new int[] { -2, -1, 0, 1 };
        for (int row = 0; row < 100; row++)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 tilePos = new Vector2(array[i] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + row * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
                
                g.AddComponent<TileData>();
                g.GetComponent<TileData>().rowIndex = row;
                g.GetComponent<TileData>().columnIndex = i;
            }

            rows[row] = new Row();
        }
        
    }

    void Update()
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
                        var tile = hit.collider.GetComponent<TileData>();
                        //Debug.Log(tile.columnIndex);
                        if (tile.columnIndex == 0)
                        {
                            rows[tile.rowIndex].A = true;
                            rows[tile.rowIndex].scale = 1;
                            
                        }
                            
                        if (tile.columnIndex == 1)
                        {
                            rows[tile.rowIndex].B = true;
                            rows[tile.rowIndex].scale = 1;
                        }
                            
                        if (tile.columnIndex == 2)
                        {
                            rows[tile.rowIndex].C = true;
                            rows[tile.rowIndex].scale = 1;
                        }
                            
                        if (tile.columnIndex == 3)
                        {
                            rows[tile.rowIndex].D = true;
                            rows[tile.rowIndex].scale = 1;
                        }
                            


                        Destroy(hit.collider.gameObject);
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
                        var tile = hit.collider.GetComponent<TileData>();
                        //Debug.Log(tile.columnIndex);
                        if (tile.columnIndex == 0)
                        {
                            rows[tile.rowIndex].A = true;
                            rows[tile.rowIndex].scale = 3;
                            if (rows[tile.rowIndex - 1].scale == 1)
                                rows[tile.rowIndex - 1].scale = 2;
                        }
                            
                        if (tile.columnIndex == 1)
                        {
                            rows[tile.rowIndex].B = true;
                            rows[tile.rowIndex].scale = 3;
                            if (rows[tile.rowIndex - 1].scale == 1)
                                rows[tile.rowIndex - 1].scale = 2;
                        }
                            
                        if (tile.columnIndex == 2)
                        {
                            rows[tile.rowIndex].C = true;
                            rows[tile.rowIndex].scale = 3;
                            if (rows[tile.rowIndex - 1].scale == 1)
                                rows[tile.rowIndex - 1].scale = 2;
                        }
                            
                        if (tile.columnIndex == 3)
                        {
                            rows[tile.rowIndex].D = true;
                            rows[tile.rowIndex].scale = 3;
                            if (rows[tile.rowIndex - 1].scale == 1)
                                rows[tile.rowIndex - 1].scale = 2;
                        }



                        Destroy(hit.collider.gameObject);

                    }
                }
            }

            ++i;

        }
    }

    void FixedUpdate()
    {
        
    }

    void saveAll()
    {
        StreamWriter sw = new StreamWriter(@"E:\Unity Documents\text.txt");
        foreach (Row e in rows)
        {
            sw.WriteLine(e.A + " " + e.B + " " + e.C + " " + e.D + " " + e.scale);
        }
        sw.Close();
    }

    void OnApplicationQuit()
    {
        saveAll();
    }

    //public void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
    //{
    //    Vector3 A = target.transform.localPosition;
    //    Vector3 B = pivot;

    //    Vector3 C = A - B; // diff from object pivot to desired pivot/origin

    //    float RS = newScale.x / target.transform.localScale.x; // relataive scale factor

    //    // calc final position post-scale
    //    Vector3 FP = B + C * RS;

    //    // finally, actually perform the scale/translation
    //    target.transform.localScale = newScale;
    //    target.transform.localPosition = FP;
    //}
}
