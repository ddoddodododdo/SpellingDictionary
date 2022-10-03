using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    //모든 스프라이트
    public List<Sprite> WordSpriteList;
    public List<Sprite> DetailSpriteList;


    //화면에 보여주는거
    public List<Image> WordImageList;

    private Image DetailPopup;

    private List<Button> _wordButtonList;
    private List<RectTransform> _wordRectList;


    private int _clickIndex = 0;
    private int _pageIndex = 0;

    private Vector2 _basePos;
    private Vector2 _baseSize;

    private void Awake()
    {
        _wordRectList = new List<RectTransform>(WordImageList.Count);
        _wordButtonList = new List<Button>(WordImageList.Count);

        foreach (var image in WordImageList) 
        {
            _wordRectList.Add(image.rectTransform);
            _wordButtonList.Add(image.gameObject.GetComponent<Button>());
        }

        _basePos = _wordRectList[0].anchoredPosition;
        _baseSize = _wordRectList[0].sizeDelta;

    }

    private void Start()
    {
        Init();
        DetailPopup = GameManager.Instance.DetailPopup;
    }

    private void Init()
    {
        for(int i = 0; i < _wordButtonList.Count; i++)
        {
            _wordButtonList[i].onClick.AddListener(() => {
                _clickIndex = i;
                ShowDetail();
                });
        }
        SetPage();
    }
    
    private void SetPage()
    {
        for(int i = 0; i < WordImageList.Count; i++)
        {
            int idx = i * _pageIndex + i;
            if(WordSpriteList.Count < idx)
                break;

            WordImageList[i].sprite = WordSpriteList[i * _pageIndex + i];
            _wordRectList[i].sizeDelta = new Vector2(WordImageList[i].sprite.rect.width, WordImageList[i].sprite.rect.height);

            Vector2 gap = _wordRectList[i].sizeDelta - _baseSize;
            _wordRectList[i].anchoredPosition = new Vector2(_basePos.x + gap.x * 0.5f, _wordRectList[i].anchoredPosition.y);

        }
    }

    private void ShowDetail()
    {
        int idx = _clickIndex * _pageIndex + _clickIndex;
        DetailPopup.sprite = DetailSpriteList[idx];
        DetailPopup.gameObject.SetActive(true);
    }

    private int GetIndex() => _clickIndex * _pageIndex + _clickIndex;


}
