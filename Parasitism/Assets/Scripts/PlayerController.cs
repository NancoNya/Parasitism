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
    public PuddingEnter PuddingEnter;
    public Character Character;
    public Transform Transform;

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

    [Header("攀爬功能")]
    public GameObject ClimbObject;
    public bool canClimb;
    public float ClimbForce;
    public bool cantEverClimb;
    public float everClimbTime;
    public float everClimbTimer;
    public float ClimbTimer;

    [Header("超級跳躍")]
    public GameObject SuperObject;
    public bool canSuper;
    public float SuperForce;
    public float NormalForce;
    public float SuperTimer;

    [Header("縮小道具")]
    public GameObject SmallObject;
    public bool canSmall;
    public bool isSmall;
    public float SmallTimer;
    public int SmallCnt;    
    
    [Header("縮小道具")]
    public GameObject BigObject;
    public bool canBig;
    public bool isBig;
    public float BigTimer;
    public int BigCnt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
        PuddingEnter = GetComponent<PuddingEnter>();
        Character = GetComponent<Character>();
        NormalForce = PuddingEnter.JumpForceNormal;
        CoolDownTimer = 0;ClimbTimer = 1;everClimbTimer = 0;SmallCnt = 0;BigCnt = 0;
        isCool = false;cantEverClimb = false;canClimb = false;
        isJump = false;
        isSmall = false;isBig = false;
    }

    void Update()
    {
        Character.Turn = faceDir;
        DashCool();
        SuperJump();
        Small();
        Big();
        Climb();
        if (Input.GetKeyDown(S)) upDir = -1; else if (Input.GetKeyDown(W)||Input.GetKeyDown(Space)) upDir = 1;
        if (Input.GetKeyDown(A)) faceDir = -1; else if (Input.GetKeyDown(D)) faceDir = 1;

        DropSpeed = rb.velocity.y;
        if (!isInPudding && DropSpeed <= DropSpeedMax) DropSpeed = DropSpeedMax; else if (isInPudding && DropSpeed <= DropSpeedInPudding) DropSpeed = DropSpeedInPudding;

        if (Input.GetKey(Shift) && isInPudding)
        {
            Dash(); return;
        }

        if(PhysicsCheck.isGround||isInPudding) isJump = false;
        //else isJump = true;//删去后半截变成二段跳
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

    public float Turn;
    public void Move()
    {

        float DropSpeed = rb.velocity.y;
        rb.velocity = new Vector2(faceDir * MoveSpeed, DropSpeed);
    }

    #region 布丁内衝刺相關

    public void Dash()
    {
        if (!isCool)
        {
            if (Input.GetKey(W) || Input.GetKey(S) || Input.GetKeyDown(Space)) { rb.AddForce(new Vector2(0, DashForce * upDir), ForceMode2D.Impulse);isCool = true; CoolDownTimer = CoolDownTime; }
            if (Input.GetKey(A) || Input.GetKey(D)) { rb.AddForce(new Vector2(DashForce * faceDir, 0), ForceMode2D.Impulse);isCool = true; CoolDownTimer = CoolDownTime; }
        }
    }

    public void DashCool()
    {
        if (isCool) {CoolDownTimer -= Time.deltaTime;}
        if (CoolDownTimer <= 0) { isCool = false;}

    }

    #endregion

    #region 攀爬相關

    public void Climb()
    {
        ClimbRespawn();
        ClimbCool();//阻斷攀爬功能
        EverClimbCool();
        if (canClimb && PhysicsCheck.isWall && !cantEverClimb)
        {
            if (Input.GetKey(W)) { rb.AddForce(new Vector2(0, ClimbForce), ForceMode2D.Impulse); cantEverClimb = true; everClimbTimer = everClimbTime; }
            if (Input.GetKey(S)) { rb.AddForce(new Vector2(0, -1*ClimbForce), ForceMode2D.Impulse); cantEverClimb = true; everClimbTimer = everClimbTime; }
        }
    }
    public void ClimbCool()
    {
        if (canClimb) { ClimbTimer -= Time.deltaTime; }
        if (ClimbTimer <= 0 ) { canClimb = false; }
    }
    public void EverClimbCool()
    {
        if (cantEverClimb) { everClimbTimer -= Time.deltaTime; }
        if (everClimbTimer <= 0) { cantEverClimb = false; }
    }


    public void ClimbRespawn()
    {
        if (ClimbObject == null) return;
        if (!canClimb && ClimbTimer <= 0) ClimbObject.SetActive(true);
    }
    #endregion

    #region 超高跳躍

    public void SuperJump()
    {
        SuperRespawn();
        SuperCool();
        if (canSuper) PuddingEnter.JumpForceNormal = SuperForce; 
        if(!canSuper) PuddingEnter.JumpForceNormal = NormalForce;
    }

    public void SuperCool()
    {
        if (canSuper) { SuperTimer -= Time.deltaTime; }
        if (SuperTimer <= 0) { canSuper = false; }
    }

    public void SuperRespawn()
    {
        if (SuperObject == null) return;
        if (!canSuper && SuperTimer <= 0) SuperObject.SetActive(true);
    }
    #endregion

    #region 縮小道具

    public void Small()
    {
        SmallCool();
        SmallRespawn();
        if (canSmall && !isSmall)
        {
            Character.HP -= 15;
            SmallCnt = 1;
            isSmall = true;
        }
    }
    public void SmallCool()
    {
        if (canSmall) { SmallTimer -= Time.deltaTime; }
        if (SmallTimer <= 0 && SmallCnt == 1) { canSmall = false; isSmall = false; SmallCnt--; Character.HP += 15; }
    }

    public void SmallRespawn()
    {
        if (SmallObject == null) return;
        if (!canSmall && !isSmall) { SmallObject.SetActive(true); }
    }

    #endregion

    #region 放大道具

    public void Big()
    {
        BigCool();
        BigRespawn();
        if (canBig && !isBig)
        {
            Character.MaxHP += 15;
            Character.HP += 15;
            BigCnt = 1;
            isBig = true;
        }
    }
    public void BigCool()
    {
        if (canBig) { BigTimer -= Time.deltaTime; }
        if (BigTimer <= 0 && BigCnt == 1) { canBig = false; isBig = false; BigCnt--; Character.HP -= 15; Character.MaxHP -= 15; }
    }

    public void BigRespawn()
    {
        if (BigObject == null) return;
        if (!canBig && !isBig) { BigObject.SetActive(true); }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Climb")) ClimbObject = collision.gameObject;
        if (collision.gameObject.CompareTag("Jump")) SuperObject = collision.gameObject;
        if (collision.gameObject.CompareTag("Small")) SmallObject = collision.gameObject;
        if (collision.gameObject.CompareTag("Big")) BigObject = collision.gameObject;
    }
}
