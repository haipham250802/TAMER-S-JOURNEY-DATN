using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMap : MonoBehaviour
{
    public float LimitLeftX;
    public float LimitRightX;
    public float LimitBottomY;
    public float LimitTopY;

    public float Speed;

    public Vector3 PosMouseDown;

    public Vector2 startPos, EndPos;
    public Rigidbody2D rb;

    bool iscanmove;

    public void onMouseDown()
    {
        startPos = Input.mousePosition;
        iscanmove = true;
    }
    public void OnMouseDrag()
    {
        EndPos = Input.mousePosition;
        if(EndPos.y > startPos.y && iscanmove)
        {
            iscanmove = false;
          //  transform.position += new Vector3(0, 100, 0);
            Debug.Log("aaaaaa");
        }
    }

    public void onMouseUp()
    {
        
    }
}
