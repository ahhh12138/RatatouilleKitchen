using UnityEngine;
using UnityEngine.UI;

public class FriesMaker : MonoBehaviour
{
    public static FriesMaker instance;
    public Button friesButton;

    [Header("薯条食材虚影")]
    public Image imgPotato;
    public Image imgSauce;

    [Header("合成出来的薯条预制体")]
    public GameObject friesPrefab;

    private bool hasPotato, hasSauce;
    public bool isGetFries=false;

    void Awake()
    {
        instance = this;
        friesButton.interactable = false;
    }

    public void AddIngredient(GameObject food)
    {
        if (food.CompareTag("potato") && !hasPotato)
        {
            hasPotato = true;
            //imgPotato.color = Color.white;
        }
        else if (food.CompareTag("sauce") && !hasSauce)
        {
            hasSauce = true;
            //imgSauce.color = Color.white;
        }

        CheckIsFull();
        AudioManager.instance.PlayLockSuccess();
    }

    void CheckIsFull()
    {
        if (hasPotato && hasSauce)
        {
            friesButton.interactable = true;
        }
    }

    public void OnFriesButtonClicked()
    {
        Debug.Log("合成薯条");
        if(!isGetFries)MakeFries();     
    }

    void MakeFries()
    {
        Debug.Log("薯条合成成功！");

        if (friesPrefab != null)
        {
            GameObject newFries = Instantiate(friesPrefab, transform);
            newFries.transform.localPosition = Vector3.zero;
            newFries.transform.localScale = new Vector2(30, 40);
            isGetFries = true;
        }
        Invoke(nameof(ResetMaker), 0.1f);
    }

    void ResetMaker()
    {
        hasPotato = false;
        hasSauce = false;

        //imgPotato.color = Color.gray;
        //imgSauce.color = Color.gray;

        isGetFries = false;
        friesButton.interactable= false;
    }
}