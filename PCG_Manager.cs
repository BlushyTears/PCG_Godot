using Godot;
using System;
using System.Collections.Generic;


public partial class PCG_Manager : Node2D
{
    [Export] private PackedScene baseTileScene;
    private List<BaseTile> tileset = new List<BaseTile>();
    private int tileSize;
    public override void _Ready()
    {
        createTile(0, new Vector2(0, 0));
        createTile(0, new Vector2(1, 0));
        createTile(0, new Vector2(0, 1));
        createTile(1, new Vector2(1, 1));

        var v = GetTile(new Vector2(0, 0));
        GD.Print("v after: ", v);
    }

    public BaseTile GetTile(Vector2 pos)
    {
        int idx = (int)pos.Y * tileSize + (int)pos.X;    
        return tileset[idx];
    }

    private void createTile(int textureIdx, Vector2 pos)
    {
        BaseTile tile = baseTileScene.Instantiate<BaseTile>();

        AddChild(tile);

        tile.SetTexture(textureIdx);
        tile.SetPosition(pos);

        tileset.Add(tile);

        GD.Print("v before: ", tile);
    }
}
