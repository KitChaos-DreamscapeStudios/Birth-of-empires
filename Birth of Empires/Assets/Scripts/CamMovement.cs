using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    float horizontal;
    float vertical;
    public Camera me;
    
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        transform.position += new Vector3(horizontal / 4, vertical / 4);
        me.orthographicSize -= Input.mouseScrollDelta.y;
        if(me.orthographicSize < 0)
        {
           me.orthographicSize = 0.1f;
        }
        //Uncomment these lines if you want to have the camera have a max size
        if (me.orthographicSize > 4)
        {
           me.orthographicSize = 4;
        }
    }
}
