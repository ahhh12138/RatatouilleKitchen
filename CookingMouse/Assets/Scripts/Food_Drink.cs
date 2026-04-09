using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Drink : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;

    private Vector3 mouseOffset;
    private float mouseZCoord;

    public bool isInCan = false;
    public bool isInTaskSlot = false; // 任务区域

    public Vector2 originPos;

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
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // 扔进垃圾桶
        if (isInCan)
        {
            Destroy(gameObject);
        }
        // 拖到任务栏提交
        else if (isInTaskSlot)
        {
            SubmitTask();
        }
        // 放回原位
        else
        {
            transform.position = originPos;
        }
    }

    void SubmitTask()
    {
        TaskIconRandom task = FindObjectOfType<TaskIconRandom>();
        if (task != null)
        {
            task.OnDropFood("Drink");
            Destroy(gameObject);
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
        else if (other.CompareTag("TaskSlot"))
        {
            isInTaskSlot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Can"))
        {
            isInCan = false;
        }
        else if (other.CompareTag("TaskSlot"))
        {
            isInTaskSlot = false;
        }
    }
}