using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownComponent : ItemComponent
{
    private float cooldown;
    private float currentCd;
    public CooldownComponent(float cooldown)
    {
        this.cooldown = cooldown;
        currentCd = 0;
    }

    public override void OnUse(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item,
        LayerMask interactLayer)
    {
        if(!HasCooldown())currentCd = cooldown;
    }

    public override void UpdateWhileSelected(PlayerController playerController, Vector2 mouseInWorld, Direction direction, Item item)
    {
        currentCd -= Time.deltaTime;
    }

    public bool HasCooldown()
    {
        return currentCd > 0;
    }
}
