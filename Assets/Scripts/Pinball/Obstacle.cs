using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PinballManager manager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ball" && transform.name.Contains("PointBall"))
        {
            manager.UpScore(transform.tag);
        }
        else if (collision.transform.tag == "Ball" && transform.name.Contains("Exit"))
        {
            manager.ResetGame();
        }
    }
}
