using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    Waypoint searchCenter;

    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    bool isRunning = true;

    public List<Waypoint> path = new List<Waypoint>();

    // Start is called before the first frame update
    void Start()
    {
    }

    private void CreatePath()
    {
        SetAsPath(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while(previous != startWaypoint)
        {
            SetAsPath(previous);
            previous = previous.exploredFrom;
        }
        SetAsPath(startWaypoint);
        path.Reverse();
    }
    private void SetAsPath(Waypoint waypoint) 
    {
        path.Add(waypoint);
        waypoint.isPlaceable = false;
    }

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }

    private void CalculatePath()
    {
        LoadBlocks();
        //ColorStartAndEnd();
        Pathfind();
        CreatePath();
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);
        while(queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            //print("Searching from " + searchCenter); //todo remove log
            HaltIfEndFound();
            ExploreNeighbours();
        }
        //print("Finished Pathfinding?");
    }

    private void HaltIfEndFound()
    {
        if(searchCenter == endWaypoint){
            //print("Searching from endnode therefore stopping"); //todo remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }
        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = grid[neighbourCoordinates];
        if (!neighbour.isExplored && !queue.Contains(neighbour))
        {
            //neighbour.SetTopColor(Color.blue);
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
            //print("Qeueing " + neighbour.name);
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        int gridSize = waypoints[0].GetGridSize();
        foreach ( Waypoint waypoint in waypoints)
        {
            //overlapping blocks?
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block" + waypoint);
            }
            else
            {
                //add to dictionary
                grid.Add(gridPos, waypoint);
                //waypoint.SetTopColor(Color.cyan);
            }
        }
        print("Loaded " + grid.Count + " blocks");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
