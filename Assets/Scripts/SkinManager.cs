using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] GameObject []pieces;
    [SerializeField] Material background;
    [SerializeField] Skin []skins;
    [SerializeField] int currentIndexSkin;
    private void Awake() {
        Skin currentSkin = skins[currentIndexSkin];
        for(int i = 0;i<5;i++)
        {
            pieces[i].GetComponent<SpriteRenderer>().sprite = currentSkin.sprites[i];
        }
        background.mainTexture = currentSkin.background;
    }
}
