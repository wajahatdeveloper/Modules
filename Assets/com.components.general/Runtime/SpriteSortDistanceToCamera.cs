using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortDistanceToCamera : MonoBehaviour
{
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;
    private bool _isVisible = false;
    
    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameVisible()
    {
        _camera = Camera.main;
        _isVisible = true;
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;
    }

    private void Update()
    {
        if (!_isVisible) { return; }

        float distance = gameObject.DistanceTo(_camera.gameObject);
        distance *= 4;
        _spriteRenderer.sortingOrder = 50 - (int)distance;
    }
}
