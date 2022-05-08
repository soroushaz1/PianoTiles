using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TileData : MonoBehaviour
{
    public int rowIndex=0;
    public int columnIndex=0;
    public int touched =0;

    public TileData(int rowIndex=0, int columnIndex=0, int touched=0){
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
        this.touched = touched;
    }
}

public class Subtitle : MonoBehaviour
{
    public GameObject tilePrefab;

    public float Timer = 1f;

    public Transform tileTrans;

    public List<TileData> tilesData;

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

            // rows[row] = new Row();
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
                        var h = hit.collider.GetComponent<TileData>();
                        this.tilesData.Add(new TileData(h.rowIndex,h.columnIndex, 1));
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
                        var h = hit.collider.GetComponent<TileData>();
                        this.tilesData.Add(new TileData(h.rowIndex,h.columnIndex, 2));
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
        for (int i =0; i< this.tilesData.Count; i++)
        {
            var e = this.tilesData[i];
            sw.WriteLine(e.rowIndex + " " + e.columnIndex+ " " + e.touched);
        }
        sw.Close();
    }

    void OnApplicationQuit()
    {
        saveAll();
    }

}
