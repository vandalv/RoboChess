using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public SpriteRenderer rend;
    public Color attackColor;
    public bool selected;
    GameMaster gm;
    public int tileSpeed;
    public bool hasMoved;
    public int playerNumber;

    public float moveSpeed;
    public int attackRange;
    List<Unit> enemiesInRange = new List<Unit>();
    public bool hasAttacked;
    public int health;
    public int attackDamage;
    public int defenseDamage;
    public int armor;
    Unit tempUnit;
    public Text bossHealth;
    public bool isBoss;

    public DamageIcon damageIcon;


    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        gm = FindObjectOfType<GameMaster>();
    }

    private void OnMouseDown()
    {
        if (selected == true)
        {
            selected = false;
            gm.selectedUnit = null;
            gm.resetTiles();
        }
        else
        {
            if(playerNumber == gm.playerTurn)
            {
                if (gm.selectedUnit != null)
                {
                    gm.selectedUnit.selected = false;
                }
                selected = true;
                gm.selectedUnit = this;
                gm.resetTiles();
                getEnemies();
                GetWalkableTiles();
            }
        }

        Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.15f);
        Unit unit = col.GetComponent<Unit>();
        if(gm.selectedUnit != null)
        {
            if (gm.selectedUnit.enemiesInRange.Contains(unit) && gm.selectedUnit.hasAttacked == false)
            {
                gm.selectedUnit.attack(unit);
                tempUnit = gm.selectedUnit;
                tempUnit.rend.color = Color.grey;
            }
        }
    }

    void attack(Unit enemy)
    {
        hasAttacked = true;
        this.hasMoved = true;
        gm.resetTiles();
        this.rend.color = Color.grey;
        enemy.rend.color = Color.white;
        int enemyDamage = attackDamage - enemy.armor;
        int myDamage = enemy.defenseDamage - armor;


        if (!transform.tag.Contains("Sni"))
        {
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("Y"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("B"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (enemyDamage >= 1)
            {
                DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
                instance.Setup(enemyDamage);
                instance.Start();
                enemy.health -= enemyDamage;
            }
            if (myDamage >= 1)
            {
                DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                instance.Setup(myDamage);
                instance.Start();
                health -= myDamage;
            }

        }
        if (transform.tag.Contains("Sni") && enemy.tag.Contains("Sni"))
        {
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("Y"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("B"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (enemyDamage >= 1)
            {
                DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
                instance.Setup(enemyDamage);
                instance.Start();
                enemy.health -= enemyDamage;
            }
            if (myDamage >= 1)
            {
                DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                instance.Setup(myDamage);
                instance.Start();
                health -= myDamage;
            }

        }
        if (transform.tag.Contains("Sni") && !enemy.tag.Contains("Sni"))
        {
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("Y"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("B"))
            {
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) <= 1)
            {
                if (myDamage >= 1)
                {
                    DamageIcon instance = Instantiate(damageIcon, transform.position, Quaternion.identity);
                    instance.Setup(myDamage);
                    instance.Start();
                    health -= myDamage;
                }
                if (enemyDamage >= 1)
                {
                    DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
                    instance.Setup(enemyDamage);
                    instance.Start();
                    enemy.health -= enemyDamage;
                }
            }
            else if(Mathf.Abs(transform.position.x - enemy.transform.position.x) + Mathf.Abs(transform.position.y - enemy.transform.position.y) > 1)
            {
                if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("Y"))
                {
                    this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                if (this.transform.position.x - enemy.transform.position.x < 0 && this.tag.Contains("B"))
                {
                    this.transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                if (enemyDamage >= 1)
                {
                    DamageIcon instance = Instantiate(damageIcon, enemy.transform.position, Quaternion.identity);
                    instance.Setup(enemyDamage);
                    instance.Start();
                    enemy.health -= enemyDamage;
                }
            }
            
        }
        if(enemy.health <= 0)
        {
            Destroy(enemy.gameObject);
            GetWalkableTiles();
        }
        if (health <= 0)
        {
            gm.resetTiles();
            Destroy(this.gameObject);
        }
    }

    void GetWalkableTiles()
    {
        if(hasMoved == true)
        {
            return;
        }
        foreach(Tile tile in FindObjectsOfType<Tile>())
        {
            if(Mathf.Abs(this.transform.position.x - tile.transform.position.x) + Mathf.Abs(this.transform.position.y - tile.transform.position.y) <= tileSpeed)
            {
                if(tile.IsClear() == true)
                {
                    tile.Highlight();
                }
            }
        }
    }

    void getEnemies()
    {
        enemiesInRange.Clear();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            if (Mathf.Abs(this.transform.position.x - unit.transform.position.x) + Mathf.Abs(this.transform.position.y - unit.transform.position.y) <= attackRange)
            {
                if(unit.playerNumber != gm.playerTurn && hasAttacked == false)
                {
                    enemiesInRange.Add(unit);
                    unit.rend.color = attackColor;
                    if (this.transform.position.x - unit.transform.position.x < 0 && this.tag.Contains("Y"))
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (this.transform.position.x - unit.transform.position.x > 0 && this.tag.Contains("Y"))
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                    if (this.transform.position.x - unit.transform.position.x > 0 && this.tag.Contains("B"))
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (this.transform.position.x - unit.transform.position.x < 0 && this.tag.Contains("B"))
                    {
                        this.transform.localRotation = Quaternion.Euler(0, 180, 0);
                    }
                }
            }
        }
    }

    public void resetColors()
    {
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            unit.rend.color = Color.white;
        }
    }

    public void Move(Vector2 tilePos)
    {
        gm.resetTiles();
        gm.selectedUnit.GetComponent<SpriteRenderer>().sortingOrder = 10;//
        StartCoroutine(StartMovement(tilePos));
        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            if (unit.rend.color != Color.grey)
            {
                unit.rend.color = Color.white;
            }
        }
        getEnemies();
    }

    IEnumerator StartMovement(Vector2 tilePos)
    {
        while (transform.position.x != tilePos.x)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(tilePos.x, transform.position.y), moveSpeed * Time.deltaTime);
            gm.selectedUnit.GetComponent<SpriteRenderer>().sortingOrder = 10;
            if (tilePos.x > transform.position.x && this.tag.Contains("B"))
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (tilePos.x < transform.position.x && this.tag.Contains("Y"))
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            yield return null;
        }

        while (transform.position.y != tilePos.y)
        {
            gm.selectedUnit.GetComponent<SpriteRenderer>().sortingOrder = 10;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, tilePos.y), moveSpeed * Time.deltaTime);
            yield return null;
        }

        hasMoved = true;
        StartCoroutine(mirrorDelay());
        if (hasAttacked)
        {
            gm.selectedUnit.rend.color = Color.grey;
        }
        getEnemies();
        if(enemiesInRange.Count == 0)
        {
            foreach (Unit unit in FindObjectsOfType<Unit>())
            {
                if(unit.playerNumber != gm.playerTurn)
                {
                    unit.rend.color = Color.white;
                }
            }
            rend.color = Color.grey;
        }
    }

    IEnumerator mirrorDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (this.tag.Contains("Y") && enemiesInRange.Count == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (this.tag.Contains("B") && enemiesInRange.Count == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        this.GetComponent<SpriteRenderer>().sortingOrder = 10 - (int)transform.position.y;
    }
}
