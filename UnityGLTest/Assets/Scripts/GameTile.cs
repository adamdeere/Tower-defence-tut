using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform arrow;
    [FormerlySerializedAs("_north")] public GameTile north;
    [FormerlySerializedAs("_east")] public GameTile east;
    [FormerlySerializedAs("_south")] public GameTile south;
    [FormerlySerializedAs("_west")] public GameTile west;
    public GameTile nextOnPath;
    
    public bool IsAlternative { get; set; }
    
    static readonly Quaternion
        northRotation = Quaternion.Euler(90f, 0f, 0f);

    static readonly Quaternion
        eastRotation = Quaternion.Euler(90f, 90f, 0f);

    static readonly Quaternion
        southRotation = Quaternion.Euler(90f, 180f, 0f);

    static readonly Quaternion
        westRotation = Quaternion.Euler(90f, 270f, 0f);

    public bool HasPath => distance != int.MaxValue;
    private int distance;
    public static void MakeEastWestNeighbors (GameTile east, GameTile west) 
    {
        west.east = east;
        east.west = west;
    }
    public static void MakeNorthSouthNeighbors (GameTile north, GameTile south) {
        Debug.Assert(south.north == null && north.south == null, "Redefined neighbors!");
        south.north = north;
        north.south = south;
    }

    public void ClearPath()
    {
        distance = int.MaxValue;
        nextOnPath = null;
    }

    public void BecomeDestination()
    {
        distance = 0;
        nextOnPath = null;
    }
    private GameTile GrowPathTo (GameTile neighbor) 
    {
        Debug.Assert(HasPath, "No path!");
        if (neighbor == null || neighbor.HasPath) 
        {
            return null;
        }
        neighbor.distance = distance + 1;
        neighbor.nextOnPath = this;

        return neighbor;
    }
    
    public void ShowPath () 
    {
        if (distance == 0) 
        {
            arrow.gameObject.SetActive(false);
            return;
        }
        arrow.gameObject.SetActive(true);
        arrow.localRotation =
            nextOnPath == north ? northRotation :
            nextOnPath == east ? eastRotation :
            nextOnPath == south ? southRotation :
            westRotation;
    }
    public GameTile GrowPathNorth () => GrowPathTo(north);

    public GameTile GrowPathEast () => GrowPathTo(east);

    public GameTile GrowPathSouth () => GrowPathTo(south);

    public GameTile GrowPathWest () => GrowPathTo(west);
}
