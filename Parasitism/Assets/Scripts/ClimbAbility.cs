using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAbility : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController PlayerController;

    public float ClimbTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            PlayerController = collision.gameObject.GetComponent<PlayerController>();
            if (PlayerController == null) return;
            PlayerController.canClimb = true;
            PlayerController.cantEverClimb = false;
            PlayerController.ClimbTimer = ClimbTime;
            gameObject.SetActive(false);
        }
    }
}
