using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite[] tileGraphics;

    public float hoverAmount;

    public LayerMask obstacleLayer;

    public Color highlightedColor;
    public bool isWalkable;
    GameMaster gm;
    public Color creatableColor;
    public bool isCreatable;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        int randTile = Random.Range(0, tileGraphics.Length);
        rend.sprite = tileGraphics[randTile];
        gm = FindObjectOfType<GameMaster>();
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.GetComponent<SpriteRenderer>().sortingOrder = 10 - (int)unit.transform.position.y;
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * hoverAmount;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * hoverAmount;
    }

    public bool IsClear()
    {
        Collider2D obstacle = Physics2D.OverlapCircle(transform.position, 0.2f, obstacleLayer);
        if(obstacle != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Highlight()
    {
        rend.color = highlightedColor;
        isWalkable = true;
    }

    public void Reset()
    {
        rend.color = Color.white;
        isWalkable = false;
        isCreatable = false;
    }

    public void SetCreatable()
    {
        rend.color = creatableColor;
        isCreatable = true;
    }

    private void OnMouseDown()
    {
        if (isWalkable && gm.selectedUnit != null)
        {
            gm.selectedUnit.GetComponent<SpriteRenderer>().sortingOrder = 10;
            gm.selectedUnit.Move(this.transform.position);
        }
        else if(isCreatable == true)
        {
            BarrackItem item = Instantiate(gm.purchasedItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            var number = Random.Range(1, 10000);
            item.name = number.ToString();
            gm.resetTiles();
            if (!item.tag.Contains("Fac"))
            {
                Unit unit = item.GetComponent<Unit>();
                unit.hasMoved = true;
                unit.hasAttacked = true;
            }
        }
    }

}
