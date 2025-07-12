using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddingCharacter : MonoBehaviour
{

    public bool isIn;
    public Character Character;
    public SpriteRenderer Renderer;
    public Character PlayerCharacter;
    public float Raise;

    void Start()
    {
        Character = GetComponent<Character>();   
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangerAlpha();
        Change();
    }

    public void Change()
    {
        if(isIn) {Character.HP -= Time.deltaTime;PlayerCharacter.HP += Time.deltaTime;}
        else if(!isIn && Character.HP<Character.MaxHP) { Character.HP += Time.deltaTime * Raise;}
        if(Character.HP < 1) { Character.HP = 1;}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCharacter = collision.GetComponent<Character>();
            isIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isIn = false;
        }
    }

    public void ChangerAlpha()
    {
            float Alpha = Character.HP/Character.MaxHP;
            Color color = Renderer.color;
            color.a = Alpha;
            Renderer.color = color;
    }
}
