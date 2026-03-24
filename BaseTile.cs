using Godot;
using System;
using System.Collections.Generic;

public partial class BaseTile : TextureRect
{
   [Export] private Godot.Collections.Array<Texture2D> textures = new Godot.Collections.Array<Texture2D>();
    private Vector2 tilesize;
    public int textureIdx = 0;

    public void SetTexture(int idx)
    {
        ZIndex = 0;
        Texture = textures[idx];
        tilesize = Texture.GetSize();
        textureIdx = idx;
    }

    public void SetPosition(Vector2 pos)
    {
        Position = new Vector2(pos.X * tilesize.X, pos.Y * tilesize.Y);
    }
}
