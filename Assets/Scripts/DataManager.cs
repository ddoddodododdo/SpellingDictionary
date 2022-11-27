using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("단어 이미지")]
    public List<Sprite> Word_Spacings;
    public List<Sprite> Word_Spellings;
    public List<Sprite> Word_BookMark;

    [Header("설명 이미지")]
    public List<Sprite> Desc_Spacings;
    public List<Sprite> Desc_Spellings;
    public List<Sprite> Desc_BookMark;



    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    public void SetBookMarkData()
    {
        Word_BookMark.Clear();
        Desc_BookMark.Clear();

        for (int i = 0; i < Word_Spellings.Count; i++)
        {
            if (PlayerPrefs.HasKey($"Spelling{i}"))
            {
                Word_BookMark.Add(Word_Spellings[i]);
                Desc_BookMark.Add(Desc_Spellings[i]);
            }
        }

        for (int i = 0; i < Word_Spacings.Count; i++)
        {
            if (PlayerPrefs.HasKey($"Spacing{i}"))
            {
                Word_BookMark.Add(Word_Spacings[i]);
                Desc_BookMark.Add(Desc_Spacings[i]);
            }
        }
    }

}
