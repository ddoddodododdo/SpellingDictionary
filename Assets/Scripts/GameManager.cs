using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GroupDocs.Conversion;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //���� ���� ��ġ �о�ͼ� ���� ���ּ���
        var converter = new Converter("sample.xlsx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
