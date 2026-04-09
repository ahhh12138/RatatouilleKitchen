using UnityEngine;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour
{
    [Header("食材生成位置")]
    public Transform spawnPoint;

    [Header("好食材预制体")]
    public GameObject bread;
    public GameObject potato;
    public GameObject meat;
    public GameObject vegetables;
    public GameObject drink;

    [Header("坏食材预制体")]
    public GameObject badBread;
    public GameObject badPotato;
    public GameObject badMeat;
    public GameObject badVegetables;
    public GameObject badDrink;

    [Header("对应按钮")]
    public Button btnBread;
    public Button btnPotato;
    public Button btnMeat;
    public Button btnVegetables;
    public Button btnDrink;

    private float gameTime = 0;

    void Start()
    {
        // 开局检查按钮状态
        InvokeRepeating("CheckAllButtons", 0, 1f);
    }

    void Update()
    {
        // 记录游戏时间（控制坏食材概率）
        gameTime += Time.deltaTime;
    }

    // 检查所有食材是否用完，控制按钮
    void CheckAllButtons()
    {
        btnBread.interactable = CheckIsEmpty("bread");
        btnPotato.interactable = CheckIsEmpty("potato");
        btnMeat.interactable = CheckIsEmpty("meat");
        btnVegetables.interactable = CheckIsEmpty("vegetables");
        btnDrink.interactable = CheckIsEmpty("juice");
    }

    // 判断某类食材是否为空
    bool CheckIsEmpty(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag) == null;
    }

    // 获取当前阶段的坏食材概率
    int GetBadRate()
    {
        if (gameTime < 60) return Random.Range(5, 11);    // 0-1分钟
        if (gameTime < 180) return Random.Range(15, 26);  // 1-3分钟
        return Random.Range(30, 41);                      // 3-5分钟
    }

    // 生成食材（好/坏）
    void SpawnIngredient(GameObject good, GameObject bad)
    {
        int rate = Random.Range(0, 100);
        int badRate = GetBadRate();

        if (rate < badRate)
        {
            Instantiate(bad, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(good, spawnPoint.position, Quaternion.identity);
        }
    }

    // 按钮点击方法
    public void OnClickBread() => SpawnIngredient(bread, badBread);
    public void OnClickPotato() => SpawnIngredient(potato, badPotato);
    public void OnClickMeat() => SpawnIngredient(meat, badMeat);
    public void OnClickVegetables() => SpawnIngredient(vegetables, badVegetables);
    public void OnClickDrink() => SpawnIngredient(drink, badDrink);
}