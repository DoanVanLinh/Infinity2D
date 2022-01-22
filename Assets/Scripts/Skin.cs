using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skin", menuName = "Infinity/Skin", order = 0)]
public class Skin : ScriptableObject
{
    public Texture2D background;
    public Sprite []sprites;
}

