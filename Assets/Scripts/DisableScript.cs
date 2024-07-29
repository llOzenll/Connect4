using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisableScript : MonoBehaviour
{
    public DiskMovement diskMovement;
    public DiskMovement1 diskMovement1;
    public DiskDetector diskDetector;

    public GameObject disk;
    public GameObject disk1;

    private GameObject[] redDisk;
    private GameObject[] blueDisk;


    public GameObject blueTurn;
    public GameObject redTurn;

    public GameObject CanvasRestart;

    public bool canCreate;
    bool canCreateAux;


    private void OnTriggerEnter2D(Collider2D other)
    {
        redDisk = GameObject.FindGameObjectsWithTag("Red");
        blueDisk = GameObject.FindGameObjectsWithTag("Blue");

        StartCoroutine(TriggerDelayed(other));
    }

    private IEnumerator TriggerDelayed(Collider2D other){

        yield return new WaitForSeconds(0.4f);

        if (other.CompareTag("Red")){

            // check if RedDisk has connected 4
            var otherPosition = other.transform.position.x;
            int col = Mathf.FloorToInt(Mathf.RoundToInt(otherPosition));
            if (col >= 0 && col < diskDetector.xOfBoard)
            {
                diskDetector.DropPiece(col);
            }

            if (redDisk.Length == 21 && blueDisk.Length == 21)
            {
                CanvasRestart.SetActive(true);
                blueTurn.SetActive(false);
                redTurn.SetActive(false);
            }
            else
            {
                canCreateAux = false;
                diskMovement.enabled = false;

                redTurn.SetActive(false);
                blueTurn.SetActive(true);

                if (canCreate)
                {
                    diskMovement1.Rb.bodyType = RigidbodyType2D.Dynamic;
                    diskMovement1.enabled = true;
                    diskMovement1.Rb.gravityScale = 0;
                    canCreateAux = true;
                }

                //create an Instanse
                if (canCreateAux == false)
                {
                    diskMovement1.Rb.gravityScale = 0;
                    disk1.tag = "Blue";
                    canCreateAux = true;

                    Instantiate(disk1, diskMovement1.IniPosition, Quaternion.identity);
                }
            }
        }
        
        else if(other.CompareTag("Blue")){


            //check if BlueDisk has connected 4
            var otherPosition = other.transform.position.x;
            int col = Mathf.FloorToInt(Mathf.RoundToInt(otherPosition));
            if (col >= 0 && col < diskDetector.xOfBoard)
            {
                diskDetector.DropPiece(col);
            }


            if (redDisk.Length == 21 && blueDisk.Length == 21)
            {
                CanvasRestart.SetActive(true);
                blueTurn.SetActive(false);
                redTurn.SetActive(false);

            }
            else
            {
                canCreateAux = false;
                diskMovement.enabled = true;

                blueTurn.SetActive(false);
                redTurn.SetActive(true);

                if (canCreateAux == false)
                {
                    diskMovement.Rb.gravityScale = 0;
                    disk.tag = "Red";
                    canCreate = false;

                    Instantiate(disk, diskMovement.IniPosition, Quaternion.identity);
                }
            }
        }

    }
}
