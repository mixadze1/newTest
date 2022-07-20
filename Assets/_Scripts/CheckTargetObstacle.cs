using UnityEngine;

public class CheckTargetObstacle : MonoBehaviour
{
    [SerializeField] private Transform _explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Obstacles>())
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Game._instance.IsLose = true;
        }
    }
}
