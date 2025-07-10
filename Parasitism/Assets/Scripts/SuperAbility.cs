using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController PlayerController;

    public float SuperTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerController = collision.gameObject.GetComponent<PlayerController>();
            if (PlayerController == null) return;
            PlayerController.canSuper = true;
            PlayerController.SuperTimer = SuperTime;
            gameObject.SetActive(false);
        }
    }
}
