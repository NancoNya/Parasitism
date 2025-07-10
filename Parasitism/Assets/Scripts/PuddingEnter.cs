using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddingEnter : MonoBehaviour
{
    [Header("²ÎÊý")]
    public float MoveSpeedInPudding;
    public float MoveSpeedNormal;
    public float JumpForceInPudding;
    public float JumpForceNormal;
    //public float DashForce;
    public Rigidbody2D rb;
    public bool isInPudding;
    public PlayerController PlayerController;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        PlayerController.isInPudding = isInPudding;
        PlayerController.MoveSpeed = isInPudding ? MoveSpeedInPudding:MoveSpeedNormal;
        PlayerController.JumpForce = isInPudding ? JumpForceInPudding:JumpForceNormal;
        rb.gravityScale = isInPudding ? 0.2f : 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Pudding"))return;
        isInPudding = true;
        float velocityX = PlayerController.rb.velocity.x;
        float velocityY = PlayerController.rb.velocity.y;
        PlayerController.rb.velocity = new Vector2 (velocityX *0.4f, velocityY *0.2f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Pudding")) return;
        isInPudding = false;
    }

}
