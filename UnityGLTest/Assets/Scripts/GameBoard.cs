using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform ground;
    [SerializeField] private GameTile tilePrefab;
     private Vector2Int _size;
     private GameTile[] _tiles;
     private Queue<GameTile> searchFrontier = new Queue<GameTile>();
    public void Initialize(Vector2Int inSize)
    {
        _tiles = new GameTile[inSize.x * inSize.y];
        _size = inSize;
        ground.localScale = new Vector3(_size.x, _size.y, 1f);
    }

    public void InitializeTiles()
    {
        var offset = new Vector2((_size.x - 1) * 0.5f, (_size.y - 1) * 0.5f);
        for (int i = 0,  y = 0; y < _size.y; y++) 
        {
            for (var x = 0; x < _size.x; x++, i++) 
            {
                var tile = _tiles[i] = Instantiate(tilePrefab, transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
                
                if (x > 0) 
                    GameTile.MakeEastWestNeighbors(tile, _tiles[i - 1]);
                
                if (y > 0) 
                    GameTile.MakeNorthSouthNeighbors(tile, _tiles[i - _size.x]);
                
                tile.IsAlternative = (x & 1) == 0;

            }
        }
    }

    public void FindPaths () 
    {
        foreach (GameTile tiles in _tiles) 
        {
            tiles.ClearPath();
        }
        _tiles[_tiles.Length / 2].BecomeDestination();
        searchFrontier.Enqueue(_tiles[_tiles.Length / 2]);
        while (searchFrontier.Count > 0)
        {
            GameTile tile = searchFrontier.Dequeue();
            if (tile != null)
            {
                searchFrontier.Enqueue(tile.GrowPathNorth());
                searchFrontier.Enqueue(tile.GrowPathSouth());
                searchFrontier.Enqueue(tile.GrowPathEast());
                searchFrontier.Enqueue(tile.GrowPathWest());
            }
        }
        
        foreach (GameTile tiles in _tiles) 
        {
            tiles.ShowPath();
        }
    }
    
    
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
