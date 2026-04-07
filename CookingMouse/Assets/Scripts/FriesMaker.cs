using UnityEngine;
using UnityEngine.UI;

public class FriesMaker : MonoBehaviour
{
    public static FriesMaker instance;

    [Header("薯条食材虚影")]
    public Image imgPotato;
    public Image imgSauce;

    [Header("合成出来的薯条预制体")]
    public GameObject friesPrefab;

    private bool hasPotato, hasSauce;

    void Awake()
    {
        instance = this;
    }

    public void AddIngredient(GameObject food)
    {
        string foodName = food.name;

        if (foodName == "potato" && !hasPotato)
        {
            hasPotato = true;
            imgPotato.color = Color.white;
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
        if (hasPotato && hasSauce)
        {
            MakeFries();
        }
    }

    void MakeFries()
    {
        Debug.Log("薯条合成成功！");

        if (friesPrefab != null)
        {
            GameObject newFries = Instantiate(friesPrefab, transform);
            newFries.transform.localPosition = Vector3.zero;
        }

        ResetMaker();
    }

    void ResetMaker()
    {
        hasPotato = false;
        hasSauce = false;

        imgPotato.color = Color.gray;
        imgSauce.color = Color.gray;
    }
}