using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public GameObject Prefab;
    [Header("½ÇÉ«ÊôÐÔ")]
    public float HP;
    public float MaxHP;
    public bool isDead;
    void Start()
    {
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
        if(MaxHP >= 200)
        {
            gameObject.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
    }
}
