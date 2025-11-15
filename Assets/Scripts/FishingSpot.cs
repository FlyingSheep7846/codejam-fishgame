using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(true);
            UIOverlays.INSTANCE.ToggleFishIndicator(true);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(false);
            UIOverlays.INSTANCE.ToggleFishIndicator(false);
        }
    }

}
