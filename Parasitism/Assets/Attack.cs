using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Characcter character;
    public float AttackPower;
    void Start()
    {
    }

    void Update()
    {
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        character = collision.GetComponent<Characcter>();
        if (character == null) return;
        character.HP -= AttackPower;
    }
}
