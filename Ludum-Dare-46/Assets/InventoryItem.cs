﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;


public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color outlineHoverColor;

    internal bool selected;

    public UnityEvent OnHover;
    public UnityEvent OnStopHover;
    private Outline outline;

    private PlayerInventory inventory;

    public int index;
    private Button button;
    private float maxScale = 1.2f;
    internal readonly static float easeTime = 0.2f;

    public static bool hoveringBox;

    private RectTransform textBox;

    internal Seed selsctedSeed;

    private void Start()
    {

        button = GetComponent<Button>();
        outline = GetComponent<Outline>();
        inventory = GetComponentInParent<PlayerInventory>();
        if (inventory == null) print("null inventory");
        button.onClick.AddListener(EquipThis);

        transform.localScale = Vector2.right;

        LeanTween.scaleY(gameObject, 1f, easeTime);

        outline.effectColor = Color.black;
    }

    public void SetCounter(int counter)
    {
        transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().SetText(counter.ToString());
    }

    public void SetUISprite(Sprite sprite)
    {
        transform.GetChild(1).GetComponent<Image>().sprite = sprite;
    }

    public void EquipThis()
    {
        inventory.Equip(this);
    }

    private void ScaleButton(bool scaleUp)
    {
        Vector2 newScale = scaleUp ? new Vector2(maxScale, maxScale) : new Vector2(1f,1f);
        LeanTween.scale(gameObject, newScale, easeTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        hoveringBox = true;

        OnHover.Invoke();
        ScaleButton(true);
        LeanTween.value(gameObject, outline.effectColor, outlineHoverColor, easeTime).setOnUpdate(SetOutlineColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoveringBox = false;

        OnStopHover.Invoke();
        ScaleButton(false);
        LeanTween.value(gameObject, outline.effectColor, Color.black, easeTime).setOnUpdate(SetOutlineColor);
    }

    private void SetOutlineColor(Color c)
    {
        outline.effectColor = c;
    }
}
