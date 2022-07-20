using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     private float _speed;
     private Vector3 _startPosition;

    private Trajectory _trajectory;

    public List<Vector3> MousePositions = new List<Vector3>();


    public void Initialize(Trajectory trajectory,Vector3 startPosition, float speed)
    {
        _trajectory = trajectory;
        _startPosition = startPosition;
        _speed = speed;
    }

    private void Update()
    {             
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AddPosition();  
        }
        MovePlayer();
    }

    private void AddPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        MousePositions.Add(position);
        _trajectory.TrajectoryRenderer(MousePositions, this, _startPosition);
    }

    private void MovePlayer()
    {       
       Vector3 position = SearchPosition();
        transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * _speed);
        if (transform.position == position)
            MousePositions.Remove(position);
    }

    private Vector3 SearchPosition()
    {
        Vector3 positionPlayer = transform.position;
        foreach (var mousePosition in MousePositions)
        {
                positionPlayer = mousePosition;
                break;
        }
        return positionPlayer;
    }
}
