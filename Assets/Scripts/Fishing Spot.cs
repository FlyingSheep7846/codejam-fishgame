using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    public bool fishing = false;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            fishing = true;
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            fishing = false;
        }
    }
}
