using UnityEngine;

public static class UserUtils
{
    private static readonly byte _alphaCanal = 255;

    public static Color32 GetRandomColor()
    {
        byte redPalleteCount = System.Convert.ToByte(Random.Range(0, 256));
        byte greenPalleteCount = System.Convert.ToByte(Random.Range(0, 256));
        byte bluePalleteCount = System.Convert.ToByte(Random.Range(0, 256));

        return new Color32(redPalleteCount, greenPalleteCount, bluePalleteCount, _alphaCanal);
    }
}
