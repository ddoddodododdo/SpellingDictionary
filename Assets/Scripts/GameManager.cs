using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GroupDocs.Conversion;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //엑셀 파일 위치 읽어와서 들어가게 해주세요
        var converter = new Converter("sample.xlsx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
