using System;
using UnityEngine;
using UnityEngine.UI;

internal class MiniMapController : MonoBehaviour
{
    [SerializeField] private Camera _minimapCamera;
    [SerializeField] private RenderTexture _minimapTexture;
    [SerializeField] private RawImage _minimapFrame;
    [SerializeField] private int cameraOpenedSize = 400;
    [SerializeField] private int cameraClosedSize = 30;
    [SerializeField] Vector2 openedPosition;
    [SerializeField] Vector2 closedPosition;
    [SerializeField] Vector2 minimapOpenedSize;
    [SerializeField] Vector2 minimapClosedSize;
    private RectTransform RectTransform;
    private bool isMapOpen;
    private void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }
    internal void Toggle()
    {
        if (isMapOpen)
            Hide();
        else
            Open();
    }
    private void Open()
    {
        isMapOpen = true;
        _minimapCamera.orthographicSize = 400;
        RectTransform.anchoredPosition = openedPosition;
        RectTransform.sizeDelta = minimapOpenedSize;
        _minimapFrame.gameObject.SetActive(false);
    }
    private void Hide()
    {
        isMapOpen = false;
        _minimapCamera.orthographicSize = 30;
        RectTransform.anchoredPosition = closedPosition;
        RectTransform.sizeDelta = minimapClosedSize;
        _minimapFrame.gameObject.SetActive(true);
    }
}