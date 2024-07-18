using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public bool m_Dragging;
    public bool isTouching;
    public bool isScrolling;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private float targetScrollPos;

    public Scrollbar scrollbar;
    public RectTransform content;
    public float smoothSpeed = 5f;
    public float scaleMiddle = 1f;
    public float scaleOther = 0.8f;
    public float swipeThreshold = 50f; // Minimum distance for a swipe to be considered
    public float smallSwipeThreshold = 15f; // Minimum distance for a small swipe
    public int itemCount = 3;
    public float animTime = 0.25f;

    [Space(10)]
    public List<Transform> child;

    void Start()
    {
        if (content.childCount > 0)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).gameObject.activeSelf)
                {
                    child.Add(content.GetChild(i));
                }
            }

            itemCount = child.Count;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_Dragging = true;
        startTouchPos = eventData.position;
        Debug.Log("Drag started");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_Dragging = false;
        endTouchPos = eventData.position;
        HandleSwipe();
        Debug.Log("Drag ended");
    }

    void Update()
    {
        if (isScrolling)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetScrollPos, smoothSpeed * Time.deltaTime);

            if (Mathf.Approximately(scrollbar.value, targetScrollPos))
            {
                isScrolling = false;
                UpdateScale();
            }
        }
    }

    public void HandleSwipe()
    {
        float swipeDistance = endTouchPos.x - startTouchPos.x;

        if (Mathf.Abs(swipeDistance) >= smallSwipeThreshold && Mathf.Abs(swipeDistance) < swipeThreshold)
        {
            int currentIndex = Mathf.RoundToInt(scrollbar.value * (itemCount - 1));
            bool isSwipeRight = swipeDistance < 0;
            SmallSwipe(currentIndex, isSwipeRight);

            Debug.Log("Small Swipe Detected. Current Index: " + currentIndex);
        }
        else if (Mathf.Abs(swipeDistance) >= swipeThreshold)
        {
            int currentIndex = Mathf.RoundToInt(scrollbar.value * (itemCount - 1));
            bool isSwipeRight = swipeDistance < 0;
            SwipeToNextImage1(currentIndex, isSwipeRight);

            Debug.Log("Swipe Gesture Detected. Current Index: " + currentIndex);
        }
    }

    public void SwipeToNextImage1(int currentIndex, bool isSwipeRight)
    {
        if (isSwipeRight && currentIndex < itemCount - 1)
        {
            targetScrollPos = (currentIndex + 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Right. Target Scroll Position: " + targetScrollPos);
        }
        else if (!isSwipeRight && currentIndex > 0)
        {
            targetScrollPos = (currentIndex - 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Left. Target Scroll Position: " + targetScrollPos);
        }
        else if (isSwipeRight && currentIndex == itemCount - 1)
        {
            targetScrollPos = 1f;
            isScrolling = true;
            Debug.Log("Swipe Right at the Last Item. Ensuring it stays on the Last Item. Target Scroll Position: " + targetScrollPos);
        }
    }

    public void SwipeToNextImage2(int currentIndex, bool isSwipeRight)
    {
        if (isSwipeRight && currentIndex < itemCount - 1)
        {
            targetScrollPos = (currentIndex + 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Right. Target Scroll Position: " + targetScrollPos);
        }
        else if (!isSwipeRight && currentIndex > 0)
        {
            targetScrollPos = (currentIndex - 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Left. Target Scroll Position: " + targetScrollPos);
        }
        else if (isSwipeRight && currentIndex == itemCount - 1)
        {
            targetScrollPos = 1f;
            isScrolling = true;
            Debug.Log("Swipe Right at the Last Item. Ensuring it stays on the Last Item. Target Scroll Position: " + targetScrollPos);
        }
    }

    public void SmallSwipe(int currentIndex, bool isSwipeRight)
    {
        float scrollStep = 1f / (itemCount - 1) / 5f; // Change position by 1/5 of an item

        if (isSwipeRight)
        {
            targetScrollPos = Mathf.Clamp(scrollbar.value + scrollStep, 0f, 1f);
        }
        else
        {
            targetScrollPos = Mathf.Clamp(scrollbar.value - scrollStep, 0f, 1f);
        }
        isScrolling = true;
    }

    void UpdateScale()
    {
        for (int i = 0; i < itemCount; i++)
        {
            RectTransform item = content.GetChild(i).GetComponent<RectTransform>();
            float itemPos = i / (float)(itemCount - 1);

            if (Mathf.Abs(scrollbar.value - itemPos) < 0.05f) // Adjust tolerance as needed
            {
                item.DOScale(new Vector3(scaleMiddle, scaleMiddle, 1f), animTime);
            }
            else
            {
                item.DOScale(new Vector3(scaleOther, scaleOther, 1f), animTime);
            }
        }
    }
}
