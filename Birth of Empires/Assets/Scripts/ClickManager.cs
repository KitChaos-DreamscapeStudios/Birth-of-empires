using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Camera voroCam;
    public VoroGen gen;
    public List<Province> provinces;
    public Texture2D texture;

    public SpriteRenderer sprite;

    public SpriteRenderer sprite2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            var ClickedPoint = Input.mousePosition;
            
            Color32 CheckColor = texture.GetPixel((int)ClickedPoint.x, (int)ClickedPoint.y);
          
            //Debug.Log(CheckColor);
            sprite.color = CheckColor;
            foreach (Province p in provinces)
            {
                //Debug.Log(p.MyClickableZone.MyColor);
                //Debug.Log(p.name + " "+ DifferenceInColors(p.MyClickableZone.MyColor, CheckColor));

                if (DifferenceInColors(p.MyClickableZone.MyColor, CheckColor) == 0)
                {
                    Debug.Log("cats");
                    p.OnLClick();   
                }
            }
        }
        
    }
    Texture2D toTexture2D(RenderTexture rTex)
{
    Texture2D tex = new Texture2D(512, 512, TextureFormat.RGBA32, false);
    // ReadPixels looks at the active RenderTexture.
    RenderTexture.active = rTex;
    tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
    tex.Apply();
    return tex;
}
float DifferenceInColors(Color32 One, Color32 Two){
    var r1 = One.r;
    var g1 = One.g;
    var b1 = One.b;
    var r2 = Two.r;
    var g2 = Two.g;
    var b2 = Two.b;
    return Vector3.Distance(new Vector3(r1, g1, b1), new Vector3(r2, g2, b2));
}
}
