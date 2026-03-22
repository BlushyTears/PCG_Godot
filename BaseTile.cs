using Godot;
using System;
using System.Collections.Generic;

public partial class BaseTile : TextureRect
{
   [Export] private Godot.Collections.Array<Texture2D> textures = new Godot.Collections.Array<Texture2D>();
    
    private Vector2 tilesize;

    public void SetTexture(int idx)
    {
        Texture = textures[idx];
        tilesize = Texture.GetSize();
        GD.Print($"Tile size: {tilesize}");
    }

    public void SetPosition(Vector2 pos)
    {
        Position = new Vector2(pos.X * tilesize.X, pos.Y * tilesize.Y);
    }
}
