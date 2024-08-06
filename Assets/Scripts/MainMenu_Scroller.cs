using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Scroller : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private float _x, _y;

    void Start()
    {
        if (_img == null)
        {
            Debug.LogError("RawImage component is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_img != null)
        {
            _img.uvRect = new Rect(_img.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _img.uvRect.size);
        }
    }
}
