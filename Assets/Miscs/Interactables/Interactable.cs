

using UnityEngine;

public abstract class Interactable:MonoBehaviour
{
   public abstract void OnInteractWithoutItem(PlayerController playerController);
   public abstract void OnInteract(PlayerController playerController, Item item);
   public abstract bool CanInteract(PlayerController playerController,Item item);
   public abstract bool ShouldPlayAnimation(PlayerController playerController, Item item);

   
}
