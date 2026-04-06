using Godot;
using System;
using System.Collections.Generic;

public partial class PCG_Manager : Node2D
{
    [Export] private PackedScene baseTileScene;
    private List<BaseTile> tileset = new List<BaseTile>();
    private const int horizontalTileCount = 70;
    private const int verticalTilecount = 110;
    private int _liveNeighboursRequired = 4;

    public BaseTile GetTile(Vector2 pos)
    {
        int idx = (int)pos.Y * horizontalTileCount + (int)pos.X;    
        return tileset[idx];
    }

    private void createTile(int textureIdx, Vector2 pos)
    {
        BaseTile tile = baseTileScene.Instantiate<BaseTile>();
        AddChild(tile);
        tile.SetTexture(textureIdx);
        tile.SetPosition(new Vector2(pos.X / 5.8f - 0.4f, pos.Y / 5.8f - 0.4f));
        tileset.Add(tile);
    }

    // Copied (And converted to 1d array) from: https://bronsonzgeb.com/index.php/2022/01/30/procedural-generation-with-cellular-automata/
    int GetNeighbourCellCount(int x, int y)
    {
        int neighbourCellCount = 0;

        if (x > 0)
        {
            neighbourCellCount += tileset[y * horizontalTileCount + (x - 1)].textureIdx;

            if (y > 0)
            {
                neighbourCellCount += tileset[(y - 1) * horizontalTileCount + (x - 1)].textureIdx;
            }
        }

        if (y > 0)
        {
            neighbourCellCount += tileset[(y - 1) * horizontalTileCount + x].textureIdx;
            if (x < horizontalTileCount - 1)
            {
                neighbourCellCount += tileset[(y - 1) * horizontalTileCount + (x + 1)].textureIdx;
            }
        }

        if (x < horizontalTileCount - 1)
        {
            neighbourCellCount += tileset[y * horizontalTileCount + (x + 1)].textureIdx;
            if (y < verticalTilecount - 1)
            {
                neighbourCellCount += tileset[(y + 1) * horizontalTileCount + (x + 1)].textureIdx;
            }
        }

        if (y < verticalTilecount - 1)
        {
            neighbourCellCount += tileset[(y + 1) * horizontalTileCount + x].textureIdx;
            if (x > 0)
            {
                neighbourCellCount += tileset[(y + 1) * horizontalTileCount + (x - 1)].textureIdx;
            }
        }

        return neighbourCellCount;
    }

    void Generate()
    {
        Clear();
        Random random = new Random();
        for(int i = 0; i < verticalTilecount; i++)
        {
            for(int j = 0; j < horizontalTileCount; j++)
            {
                int rnd = random.Next(0, 2);
                createTile(rnd, new Vector2(i, j));
            }
        } 
    }

    void Clear()
    {
        if(tileset.Count <= 0)
            return;
        for(int i = 0; i < verticalTilecount; i++)
        {
            for(int j = 0; j < horizontalTileCount; j++)
            {
                tileset[j * horizontalTileCount + i].QueueFree();           
            }
        }    
        tileset.Clear();
    }

    void ApplyAlgo()
    {
        int[] nextStates = new int[tileset.Count];

        for(int i = 0; i < horizontalTileCount; i++)
        {
            for(int j = 0; j < verticalTilecount; j++)
            {
                BaseTile tile = GetTile(new Vector2(i, j));
                int neighboors = GetNeighbourCellCount(i, j);
                nextStates[j * horizontalTileCount + i] = (neighboors >= _liveNeighboursRequired) ? 1 : 0;
            }
        }    

        for(int i = 0; i < tileset.Count; i++)
        {
            tileset[i].textureIdx = nextStates[i];
            tileset[i].SetTexture(nextStates[i]);          
        }
    }
}
