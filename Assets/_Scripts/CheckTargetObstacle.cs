using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTargetObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Obstacles>())
        {
            Destroy(gameObject);
            Game._instance.IsLose = true;
        }
    }
}
