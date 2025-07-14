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
        // 检查面板是否需要打开且存在有效的道具
        if (isOpen && (PlayerController.canSmall || PlayerController.canSuper || PlayerController.canBig || PlayerController.canClimb))
        {
            Panel.SetActive(true);

            // 获取四种道具的计时器值
            float smallTimer = PlayerController.SmallTimer;
            float superTimer = PlayerController.SuperTimer;
            float bigTimer = PlayerController.BigTimer;
            float climbTimer = PlayerController.ClimbTimer;

            // 找到最大的计时器值
            float maxTimer = Mathf.Max(smallTimer, superTimer, bigTimer, climbTimer);

            // 根据最大计时器值显示对应的道具信息
            if (maxTimer == smallTimer && PlayerController.canSmall)
            {
                Image.sprite = SmallSprite;
                Description.text = $"You are Smaller in {smallTimer:F1}s";
            }
            else if (maxTimer == superTimer && PlayerController.canSuper)
            {
                Image.sprite = SuperSprite;
                Description.text = $"You can Jump Higher in {superTimer:F1}s";
            }
            else if (maxTimer == bigTimer && PlayerController.canBig)
            {
                Image.sprite = BigSprite;
                Description.text = $"You are Bigger in {bigTimer:F1}s";
            }
            else if (maxTimer == climbTimer && PlayerController.canClimb)
            {
                Image.sprite = ClimbSprite;
                Description.text = $"You can Climb Wall in {climbTimer:F1}s";
            }

            Time.timeScale = 0f;
        }
        else
        {
            Panel.SetActive(false);
        }
    }

    public void ClosePanel()
    {
        Time.timeScale = 1f;
        Panel.SetActive(false);
        PlayerController.isOpen = false;
    }
}