using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class VoroGen : MonoBehaviour
{
    Color32[] allColors;
    public Color32[,] colors;
    public Vector2Int Size;
    public RawImage me;

    public int GridSize = 10;

    public Vector2[,] points;
    Vector2Int CellDimensions;

    public ClickableZone[,] provinces;

    public ClickManager clickManager;
    // Start is called before the first frame update
    private void Awake() {
        me = GetComponent<RawImage>();
        Size = new Vector2Int(Mathf.RoundToInt(GetComponent<RectTransform>().sizeDelta.x), Mathf.RoundToInt(GetComponent<RectTransform>().sizeDelta.y));
        CellDimensions.x = Size.x/GridSize;
        CellDimensions.y = Size.y/GridSize;
        GenPoints();
        GenDiagram();
    }
    public void GenDiagram(){
       
        //We can get the color of the pixel to affirm if it
        Texture2D tex = new Texture2D(Size.x, Size.y);
         
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            
            {
                float nearestDist = Mathf.Infinity;
                Vector2 nearestPoint = new Vector2Int();
                int GridX = i/CellDimensions.x;
                int GridY = j/CellDimensions.y;
                for (int a = -1; a < 2; a++)
                {
                    for (int b = -1; b < 2; b++)
                    {
                       
                        int X = GridX+a;
                        int Y = GridY+b;
                        if (X<0||Y<0||X>=GridSize||Y>=GridSize)
                        {
                            continue;
                        }
                      
                        var Dist = Vector2.Distance(new Vector2Int(i,j), points[X,Y]);
                        if(Dist < nearestDist){
                            nearestDist = Dist;
                            nearestPoint = new Vector2Int(X,Y);
                        }
                    }
                }
                tex.SetPixel(i,j, colors[(int)nearestPoint.x, (int)nearestPoint.y]);
                //provinces[i,j].MyColor = colors[(int)nearestPoint.x, (int)nearestPoint.y];
            }
        }
        tex.Apply();
        me.texture = tex;
        File.WriteAllBytes("./Assets/ClickableMap.png", tex.EncodeToPNG());
        int I = 0;
        foreach (ClickableZone C in provinces)
        {
            I++;
            var NewProvince = new GameObject($"Province{I}");
            var ProvData = NewProvince.AddComponent<Province>();
            ProvData.MyClickableZone = C;
            clickManager.provinces.Add(ProvData);
        }
    }
    public Vector3 GetPointsArea(){
        return new Vector3();
    }
    void GenPoints(){
        colors = new Color32[GridSize,GridSize];
        points = new Vector2[GridSize,GridSize];
        provinces = new ClickableZone[GridSize,GridSize];
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                points[i,j] = new Vector2(i*CellDimensions.x+Random.Range(0, CellDimensions.x),j*CellDimensions.y+Random.Range(0, CellDimensions.y));
                provinces[i,j] = new ClickableZone(); 
                provinces[i,j].MyPoint = points[i,j];
                //colors[i,j] = allColors[Random.Range(0, allColors.Length)];
                var Clr = new Color32((byte)Random.Range(0, 255f), (byte)Random.Range(0, 255f), (byte)Random.Range(0, 255f), 255);
                colors[i,j] = Clr;
                provinces[i,j].MyColor = colors[i,j];
                //Debug.Log(points[i,j]);
            }
        }
    }
}
