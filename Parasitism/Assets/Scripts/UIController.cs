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

    [Header("道具获得")]
    public GameObject Panel;
    public bool isOpen;
    public TextMeshProUGUI Description;
    public Image Image;
    public Sprite SmallSprite;
    public Sprite BigSprite;
    public Sprite SuperSprite;
    public Sprite ClimbSprite;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        Character = Player.GetComponent<Character>();
    }

    private void Update()
    {
        isOpen = PlayerController.isOpen;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        ChangeHP();
        OpenPanel();
    }

    public void ChangeHP()
    {
        HP.text = "HP:" + (int)(Character.HP) + "/" + (int)(Character.MaxHP);
    }

    public void OpenPanel()
    {
        if (isOpen&& (PlayerController.canSmall || PlayerController.canSuper || PlayerController.canBig || PlayerController.canClimb))
        {
            Panel.SetActive(isOpen);
            if (PlayerController.canSmall) { Image.sprite = SmallSprite; Description.text = "You are Smaller in 10s"; }
            if (PlayerController.canSuper) { Image.sprite = SuperSprite; Description.text = "You can Jump Higher in 10s"; }
            if (PlayerController.canBig)   {Image.sprite = BigSprite; Description.text = "You are Bigger in 10s"; }
            if (PlayerController.canClimb) {Image.sprite = ClimbSprite; Description.text = "You can Climb Wall in 10s"; }
            Time.timeScale = 0f;
        }

    }

    public void ClosePanel()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
        PlayerController.isOpen = false;
    }
}
