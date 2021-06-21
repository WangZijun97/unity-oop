using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NimbleDog : Enemy
{
    protected override void Awake()
    {
        maxHealth = 6;
        initialAttackStrength = 2;
        base.Awake();
    }

    public override int GetHit(int damage)
    {
        int nimbleDmg = damage;
        if (Random.Range(0f, 1f) > 0.6f)
        {
            string infoString = $"{name} dodged some damage!";
            Debug.Log(infoString);
            gameManager.DisplayAdditionalActionText(infoString);

            nimbleDmg -= 3;
        }
        nimbleDmg = nimbleDmg < 0 ? 0 : nimbleDmg;
        return base.GetHit(nimbleDmg);
    }
}
