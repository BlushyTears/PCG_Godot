using Godot;
using System;
using System.Collections.Generic;


public partial class PCG_Manager : Node2D
{
    [Export] private PackedScene baseTileScene;
    private List<BaseTile> tileset = new List<BaseTile>();
    private int tileSize;
    private const int horizontalTileCount = 18;
    private const int vertocalTileCount = 10;
    public override void _Ready()
    {
        Random random = new Random();
        for(int i = 0; i < horizontalTileCount; i++)
        {
            for(int j = 0; j < vertocalTileCount; j++)
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
        for(int i = 0; i < horizontalTileCount; i++)
        {
            int neighboorCount = 0;
            for(int j = 0; j < vertocalTileCount; j++)
            {
                BaseTile tile = GetTile(new Vector2(i, j));

                // only check for horizontal and vertical neighboors
                for(int i_prime = -1; i_prime <= 1; i_prime++)
                {
                    for(int j_prime = -1; j_prime <= 1; j_prime++)
                    {
                        BaseTile compareTile = GetTile(new Vector2(i, j));

                        if(tile.textureIdx == compareTile.textureIdx)
                        {   
                            neighboorCount++;
                        }
                    }    
                }

                if(neighboorCount > 2)
                {
                    tile.SetTexture(0);                   
                }
            }
        }    
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
