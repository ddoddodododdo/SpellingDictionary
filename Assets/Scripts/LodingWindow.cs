using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class LodingWindow : MonoBehaviour, IPointerClickHandler
{
    public Image StartTextImage;

    private bool _isLate = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isLate)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        _isLate = false;
        yield return new WaitForSeconds(1f);
        _isLate = true;

        while (true)
        {
            StartTextImage.gameObject.SetActive(!StartTextImage.gameObject.activeSelf);

            yield return new WaitForSeconds(0.5f);

        }
    }

}
