using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private PhysicsCheck PhysicsCheck;
    [SerializeField] private Vector3 RespawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        PhysicsCheck = GetComponent<PhysicsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhysicsCheck.isGround)RespawnPoints = transform.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            transform.position = RespawnPoints;
            Character character = GetComponent<Character>();
        }
    }
}
