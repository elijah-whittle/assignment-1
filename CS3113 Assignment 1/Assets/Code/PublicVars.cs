
public static class PublicVars
{
    public static string[] elements = { "fire", "earth", "water", "wind" };
    public static int currentLevel = 0;
    public static bool paused = false;

    public static bool[] spells = { false, false, false, false };

    public static void ClearSpells()
    {
        spells.SetValue(false, 0, 1, 2, 3);
    }
    
}
