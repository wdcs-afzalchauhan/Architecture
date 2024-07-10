using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class NewSwipe : MonoBehaviour
{
    public Scrollbar scrollbar;
    public RectTransform content;
    public float smoothSpeed = 5f;
    public float scaleMiddle = 1f;
    public float scaleOther = 0.8f;
    public float swipeThreshold = 50f; // Minimum distance for a swipe to be considered
    public float smallSwipeThreshold = 15f; // Minimum distance for a small swipe

    [Space(10), Header("animTime")]
    public float animTime = 0.25f;

    [Space(10)]
    public int itemCount;
    private bool isScrolling = false;
    private float targetScrollPos;

    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    private bool isTouching = false;

    public int Crt;

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

    void Update()
    {
        if (ScrollManager.Instance.activeScrollRect != null)
            return;

        if (isScrolling)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetScrollPos, smoothSpeed * Time.deltaTime);

            if (Mathf.Approximately(scrollbar.value, targetScrollPos))
            {
                isScrolling = false;
            }
        }

        UpdateScale();

        // Detect swipe gestures
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
            isTouching = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPos = Input.mousePosition;
            isTouching = false;
            HandleSwipe();
        }
    }

    private void HandleSwipe()
    {
        float swipeDistance = endTouchPos.x - startTouchPos.x;

        if (Mathf.Abs(swipeDistance) >= smallSwipeThreshold && (Mathf.Abs(swipeDistance) < swipeThreshold))
        {
            int currentIndex = Mathf.RoundToInt(scrollbar.value * (itemCount - 1));
            bool isSwipeRight = swipeDistance < 0;
            SmallSwipe(currentIndex, isSwipeRight);
            SwipeToNextImage2(currentIndex, isSwipeRight);

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
            targetScrollPos = 1;//(currentIndex + 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Right. Target Scroll Position: " + targetScrollPos);
        }
        else if (!isSwipeRight && currentIndex > 0)
        {
            targetScrollPos = 0;//(currentIndex - 1) / (float)(itemCount - 1);
            isScrolling = true;
            Debug.Log("Swipe Left. Target Scroll Position: " + targetScrollPos);
        }
        else if (isSwipeRight && currentIndex == itemCount - 1)
        {
            // Additional check to ensure it stays on the last item
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
            // Additional check to ensure it stays on the last item
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
        float middleScrollPos = 0.5f;
        float tolerance = 0.05f;

        for (int i = 0; i < itemCount; i++)
        {
            RectTransform item = content.GetChild(i).GetComponent<RectTransform>();
            float itemPos = i / (float)(itemCount - 1);

            if (Mathf.Abs(scrollbar.value - itemPos) < tolerance)
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
