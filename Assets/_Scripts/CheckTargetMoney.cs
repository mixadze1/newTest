using UnityEngine;

public class CheckTargetMoney : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Money>())           
        {
            Game._instance.DestroyMoney(collision.gameObject.GetComponent<Money>());
            if (Game._instance.CountMoney.Count <= 0)
            {
                Destroy(gameObject);
            }
            GUIManager._instance.Score += Game._instance.ScoreCoin;
        }
    }
}
