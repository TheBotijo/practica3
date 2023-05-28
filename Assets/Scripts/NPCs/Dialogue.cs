using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(2,10)]
    public string[] sentences;
    [TextArea(2,10)]
    public string[] badSentences;

}
