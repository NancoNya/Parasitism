using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Character character;
    public Rigidbody2D rb;
    public float AttackPower;
    public float Force;
    void Start()
    {

    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        character = collision.GetComponent<Character>();
        rb = collision.GetComponent<Rigidbody2D>();
        if (character == null) return;
        character.HP -= AttackPower;
        rb.AddForce(new Vector2(Force,0f),ForceMode2D.Impulse);
    }
}
