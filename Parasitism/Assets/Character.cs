using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float Multiply;//Gizmos的補償
    public GameObject Prefab;
    [Header("角色属性")]
    public float HP;
    public float MaxHP;
    public bool Size1;
    public bool isDead;
    void Start()
    {
        Size1 = false;
        isDead = false;
        HP = MaxHP;
    }

    void Update()
    {
        Dead();
        Change();
    }

    public void Dead()
    {
        if (HP <= 0)
        {
            HP = 0; isDead = true;
            Vector3 OriginalPosition = gameObject.transform.position;
            Quaternion OriginalRotation = gameObject.transform.rotation;
            Instantiate(Prefab,OriginalPosition,OriginalRotation);
            gameObject.SetActive(false);
        }
    }
    public void Change()
    {
        if(MaxHP >= 200 && Size1 == false)
        {
            gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            PhysicsCheck physicsCheck = gameObject.GetComponent<PhysicsCheck>();
            physicsCheck.checkRaduis= physicsCheck.checkRaduis * Multiply;
            Size1 = true;
        }
    }
}
