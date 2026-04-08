using UnityEngine;
using UnityEngine.UI;

public class BurgerMaker : MonoBehaviour
{
    public static BurgerMaker instance;
    public Button burgerButton;

    [Header("汉堡食材虚影")]
    public Image imgBread;
    public Image imgMeat;
    public Image imgVegetables;
    public Image imgSauce;

    [Header("汉堡预制体")]
    public GameObject burgerPrefab;

    private bool hasBread, hasMeat, hasVegetables, hasSauce;
    public bool isGetBurger=false;

    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        burgerButton.interactable = false;
    }

    public void AddIngredient(GameObject food)
    {
        string foodName = food.name;

        if (foodName == "bread" && !hasBread)
        {
            hasBread = true;
            //imgBread.color = Color.white;
        }
        else if (foodName == "meat" && !hasMeat)
        {
            hasMeat = true;
            //imgMeat.color = Color.white;
        }
        else if (foodName == "vegetables" && !hasVegetables)
        {
            hasVegetables = true;
            //imgVegetables.color = Color.white;
        }
        else if (foodName == "sauce" && !hasSauce)
        {
            hasSauce = true;
            //imgSauce.color = Color.white;
        }

        CheckIsFull();
    }

    void CheckIsFull()
    {
        if (hasBread && hasMeat && hasVegetables && hasSauce)
        {
            burgerButton.interactable = true;
        }
    }

    public void OnBurgerButtonClicked()
    {
        Debug.Log("合成汉堡");
        if (!isGetBurger) 
        {
            MakeBurger();
        }
    }

    void MakeBurger()
    {
        Debug.Log("汉堡合成成功！");
        if (burgerPrefab != null)
        {
            GameObject newBurger = Instantiate(burgerPrefab, transform);
            newBurger.transform.localPosition = Vector3.zero+Vector3.left*120+Vector3.up*50;
            newBurger.transform.localScale = new Vector2(618, 309);
        }
        isGetBurger = true;
        Invoke(nameof(ResetMaker), 0.1f);
    }

    void ResetMaker()
    {
        hasBread = false;
        hasMeat = false;
        hasVegetables = false;
        hasSauce = false;

        //imgBread.color = Color.gray;
        //imgMeat.color = Color.gray;
        //imgVegetables.color = Color.gray;
        //imgSauce.color = Color.gray;

        burgerButton.interactable = false;
        isGetBurger =false;
    }
}