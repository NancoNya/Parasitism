using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController PlayerController;

    public float SmallTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerController = collision.gameObject.GetComponent<PlayerController>();
            if (PlayerController == null) return;
            PlayerController.canSmall = true;
            PlayerController.SmallTimer = SmallTime;
            gameObject.SetActive(false);
        }
    }
}
