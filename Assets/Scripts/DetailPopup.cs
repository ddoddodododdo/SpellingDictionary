using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPopup : MonoBehaviour
{
    public Button CloseButton;

    private void Awake()
    {
        CloseButton.onClick.AddListener(() =>
        {
            CloseDetailPopup();
        });
    }

    public void CloseDetailPopup()
    {
        gameObject.SetActive(false);
    }
}
