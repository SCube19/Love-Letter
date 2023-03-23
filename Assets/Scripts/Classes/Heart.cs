using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private int _index;
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            LetterFragment = "something with index";
        }
    }

    public string LetterFragment { get; private set; }
    public Texture2D HeartTexture 
    {
        get { return Resources.Load<Texture2D>($"SpriteSheets/heart_pieces_{_index}"); }
    }
}
