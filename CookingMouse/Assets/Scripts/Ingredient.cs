using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum IngredientType
{
    Hamburger,
    Fries,
    Drink,
    Sauce
}

public class Ingredient : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;

    private Vector3 mouseOffset;
    private float mouseZCoord;

    public IngredientType ingredientType;//食材分类

    public bool isInCan = false;
    public bool isInHamArea = false;
    public bool isInFriesArea = false;

    public Vector2 originPos;
    public Vector2 LockPos;

    public GameObject lumpSaucePrefab;
    public Vector3 burgerSaucePos=new Vector3(0,0,2);
    public Vector3 friesSaucePos = new Vector3(0, 0, -2);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDragging = false;
        originPos = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - GetMouseWorldPos();
        
        // 拿起音效（6种不同）
        AudioManager.instance.PlayPickUp(gameObject.tag);
    }
    private void OnMouseUp()
    {
        isDragging = false;
        if (isInCan)
        {
            // 丢垃圾桶音效（通用）
            AudioManager.instance.PlayThrowTrash();
            Destroy(gameObject);
        }
        else if (isInHamArea && ingredientType == IngredientType.Hamburger)
        {
            transform.position = LockPos;
            originPos = LockPos;
            BurgerMaker.instance.AddIngredient(gameObject);
        }
        else if (isInFriesArea && ingredientType == IngredientType.Fries)
        {
            transform.position = LockPos;
            originPos = LockPos;
            FriesMaker.instance.AddIngredient(gameObject);
        }
        else if (ingredientType == IngredientType.Sauce)
        {
            Transform parent = GameObject.Find("Ingredient").transform;
            if (isInHamArea)
            {
                Instantiate(lumpSaucePrefab, burgerSaucePos,Quaternion.identity,parent.transform);
                BurgerMaker.instance.AddIngredient(gameObject);
            }
            else if (isInFriesArea)
            {
                Instantiate(lumpSaucePrefab, friesSaucePos, Quaternion.identity, parent.transform);
                FriesMaker.instance.AddIngredient(gameObject);
            }
            transform.position = originPos;
            
            // 放回原位音效（通用）
            AudioManager.instance.PlayPutBack();
        }
        else
        {
            transform.position = originPos;
            
            // 放回原位音效（通用）
            AudioManager.instance.PlayPutBack();
        }
    }
    private void OnMouseEnter()
    {
        transform.localScale += Vector3.one * 0.07f;
    }

    private void OnMouseExit()
    {
        transform.localScale -= Vector3.one * 0.07f;
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rb.MovePosition(GetMouseWorldPos() + mouseOffset);
        }

        if(!(ingredientType == IngredientType.Sauce)&&!isDragging)
        {
            if (BurgerMaker.instance.isGetBurger && isInHamArea)
            {
                Destroy(gameObject);
            }
            if (FriesMaker.instance.isGetFries && isInFriesArea)
            {
                Destroy(gameObject);
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mouseZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Can") && !(ingredientType == IngredientType.Sauce))
        {
            isInCan = true;
        }
        else if (other.CompareTag("BurgerArea") && (ingredientType == IngredientType.Hamburger
            || ingredientType == IngredientType.Sauce))
        {
            isInHamArea = true;
            isInFriesArea = false;

            // 进入制作区分音效
            if (ingredientType == IngredientType.Sauce)
                AudioManager.instance.PlayEnterSauce();
            else
                AudioManager.instance.PlayEnterNormal();
        }
        else if (other.CompareTag("FriesArea") && (ingredientType == IngredientType.Fries
            || ingredientType == IngredientType.Sauce))
        {
            isInFriesArea = true;
            isInHamArea = false;

            // 进入制作区分音效
            if (ingredientType == IngredientType.Sauce)
                AudioManager.instance.PlayEnterSauce();
            else
                AudioManager.instance.PlayEnterNormal();
        }
        else
        {
            transform.position = originPos;
            AudioManager.instance.PlayPutBack();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Can"))
        {
            isInCan = false;
        }
        else if (other.CompareTag("BurgerArea") && (ingredientType == IngredientType.Hamburger
            || ingredientType == IngredientType.Sauce))
        {
            isInHamArea = false;
        }
        else if (other.CompareTag("FriesArea") && (ingredientType == IngredientType.Fries
            || ingredientType == IngredientType.Sauce))
        {
            isInFriesArea = false;
        }
    }
}