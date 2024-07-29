using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiskMovement1 : MonoBehaviour
{
    #region  Boundary
    public Transform BoundaryHolder;
    Boundary diskBoundary;
    public struct Boundary{
        public float UpLimit, DownLimit, LeftLimit, RightLimit;

        public Boundary(float up, float down, float left, float right)
        {
            UpLimit = up; DownLimit = down; LeftLimit = left; RightLimit = right;
        }
    }
    #endregion

    
    Vector2 touchPosition;
    public Rigidbody2D Rb;

    public Vector2 IniPosition;

    public DiskDetector diskDetector;

    public Collider2D topWall;

    

    void Start(){
        diskBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,BoundaryHolder.GetChild(1).position.y,
                                    BoundaryHolder.GetChild(2).position.x, BoundaryHolder.GetChild(3).position.x)
        ;

        Rb = GetComponent<Rigidbody2D>();

        IniPosition = new Vector2(6,8);
    }

    void OnMouseDrag(){
        if (diskDetector.currentDisk == 2)
        {

            if (Time.timeScale == 1)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                MoveToPosition(touchPosition);
                Rb.gravityScale = 0;
                topWall.isTrigger = false;
            }
        }
    }
    void OnMouseUp(){
        Rb.gravityScale = 5;
        topWall.isTrigger = true;
    }

    public void MoveToPosition(Vector2 position){
        Vector2 clampedDisk = new(Mathf.Clamp(position.x, diskBoundary.LeftLimit, diskBoundary.RightLimit),
                                 Mathf.Clamp(position.y, diskBoundary.DownLimit, diskBoundary.UpLimit));

        Rb.MovePosition(clampedDisk);
    }
}