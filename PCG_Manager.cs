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
        Random random = new Random();
        for(int i = 0; i < 18; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                int rnd = random.Next(0, 2);
                createTile(rnd, new Vector2(i, j));
            }
        }
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
    }

    void Generate()
    {
        GD.Print("Generate btn");        
    }

    void Clear()
    {
        GD.Print("Clear btn");        
    }

    void ApplyAlgo()
    {
        GD.Print("Algo btn");        
    }
}
