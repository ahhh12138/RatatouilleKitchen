using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class IngredientManager : MonoBehaviour
{
    [Header("食材生成位置（自动找场景里现有食材）")]
    public List<Transform> breadSpawnPoints = new List<Transform>();
    public List<Transform> potatoSpawnPoints = new List<Transform>();
    public List<Transform> meatSpawnPoints = new List<Transform>();
    public List<Transform> vegetableSpawnPoints = new List<Transform>();
    public List<Transform> juiceSpawnPoints = new List<Transform>();

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
    private const int MaxCount = 3;

    void Start()
    {
        // 只记录位置，不生成新食材
        RecordExistIngredientPositions();

        InvokeRepeating("CheckAllButtons", 0, 1f);
    }

    void Update()
    {
        gameTime += Time.deltaTime;
    }

    // 记录场景里已经存在的食材位置
    void RecordExistIngredientPositions()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("bread"))
            breadSpawnPoints.Add(obj.transform);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("potato"))
            potatoSpawnPoints.Add(obj.transform);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("meat"))
            meatSpawnPoints.Add(obj.transform);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("vegetables"))
            vegetableSpawnPoints.Add(obj.transform);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("juice"))
            juiceSpawnPoints.Add(obj.transform);
    }

    void CheckAllButtons()
    {
        btnBread.interactable = CountIngredients("bread") == 0;
        btnPotato.interactable = CountIngredients("potato") == 0;
        btnMeat.interactable = CountIngredients("meat") == 0;
        btnVegetables.interactable = CountIngredients("vegetables") == 0;
        btnDrink.interactable = CountIngredients("juice") == 0;
    }

    int CountIngredients(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag).Length;
    }

    int GetBadRate()
    {
        if (gameTime < 60) return Random.Range(5, 11);
        if (gameTime < 180) return Random.Range(15, 26);
        return Random.Range(30, 41);
    }

    void SpawnSingleIngredient(GameObject good, GameObject bad, Transform spawnPoint)
    {
        int rate = Random.Range(0, 100);
        int badRate = GetBadRate();

        if (rate < badRate)
            Instantiate(bad, spawnPoint.position, Quaternion.identity);
        else
            Instantiate(good, spawnPoint.position, Quaternion.identity);
    }

    void SpawnBatchIngredients(GameObject good, GameObject bad, List<Transform> spawnPoints)
    {
        foreach (Transform t in spawnPoints)
        {
            SpawnSingleIngredient(good, bad, t);
        }
    }

    // 按钮点击
    public void OnClickBread() => SpawnBatchIngredients(bread, badBread, breadSpawnPoints);
    public void OnClickPotato() => SpawnBatchIngredients(potato, badPotato, potatoSpawnPoints);
    public void OnClickMeat() => SpawnBatchIngredients(meat, badMeat, meatSpawnPoints);
    public void OnClickVegetables() => SpawnBatchIngredients(vegetables, badVegetables, vegetableSpawnPoints);
    public void OnClickDrink() => SpawnBatchIngredients(drink, badDrink, juiceSpawnPoints);
}