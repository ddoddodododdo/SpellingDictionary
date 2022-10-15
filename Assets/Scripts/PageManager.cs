using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public static PageManager instance;

    public enum WordType
    {
        Spacing,
        Spelling,
        BookMark
    }

    //화면에 보여주는거
    public List<Image> WordImageList;

    public WordType PageType;

    private Image DetailPopup;

    private List<Button> _wordButtonList;
    private List<RectTransform> _wordRectList;

    private List<Sprite> _wordSpriteList;
    private List<Sprite> _descSpriteList;

    private int _clickIndex = 0;
    private int _pageIndex = 0;

    private Vector2 _basePos;
    private Vector2 _baseSize;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;

        PageType = WordType.Spelling;

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
        DetailPopup = GameManager.Instance.DetailPopup;
        Init();
    }

    private void Init()
    {
        foreach(var button in _wordButtonList)
        {
            button.onClick.AddListener(() =>
            {
                _clickIndex = _wordButtonList.IndexOf(button);
                ShowDetail();
            });
        }


        SetPage();
    }
    
    private void SetPage()
    {
        if(PageType == WordType.Spacing)
        {
            _wordSpriteList = DataManager.instance.Word_Spacings;
            _descSpriteList = DataManager.instance.Desc_Spacings;
        }
        else if(PageType == WordType.Spelling)
        {
            _wordSpriteList = DataManager.instance.Word_Spellings;
            _descSpriteList = DataManager.instance.Desc_Spellings;
        }


        if (_wordSpriteList == null || _descSpriteList == null)
            return;


        for(int i = 0; i < WordImageList.Count; i++)
        {
            int idx = i * _pageIndex + i;
            if(_wordSpriteList.Count < idx)
                break;

            WordImageList[i].sprite = _wordSpriteList[i * _pageIndex + i];
            _wordRectList[i].sizeDelta = new Vector2(WordImageList[i].sprite.rect.width, WordImageList[i].sprite.rect.height);

            Vector2 gap = _wordRectList[i].sizeDelta - _baseSize;
            _wordRectList[i].anchoredPosition = new Vector2(_basePos.x + gap.x * 0.5f, _wordRectList[i].anchoredPosition.y);

        }
    }

    private void ShowDetail()
    {
        int idx = _clickIndex * _pageIndex + _clickIndex;
        DetailPopup.sprite = _descSpriteList[idx];
        DetailPopup.gameObject.SetActive(true);
    }

    private int GetIndex() => _clickIndex * _pageIndex + _clickIndex;


    public void PrePage()
    {
        _pageIndex = Mathf.Clamp(--_pageIndex, 0, _wordButtonList.Count);
        SetPage();
    }

    public void NextPage()
    {
        _pageIndex = Mathf.Clamp(++_pageIndex, 0, _wordButtonList.Count);
        SetPage();
    }

}
