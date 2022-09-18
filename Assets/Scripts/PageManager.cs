using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public List<Sprite> WordSpriteList;
    public List<Sprite> DetailSpriteList;

    public List<Image> WordImageList;
    public List<Button> WordButtonList;

    public Image DetailPopupImage;

    private int _clickIndex = 0;
    private int _pageIndex = 0;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        for(int i = 0; i < WordButtonList.Count; i++)
        {
            WordButtonList[i].onClick.AddListener(() => {
                _clickIndex = i;
                ShowPage();
                });
        }
    }
    
    private void ShowPage()
    {
        DetailPopupImage.sprite = DetailSpriteList[_clickIndex * (_pageIndex + 1)];
        DetailPopupImage.gameObject.SetActive(true);
    }



}
