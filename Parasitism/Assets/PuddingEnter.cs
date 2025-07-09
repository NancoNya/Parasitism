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
        rb.gravityScale = isInPudding ? 0.2f : 2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Pudding"))return;
        isInPudding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Pudding")) return;
        isInPudding = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (Input.GetKeyDown(PlayerController.Shift)){
        //    if (Input.GetKeyDown(PlayerController.W) || Input.GetKeyDown(PlayerController.S)) rb.AddForce(new Vector2(0, DashForce * PlayerController.upDir), ForceMode2D.Impulse);
        //    if (Input.GetKeyDown(PlayerController.A) || Input.GetKeyDown(PlayerController.D)) rb.AddForce(new Vector2(DashForce * PlayerController.faceDir, 0), ForceMode2D.Impulse);
        //}
    }
}
