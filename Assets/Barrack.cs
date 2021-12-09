using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrack : MonoBehaviour
{
    public Button player1Button;
    public Button player2Button;

    public GameObject player1Menu;
    public GameObject player2Menu;

    GameMaster gm;

    private void Start()
    {
        gm = GetComponent<GameMaster>();
    }

    private void Update()
    {
        if(gm.playerTurn == 1)
        {
         player1Button.interactable = true;
         player2Button.interactable = false;
        }
        else
        {
            player1Button.interactable = false;
            player2Button.interactable = true;
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        if(menu.activeInHierarchy == false)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }

    public void closeMenus()
    {
        player1Menu.SetActive(false);
        player2Menu.SetActive(false);
    }

    public void buyItem(BarrackItem item)
    {
        if(gm.playerTurn == 1 && item.cost <= gm.player1Energy)
        {
            gm.player1Energy -= item.cost;
            player1Menu.SetActive(false);
        }
        else if (gm.playerTurn == 2 && item.cost <= gm.player2Energy)
        {
            gm.player2Energy -= item.cost;
            player2Menu.SetActive(false);
        }
    }
}
