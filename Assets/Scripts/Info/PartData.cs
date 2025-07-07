using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartData", menuName = "Motor/PartData")]
public class PartData : ScriptableObject
{
    public string partName;
    [TextArea] public string description;
    [TextArea] public string[] possibleFailures;
    public Sprite partSprite;
}