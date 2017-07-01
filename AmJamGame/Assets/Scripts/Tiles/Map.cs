using System;

[Serializable]
public class Map
{
    public int width;
    public int height;
    public Layer[] layers;
    public Tiles[] tilesets;
}

