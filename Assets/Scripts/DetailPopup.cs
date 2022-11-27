using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPopup : MonoBehaviour
{
    public Button CloseButton;
    public Image DetailImage;

    public List<GameObject> HideButtons;

    private void Awake()
    {
        CloseButton.onClick.AddListener(() =>
        {
            CloseDetailPopup();
        });
    }

    private void OnEnable()
    {
        foreach (var button in HideButtons)
        {
            button.SetActive(false);
        }
    }

    public void CloseDetailPopup()
    {
        foreach(var button in HideButtons)
        {
            button.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
