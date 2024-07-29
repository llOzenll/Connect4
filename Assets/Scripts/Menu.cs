using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public DiskMovement diskMovement;
    public DiskMovement1 diskMovement1;
    public DisableScript disableScript;
    public DiskDetector diskDetector;

    public GameObject canvasStart;
    public GameObject canvasMenu;
    public GameObject canvasWins;
    public GameObject canvasLose;
    public GameObject canvasRestart;

    public GameObject redTurn;
    public GameObject blueTurn;

    private List<GameObject> listRed = new List<GameObject>();
    private List<GameObject> listBlue = new List<GameObject>();

    void Start()
    {
        Time.timeScale = 0;

        diskMovement.enabled = false;
        diskMovement1.enabled = false;
    }

    void Update()
    {
        UpdateDiskListWithTag("Red", listRed);
        UpdateDiskListWithTag("Blue", listBlue);
    }

    private void UpdateDiskListWithTag(string tag, List<GameObject> list)
    {
        GameObject[] disks = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject disk in disks)
        {
            if (!list.Contains(disk))
            {
                list.Add(disk);
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(Start1());
    }

    private IEnumerator Start1()
    {
        Time.timeScale = 1;
        diskDetector.IniBoard();
        canvasStart.SetActive(false);
        canvasMenu.SetActive(true);

        redTurn.SetActive(true);
        blueTurn.SetActive(false);

        diskMovement1.Rb.position = diskMovement1.IniPosition;
        diskMovement1.Rb.gravityScale = 0;
        diskMovement.enabled = true;

        diskMovement.Rb.gravityScale = 0;
        diskMovement.Rb.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(0.1f);
        
        diskMovement1.Rb.bodyType = RigidbodyType2D.Static;

        diskMovement1.enabled = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        StartCoroutine(BackMethod());
    }
    private IEnumerator BackMethod()
    {

        Time.timeScale = 1;
        Deleted(listRed);
        Deleted(listBlue);


        disableScript.canCreate = true;

        canvasStart.SetActive(true);
        canvasMenu.SetActive(false);

        canvasWins.SetActive(false);
        canvasLose.SetActive(false);

        canvasRestart.SetActive(false);


        diskMovement1.enabled = true;
        diskMovement1.Rb.position = diskMovement1.IniPosition;
        diskMovement1.Rb.gravityScale = 0;

        diskMovement.Rb.position = diskMovement.IniPosition;
        diskMovement.Rb.gravityScale = 0;

        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
    }

    private void Deleted(List<GameObject> list)
    {
        while (list.Count > 1)
        {
            GameObject toDelete = list[list.Count - 1]; // Toma el último elemento
            list.RemoveAt(list.Count - 1); // Elimina de la lista
            Destroy(toDelete); // Destruye el GameObject
        }
    }
}