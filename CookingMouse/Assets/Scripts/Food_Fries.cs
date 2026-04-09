using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Food_Fries : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging;

    private Vector3 mouseOffset;
    private float mouseZCoord;

    public bool isInCan = false;
    public bool isInTask = false; // 新加：是否拖到任务区

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

        if (isInCan)
        {
            Destroy(gameObject);
        }
        // 新加：如果拖到任务图标 且 匹配成功
        else if (isInTask)
        {
            TaskIconRandom task = FindObjectOfType<TaskIconRandom>();
            if (task != null)
            {
                task.OnDropFood("Fries"); // 提交薯条
                Destroy(gameObject); // 提交后吃掉食物
            }
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

        // 新加：碰到任务图标
        if (other.CompareTag("TaskSlot"))
        {
            isInTask = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Can"))
        {
            isInCan = false;
        }

        // 新加：离开任务图标
        if (other.CompareTag("TaskSlot"))
        {
            isInTask = false;
        }
    }
}