using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;

    public int playerTurn = 1;
    public Image playerIndicator;
    public Sprite player1Indicator;
    public Sprite player2Indicator;
    public int player1Energy = 110;
    public int player2Energy = 110;
    public Text player1EnergyText;
    public Text player2EnergyText;

    public BarrackItem purchasedItem;
    public GameObject statsPanel;
    public Unit viewedUnit;
    public Unit yviewedUnit;
    public Unit bviewedUnit;

    public Text yHealthText;
    public Text yArmorText;
    public Text yAttackDamageText;
    public Text yDefenceDamageText;

    public Text bHealthText;
    public Text bArmorText;
    public Text bAttackDamageText;
    public Text bDefenceDamageText;


    public void ToggleStatsPanel(Unit unit)
    {
        if(unit.Equals(viewedUnit) == false)
        {
            viewedUnit = unit;
            if (viewedUnit.tag.Contains("Y"))
            {
                yviewedUnit = viewedUnit;
                UpdateStatsPanel();
            }
            if (viewedUnit.tag.Contains("B"))
            {
                bviewedUnit = viewedUnit;
                UpdateStatsPanel();
            }
        }
    }

    public void UpdateStatsPanel()
    {
        if(yviewedUnit != null)
        {
            yHealthText.text = yviewedUnit.health.ToString();
            yArmorText.text = yviewedUnit.armor.ToString();
            yAttackDamageText.text = yviewedUnit.attackDamage.ToString();
            yDefenceDamageText.text = yviewedUnit.defenseDamage.ToString();
            if (yviewedUnit.isBoss)
            {
                yHealthText.text = "-";
            }
        }
        if (bviewedUnit != null)
        {
            bHealthText.text = bviewedUnit.health.ToString();
            bArmorText.text = bviewedUnit.armor.ToString();
            bAttackDamageText.text = bviewedUnit.attackDamage.ToString();
            bDefenceDamageText.text = bviewedUnit.defenseDamage.ToString();
            if (bviewedUnit.isBoss)
            {
                bHealthText.text = "-";
            }
        }
    }

    void getEnergyIncome(int playerTurn)
    {
        foreach (Factory f in FindObjectsOfType<Factory>())
        {
                if(f.playerNumber == playerTurn)
                {
                    if(playerTurn == 1)
                    {
                        player1Energy += f.energyPerTurn;
                    }
                    else
                    {
                        player2Energy += f.energyPerTurn;
                    }
                }

            }
            UpdateEnergyText();
    }

    public void UpdateEnergyText()
    {
        player1EnergyText.text = player1Energy.ToString();
        player2EnergyText.text = player2Energy.ToString();
    }



    public void resetTiles()
    {
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    private void Start()
    {
        getEnergyIncome(1);
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
            playerIndicator.sprite = player2Indicator;
        }
        else if(playerTurn == 2)
        {
            playerTurn = 1;
            playerIndicator.sprite = player1Indicator;
        }
        getEnergyIncome(playerTurn);
        if (selectedUnit != null)
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
        GetComponent<Barrack>().closeMenus();
    }
}
