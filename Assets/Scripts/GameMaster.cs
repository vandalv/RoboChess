using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public int playerTurn = 1;

    public void resetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }
    }
    void EndTurn()
    {
        if(playerTurn == 1)
        {
            playerTurn = 2;
        }
        else if(playerTurn == 2)
        {
            playerTurn = 1;
        }
        if(selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }
        resetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.rend.color = Color.white;
            unit.hasAttacked = false;
        }
    }
}
