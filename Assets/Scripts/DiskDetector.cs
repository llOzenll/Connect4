using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DiskDetector : MonoBehaviour
{
    public int xOfBoard = 7;
    public int yOfBoard = 6;
    public int[,] board;
    public int currentDisk;

    public GameObject winsRed;
    public GameObject winsBlue;
    public GameObject canvasMenu;

    private void Start()
    {
        IniBoard();
    }

    public void IniBoard()
    {
        currentDisk = 1;

        board = new int[yOfBoard, xOfBoard];

        for (int y = 0; y < yOfBoard; y++)
        {
            for (int x = 0; x < xOfBoard; x++)
            {
                board[y, x] = 0;
            }
        }
    }

    public void DropPiece(int col)
    {
        for (int row = 0; row < yOfBoard; row++)
        {
            if (board[row, col] == 0)
            {
                board[row, col] = currentDisk;

                if (DidWin(currentDisk))
                {

                    if (currentDisk == 1)
                    {
                        winsRed.SetActive(true);
                        canvasMenu.SetActive(false);
                        IniBoard();
                        Time.timeScale = 0;
                    }

                    else if (currentDisk == 2)
                    {
                        winsBlue.SetActive(true);
                        canvasMenu.SetActive(false);
                        IniBoard();
                        Time.timeScale = 0;
                    }
                }
                currentDisk = (currentDisk == 1) ? 2 : 1; // Cambia de jugador
                return;
            }
        }
    }

    public bool DidWin(int diskNum)
    {
        // Horizontal
        for (int y = 0; y < yOfBoard; y++)
        {
            for (int x = 0; x < xOfBoard - 3; x++)
            {
                if (board[y, x] == diskNum && board[y, x + 1] == diskNum && board[y, x + 2] == diskNum && board[y, x + 3] == diskNum)
                {
                    return true;
                }
            }
        }

        // Vertical
        for (int x = 0; x < xOfBoard; x++)
        {
            for (int y = 0; y < yOfBoard - 3; y++)
            {
                if (board[y, x] == diskNum && board[y + 1, x] == diskNum && board[y + 2, x] == diskNum && board[y + 3, x] == diskNum)
                {
                    return true;
                }
            }
        }

        // Diagonal /
        for (int y = 0; y < yOfBoard - 3; y++)
        {
            for (int x = 0; x < xOfBoard - 3; x++)
            {
                if (board[y, x] == diskNum && board[y + 1, x + 1] == diskNum && board[y + 2, x + 2] == diskNum && board[y + 3, x + 3] == diskNum)
                {
                    return true;
                }
            }
        }

        // Diagonal \
        for (int y = 3; y < yOfBoard; y++)
        {
            for (int x = 0; x < xOfBoard - 3; x++)
            {
                if (board[y, x] == diskNum && board[y - 1, x + 1] == diskNum && board[y - 2, x + 2] == diskNum && board[y - 3, x + 3] == diskNum)
                {
                    return true;
                }
            }
        }

        return false;
    }
}