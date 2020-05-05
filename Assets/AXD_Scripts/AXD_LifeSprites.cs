using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AXD_LifeSprites : MonoBehaviour
{
    bool isFull;
    [SerializeField]
    List<Sprite> sprites;
    Image sourceSprite;


    // Start is called before the first frame update
    void Start()
    {
        sourceSprite = GetComponent<Image>();
        isFull = true;
    }

    // Update is called once per frame
    
    public void ChangeSprite()
    {
        if(sourceSprite.sprite == sprites[0])
        {
            sourceSprite.sprite = sprites[1];
        }
        else
        {
            sourceSprite.sprite = sprites[0];
        }
    }
    public void ChangeSprite(bool full)
    {
        if (full)
        {
            sourceSprite.sprite = sprites[0];
        }else if (!full)
        {
            sourceSprite.sprite = sprites[1];
        }
    }
}
