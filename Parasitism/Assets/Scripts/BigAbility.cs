using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController PlayerController;

    public float BigTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerController = collision.gameObject.GetComponent<PlayerController>();
            if (PlayerController == null) return;
            PlayerController.canBig = true;
            PlayerController.BigTimer = BigTime;
            gameObject.SetActive(false);
        }
    }
}
