using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIcon : MonoBehaviour
{
    public Sprite[] damageSprites;
    public float lifetime;
    public GameObject effect;

    public void Start()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
        Invoke("Destruction", lifetime);
    }

    public void Setup(int damage)
    {
        GetComponent<SpriteRenderer>().sprite = damageSprites[damage - 1];
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
