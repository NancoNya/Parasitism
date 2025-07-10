using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public PhysicsCheck physicsCheck;
    public float OriginalRaduis;
    public GameObject Prefab;
    [Header("角色属性")]
    public float HP;
    public float MaxHP;
    public bool isDead;
    void Start()
    {
        isDead = false;
        HP = MaxHP;
        physicsCheck = gameObject.GetComponent<PhysicsCheck>();
        OriginalRaduis = physicsCheck.checkRaduis;
    }

    void Update()
    {
        Dead();
        Change();
    }

    public void Dead()
    {
        if ((HP+15) <= 0)
        {
            HP = 0; isDead = true;
            Vector3 OriginalPosition = gameObject.transform.position;
            Quaternion OriginalRotation = gameObject.transform.rotation;
            if(Prefab != null) Instantiate(Prefab, OriginalPosition, OriginalRotation);
            gameObject.SetActive(false);
        }
    }
    public void Change()
    {
        int Num = (int)(HP+15) / 15; 
        switch (Num)
        {
            case 0:
                gameObject.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                physicsCheck.checkRaduis = OriginalRaduis * 0.75f;
                break;
            case 1:
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                physicsCheck.checkRaduis = OriginalRaduis;
                break;
            case 2:
                gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                physicsCheck.checkRaduis = OriginalRaduis * 1.5f;
                break;
            case 3:
                gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
                physicsCheck.checkRaduis = OriginalRaduis * 3f;
                break;
        }
    }
}
