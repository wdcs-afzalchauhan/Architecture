using UnityEngine;

public class ScrollManager : MonoBehaviour
{
    private static ScrollManager _instance;
    public static ScrollManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("ScrollManager");
                _instance = go.AddComponent<ScrollManager>();
            }
            return _instance;
        }
    }

    public CustomScrollRect activeScrollRect;

    public void RegisterScrollRect(CustomScrollRect scrollRect)
    {
        activeScrollRect = scrollRect;
    }

    public void UnregisterScrollRect(CustomScrollRect scrollRect)
    {
        if (activeScrollRect == scrollRect)
        {
            activeScrollRect = null;
        }
    }

    public bool IsScrollRectActive(CustomScrollRect scrollRect)
    {
        return activeScrollRect == scrollRect;
    }

    public bool IsAnyScrollRectActive()
    {
        return activeScrollRect != null;
    }
}
