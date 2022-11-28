using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public static PageManager instance;

    public enum WordType
    {
        BookMark,
        Spelling,
        Spacing
    }

    //화면에 보여주는거
    public List<Image> WordImageList;

    public WordType PageType;
    public Toggle BookMarkToggle;
    public ButtonViewer ButtonViewer;

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
        DetailPopup = GameManager.Instance.DetailPopup.DetailImage;
        Init();
    }

    private void Init()
    {
        var detailPopup = GameManager.Instance.DetailPopup;
        foreach(var button in _wordButtonList)
        {
            button.onClick.AddListener(() =>
            {
                _clickIndex = _wordButtonList.IndexOf(button);
                detailPopup.CloseButton.onClick.AddListener(() => SetPage());
                ShowDetail();
                SetBookMarkToggle();
            });
        }


        WordTypeButton(1);
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
        else if (PageType == WordType.BookMark)
        {
            DataManager.instance.SetBookMarkData();
            _wordSpriteList = DataManager.instance.Word_BookMark;
            _descSpriteList = DataManager.instance.Desc_BookMark;
        }
     
        if (_wordSpriteList == null || _descSpriteList == null)
            return;

        _pageIndex = Mathf.Clamp(_pageIndex, 0, _wordSpriteList.Count / WordImageList.Count);

        for (int i = 0; i < WordImageList.Count; i++)
        {
            int idx = i + _pageIndex * WordImageList.Count;
            if (_wordSpriteList.Count <= idx)
            {
                WordImageList[i].color = Color.clear;
                continue;
            }

            WordImageList[i].color = Color.white;
            WordImageList[i].sprite = _wordSpriteList[idx];
            _wordRectList[i].sizeDelta = new Vector2(WordImageList[i].sprite.rect.width, WordImageList[i].sprite.rect.height);

            Vector2 gap = _wordRectList[i].sizeDelta - _baseSize;
            _wordRectList[i].anchoredPosition = new Vector2(_basePos.x + gap.x * 0.5f, _wordRectList[i].anchoredPosition.y);

        }
    }

    private void ShowDetail()
    {
        int idx = GetClickIndex();
        if (_descSpriteList.Count <= idx)
            return;

        DetailPopup.sprite = _descSpriteList[idx];
        DetailPopup.gameObject.SetActive(true);
    }

    public void SetBookMarkToggle()
    {
        string tempKey = GetBookMarkKey();
        bool hasKey = PlayerPrefs.HasKey(GetBookMarkKey());
        BookMarkToggle.isOn = hasKey;
    }

    private int GetClickIndex() => WordImageList.Count * _pageIndex + _clickIndex;
    private string GetBookMarkKey()
    {
        if(PageType == WordType.BookMark)
        {
            if (DataManager.instance.Desc_Spellings.Contains(DetailPopup.sprite))
                return WordType.Spelling.ToString() + DataManager.instance.Desc_Spellings.IndexOf(DetailPopup.sprite);
            else
                return WordType.Spacing.ToString() + DataManager.instance.Desc_Spacings.IndexOf(DetailPopup.sprite);
        }
        else 
            return PageType.ToString() + GetClickIndex();
    }



    /// <summary>
    /// Buttons
    /// </summary>

    #region 버튼 함수
    public void WordTypeButton(int type)
    {
        PageType = (WordType)type;
        _pageIndex = 0;
        SetPage();
        ButtonViewer.SetButtonHighlight(type);
    }

    public void PrePage()
    {
        SetBookMarkToggle();
        _pageIndex--;
        SetPage();
    }

    public void NextPage()
    {
        _pageIndex++;
        SetPage();
    }

    public void ChangedBookMarkData()
    {
        if (BookMarkToggle.isOn)
        {
            PlayerPrefs.SetInt(GetBookMarkKey(), 0);
        }
        else if (PlayerPrefs.HasKey(GetBookMarkKey()))
        {
            PlayerPrefs.DeleteKey(GetBookMarkKey());
        }
    }

    public void ResetBookMarkData()
    {
        PlayerPrefs.DeleteAll();
        SetPage();
    }
    #endregion
}
