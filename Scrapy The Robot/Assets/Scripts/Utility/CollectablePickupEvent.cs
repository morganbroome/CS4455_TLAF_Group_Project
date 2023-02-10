using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CollectablePickupEventHandler(GameObject Collectable);

public class CollectablePickupEvent : MonoBehaviour
{
    public static event CollectablePickupEventHandler OnCollectablePickup;

    public static void TriggerCoinPickupEvent(GameObject Collectable)
    {
        OnCollectablePickup?.Invoke(Collectable);
    }
}
