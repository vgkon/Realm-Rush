using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towers;
    // Start is called before the first frame update
    void Start()
    {
        towers = new Queue<Tower>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTower(Waypoint baseWaypoint)
    {
        if (towers.Count < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        Debug.Log("can't place another tower ");
        var towerToMove = towers.Dequeue();
        towerToMove.baseWaypoint.isPlaceable = true;
        towerToMove.baseWaypoint = baseWaypoint;
        towerToMove.transform.position = baseWaypoint.transform.position;
        towers.Enqueue(towerToMove);
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.baseWaypoint = baseWaypoint;
        towers.Enqueue(newTower);
        newTower.transform.parent = GameObject.Find("Towers").transform;
        baseWaypoint.isPlaceable = false;
    }
}
