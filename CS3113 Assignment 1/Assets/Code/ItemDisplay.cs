using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    public Color selected;
    public Color deselected;
    public GameObject activeIcon;
    Image activeImage;
    public Sprite active;
    public Sprite locked;
    
    // Start is called before the first frame update
    void Start()
    {
        activeImage = activeIcon.GetComponent<Image>();
        activeImage.sprite = locked;
    }

    public void Select()
    {
        GetComponent<Image>().color = selected;
    }
    public void Deselect()
    {
        GetComponent<Image>().color = deselected;
    }
    public void SetActive()
    {
        activeImage.sprite = active;
    }


}
