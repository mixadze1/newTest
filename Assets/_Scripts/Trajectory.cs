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
            if (CheckIsStartPosition(player, startPosition))
            {
                _rememberVector = SetLineRenderer(trajectory, pos, startPosition);
            }
            else 
            {
                _rememberVector = SetLineRenderer(trajectory, pos, _rememberVector);
            }           
        }       
        trajectory = _render.GetComponent<Trajectory>();
    }

    private bool CheckIsStartPosition(Player player, Vector3 startPosition)
    {
        if (player.gameObject.transform.position == startPosition)
        {
            return true;
        }
        return false;
    }

    private Vector3 SetLineRenderer(Trajectory trajectory, Vector3 oldPosition, Vector3 newPosition)
    {
        trajectory.GetComponent<LineRenderer>().SetPosition(_startPosition, oldPosition);
        trajectory.GetComponent<LineRenderer>().SetPosition(_endPosition, newPosition);
        return oldPosition;
    }
}
