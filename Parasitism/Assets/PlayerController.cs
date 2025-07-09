using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("按键")]
    public KeyCode Space;
    public KeyCode W;
    public KeyCode A;
    public KeyCode S;
    public KeyCode D;
    public KeyCode Shift;

    [Header("组件")]
    public Rigidbody2D rb;
    public PhysicsCheck PhysicsCheck;

    [Header("控制台参数")]
    public float JumpForce;
    public float DropSpeed;
    public float DropSpeedMax;
    public float DropSpeedInPudding;
    public float DashForce;
    public int faceDir = 1;
    public int upDir;
    public float MoveSpeed;
    public bool isJump;
    public bool isInPudding;

    [Header("冲刺冷却")]
    public float CoolDownTime;
    public float CoolDownTimer;
    public bool isCool;

    void Start()
    {
        CoolDownTimer = 0;
        isCool = false;
        isJump = false;
        rb = GetComponent<Rigidbody2D>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
    }

    void Update()
    {
        DashCool();
        if (Input.GetKeyDown(S)) upDir = -1; else if (Input.GetKeyDown(W)) upDir = 1;
        if (Input.GetKeyDown(A)) faceDir = -1; else if (Input.GetKeyDown(D)) faceDir = 1;

        DropSpeed = rb.velocity.y;
        if (!isInPudding && DropSpeed <= DropSpeedMax) DropSpeed = DropSpeedMax; else if (isInPudding && DropSpeed <= DropSpeedInPudding) DropSpeed = DropSpeedInPudding;

        if (Input.GetKey(Shift) && isInPudding)
        {
            Dash(); return;
        }

        if(PhysicsCheck.isGround||isInPudding) isJump = false;else isJump = true;//删去后半截变成二段跳
        if (!isJump && Input.GetKeyDown(Space))
        {
            Jump();return;
        }
        if (Input.GetKey(A)||Input.GetKey(D))
        {
            Move();return;
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(0,JumpForce), ForceMode2D.Impulse);
        isJump = true;
    }

    public void Move()
    { 
        float DropSpeed = rb.velocity.y;
        if (faceDir == -1) rb.velocity = new Vector2(faceDir * MoveSpeed, DropSpeed); else rb.velocity = new Vector2(faceDir * MoveSpeed, DropSpeed);
    }

    public void Dash()
    {
        if (!isCool)
        {
            if (Input.GetKey(W) || Input.GetKey(S)) { rb.AddForce(new Vector2(0, DashForce * upDir), ForceMode2D.Impulse);isCool = true; CoolDownTimer = CoolDownTime; }
            if (Input.GetKey(A) || Input.GetKey(D)) { rb.AddForce(new Vector2(DashForce * faceDir, 0), ForceMode2D.Impulse);isCool = true; CoolDownTimer = CoolDownTime; }
        }
    }

    public void DashCool()
    {
        if (isCool) {CoolDownTimer -= Time.deltaTime;}
        if (CoolDownTimer < 0) { isCool = false;}

    }
}
