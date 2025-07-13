using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI HP;
    public PlayerController PlayerController;
    public Character Character;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        Character = Player.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeHP();
    }

    public void ChangeHP()
    {
        HP.text = "HP:" + (int)(Character.HP) + "/" + (int)(Character.MaxHP);
    }
}
