using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralScript : MonoBehaviour
{
    public Character Character;
    public float Supplement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character = collision.gameObject.GetComponent<Character>();
            if (Character == null) return;
            Character.MaxHP += Supplement * 0.5f;
            if (Character.HP + Supplement <= Character.MaxHP) Character.HP += Supplement; else Character.HP = Character.MaxHP;
            gameObject.SetActive(false);
        }
    }

}
