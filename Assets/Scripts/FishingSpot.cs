using UnityEngine;

public class FishingSpot : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float rotation;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(true, rotation);
            UIOverlays.INSTANCE.ToggleFishIndicator(true);
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            FishingManager.INSTANCE.CanFish(false, rotation);
            UIOverlays.INSTANCE.ToggleFishIndicator(false);
        }
    }

}
