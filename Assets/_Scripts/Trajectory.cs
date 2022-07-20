using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
   [SerializeField] public List<Trajectory> Trajectorys = new List<Trajectory>();

    private LineRenderer _render;

    private Vector3 _rememberVector;

    private int _startPosition = 0;
    private int _endPosition = 1;

    private void Awake()
    {
        _render = GetComponent<LineRenderer>();
    }


    public void TrajectoryRenderer(List<Vector3> mousePositions, Player player, Vector3 startPosition)
    {
        
        Trajectory trajectory = Instantiate(this);
        Trajectorys.Add(trajectory);
        foreach (var pos in mousePositions)
        {
            if (CheckStartPosition(player, startPosition))
            {
                trajectory.GetComponent<LineRenderer>().SetPosition(_startPosition, pos);
                trajectory.GetComponent<LineRenderer>().SetPosition(_endPosition, startPosition);
                _rememberVector = pos;
            }

            else 
            {
                trajectory.GetComponent<LineRenderer>().SetPosition(_startPosition, pos);
                trajectory.GetComponent<LineRenderer>().SetPosition(_endPosition, _rememberVector);

                _rememberVector = pos;
            }
            
        }       
        trajectory = _render.GetComponent<Trajectory>();
    }

    private bool CheckStartPosition(Player player, Vector3 startPosition)
    {
        if (player.gameObject.transform.position == startPosition)
        {
            return true;
        }
        return false;
    }
}
