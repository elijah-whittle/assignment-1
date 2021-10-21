using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : MonoBehaviour
{
    public enum Element
    {
        Fire, Earth, Water, Wind
    }
    public Element spell;

    public void AddSpell()
    {
        PublicVars.spells[(int)spell] = true;
    }
}
