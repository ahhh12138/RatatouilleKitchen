using UnityEngine;
using UnityEngine.UI;

public class BurgerMaker : MonoBehaviour
{
    public static BurgerMaker instance;

    [Header("汉堡食材虚影")]
    public Image imgBread;
    public Image imgMeat;
    public Image imgVegetables;
    public Image imgSauce;

    [Header("合成出来的汉堡预制体")]
    public GameObject burgerPrefab;

    private bool hasBread, hasMeat, hasVegetables, hasSauce;

    void Awake()
    {
        instance = this;
    }

    public void AddIngredient(GameObject food)
    {
        string foodName = food.name;

        if (foodName == "bread" && !hasBread)
        {
            hasBread = true;
            imgBread.color = Color.white;
        }
        else if (foodName == "meat" && !hasMeat)
        {
            hasMeat = true;
            imgMeat.color = Color.white;
        }
        else if (foodName == "vegetables" && !hasVegetables)
        {
            hasVegetables = true;
            imgVegetables.color = Color.white;
        }
        else if (foodName == "sauce" && !hasSauce)
        {
            hasSauce = true;
            imgSauce.color = Color.white;
        }

        CheckIsFull();
    }

    void CheckIsFull()
    {
        if (hasBread && hasMeat && hasVegetables && hasSauce)
        {
            MakeBurger();
        }
    }

    void MakeBurger()
    {
        Debug.Log("汉堡合成成功！");

        if (burgerPrefab != null)
        {
            // 汉堡生成在合成区正中间，一定能看见
            GameObject newBurger = Instantiate(burgerPrefab, transform);
            newBurger.transform.localPosition = Vector3.zero;
        }

        ResetMaker();
    }

    void ResetMaker()
    {
        hasBread = false;
        hasMeat = false;
        hasVegetables = false;
        hasSauce = false;

        imgBread.color = Color.gray;
        imgMeat.color = Color.gray;
        imgVegetables.color = Color.gray;
        imgSauce.color = Color.gray;
    }
}