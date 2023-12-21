using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public event Action<ScrollableList, string, string> OnItemSelected = delegate { };

    public bool horizontalScroll = true;
    public bool verticalScroll = true;
    public float itemHeight = 90;
    public Color itemColor = Color.gray;

    [Header("Internal References")]
    public ScrollRect scrollRect;
    public Transform content;
    public GameObject itemPrefab;
    public List<GameObject> items = new();

    private void OnEnable()
    {
        EnableScroll();
    }

    void EnableScroll()
    {
        scrollRect.horizontal = horizontalScroll;
        scrollRect.vertical = verticalScroll;
        scrollRect.verticalNormalizedPosition = 1;
    }

    public List<T> GetItems<T>() where T : ScrollableListItem
    {
        return items.Select(x => x.GetComponent<T>()).ToList();
    }

    public T AddItemAsType<T>(string title,
        string subtitle = "",
        Sprite sprite = null,
        Action<ScrollableListItem> onClick = null) where T : ScrollableListItem
    {
        return AddItem(title, subtitle, sprite, onClick).GetComponent<T>();
    }

    public GameObject AddItem(string title,
        string subtitle,
        Sprite sprite = null,
        Action<ScrollableListItem> onClick = null)
    {
        GameObject newItem = Instantiate(itemPrefab, content.position, Quaternion.identity);

        newItem.GetComponent<LayoutElement>().preferredHeight = itemHeight;
        newItem.GetComponent<Image>().color = itemColor;

        ScrollableListItem itemComp = newItem.GetComponent<ScrollableListItem>();
        itemComp.title.text = title;
        itemComp.subtitle.text = subtitle;

        if (sprite != null)
        {
            itemComp.image.sprite = sprite;
        }

        if (onClick != null)
        {
            itemComp.button.onClick.AddListener(() => onClick(itemComp));
        }

        itemComp.button.onClick.AddListener(() =>
        {
            OnItemSelected(this, title, subtitle);
        });

        newItem.transform.SetParent(content, false);
        items.Add(newItem);

        return newItem;
    }

    public void DestroyAllItems()
    {
        content.DestroyAllChildren();
        items.Clear();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}