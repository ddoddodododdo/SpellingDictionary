using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonViewer : MonoBehaviour
{
    public List<Image> ButtonImages;
    public GameObject PageObject;

    public List<Sprite> HighlightImages;
    public List<Sprite> UnHighlightImages;

    public void SetButtonHighlight(int typeIndex)
    {
        PageObject.transform.SetAsLastSibling();
        for(int i = 0; i < ButtonImages.Count; i++)
        {
            if(i == typeIndex)
            {
                ButtonImages[i].sprite = HighlightImages[i];
                ButtonImages[i].transform.SetAsLastSibling();
            }
            else
                ButtonImages[i].sprite = UnHighlightImages[i];
            
        }

    }


}
