using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Ingredient_lumpSauce : MonoBehaviour
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
    public Vector3 burgerSaucePos = new Vector3(0, 0, 2);
    public Vector3 friesSaucePos = new Vector3(0, 0, -2);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDragging = false;
        isInCan = false;
        isInHamArea = false;
        isInFriesArea = false;
        originPos = transform.position;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        mouseZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        mouseOffset = transform.position - GetMouseWorldPos();
    }
    private void OnMouseUp()
    {
        isDragging = false;
        if (isInCan)
        {
            Destroy(gameObject);
        }
        else if (isInHamArea && ingredientType == IngredientType.Hamburger)
        {
            transform.position = LockPos;
            originPos = LockPos;
            BurgerMaker.instance.AddIngredient(gameObject);
        }
        else
        {
            transform.position = originPos;
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

        if (!(ingredientType == IngredientType.Sauce))
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
        if (other.CompareTag("Can"))
        {
            isInCan = true;
        }
        else if (other.CompareTag("BurgerArea") && (ingredientType == IngredientType.Hamburger
            || ingredientType == IngredientType.Sauce))
        {
            isInHamArea = true;
            isInFriesArea = false;
        }
        else if (other.CompareTag("FriesArea") && (ingredientType == IngredientType.Fries
            || ingredientType == IngredientType.Sauce))
        {
            isInFriesArea = true;
            isInHamArea = false;
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