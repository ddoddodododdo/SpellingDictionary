using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("�ܾ� �̹���")]
    public List<Sprite> Word_Spacings;
    public List<Sprite> Word_Spellings;

    [Header("���� �̹���")]
    public List<Sprite> Desc_Spacings;
    public List<Sprite> Desc_Spellings;

    

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }


}
