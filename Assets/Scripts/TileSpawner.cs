using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class StoreData: IEquatable<StoreData>
{
    public int rowIndex=0;
    public int columnIndex=0;
    public int touched =0;

    public StoreData(int rowIndex=0, int columnIndex=0, int touched=0){
        this.rowIndex = rowIndex;
        this.columnIndex = columnIndex;
        this.touched = touched;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        StoreData objAsPart = obj as StoreData;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }

    public bool Equals(StoreData other)
    {
        if (other == null) return false;
        return (this.rowIndex.Equals(other.rowIndex) && this.columnIndex.Equals(other.columnIndex) && this.touched.Equals(other.touched));
    }

    public override int GetHashCode()
    {
        return rowIndex+columnIndex+touched;
    }

}

public class TileSpawner : MonoBehaviour
{
    public GameObject tilePrefab;

    public float Timer = 0f;

    public Transform tileTrans;

    // public Row[] rows = new Row[100];
    public List<StoreData> tilesData = new List<StoreData>();

    public Vector2 tilePos = Vector2.zero;

    public int score;

    static Predicate<StoreData> ByRowCol(int row, int col, int touched)
    {
        return delegate(StoreData t)
        {
            return t.rowIndex == row && t.columnIndex == col && t.touched == touched;
        };
    }

    private int findLengthOfTileInColAndRow(StoreData currentTile){
        var curIdx = tilesData.FindIndex(0, tilesData.Count, x=> x.columnIndex == currentTile.columnIndex && x.rowIndex == currentTile.rowIndex && x.touched==currentTile.touched);
        
        var found =true;
        var length =0;
        var i=0;

        while(found == true){
            var t = tilesData.Find(x=> x.columnIndex == currentTile.columnIndex && x.rowIndex == currentTile.rowIndex+i && x.touched==2);
            if (t!=null){
                found = true;
                t.touched =5;
            }else{
                found = false;
                length =  i;
            }
            i++;
        }

        return length;
    }

    void Start()
    {
        this.tilesData = LoadAll();
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        tileTrans = tilePrefab.GetComponent<Transform>();
        tileTrans.localScale = new Vector2((width - 1.5f) / 4f, height / 4f);

        var array = new int[] { -2, -1, 0, 1 };
        // var row =0;
        

        tilesData.ForEach(currentTile=>{
            var t = tilesData.Find(x=> x.columnIndex == currentTile.columnIndex && x.rowIndex == currentTile.rowIndex && x.touched==1);
            if (t != null){
                
                tilePos = new Vector2(array[currentTile.columnIndex] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, transform.position.y + currentTile.rowIndex * 5f);
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
                
                g.AddComponent<TileData>();
                g.GetComponent<TileData>().rowIndex = currentTile.rowIndex;
                g.GetComponent<TileData>().columnIndex = currentTile.columnIndex;
                g.GetComponent<TileData>().touched = 1;

            }
        });

        var idxOfCurrentTile = 0;
        while(idxOfCurrentTile<tilesData.Count){
            var currentTile = tilesData[idxOfCurrentTile];
            if (currentTile.touched==2){
                var len = this.findLengthOfTileInColAndRow(currentTile);

                tileTrans.localScale = new Vector2((width - 1.5f) / 4f, (height / 4f) * (len+1));
                tilePos = new Vector2(array[currentTile.columnIndex] * tileTrans.localScale.x + tileTrans.localScale.x / 2f, (transform.position.y + ((len * 5f)/2f) + currentTile.rowIndex * 5f));
                var g = Instantiate(tilePrefab, tilePos, Quaternion.identity, gameObject.transform);
                g.AddComponent<TileData>();
                g.GetComponent<TileData>().rowIndex = currentTile.rowIndex;
                g.GetComponent<TileData>().columnIndex = currentTile.columnIndex;
                g.GetComponent<TileData>().touched = 2;
            }
            idxOfCurrentTile++;
        }
        

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
        
    }

    List<StoreData> LoadAll()
    {
        string[] lines = System.IO.File.ReadAllLines(@"E:\Unity Documents\text.txt");
        var tiles = new List<StoreData>();

        foreach (string item in lines)
        {
            string[] parts = item.Split(' ');
            tiles.Add(new StoreData(Int32.Parse(parts[0]), Int32.Parse(parts[1]), Int32.Parse(parts[2])));
            
        }

        return tiles;
    }

  
}
