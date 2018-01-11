using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthCanvas;

    public override Transform Select()
    {
        healthCanvas.alpha = 1f;

        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        return base.Select();
    }

    public override void Deselect()
    {
        healthCanvas.alpha = 0;

        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

        base.Deselect();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (health.MyCurrentValue <= 0)
        {
            //this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

            myAnimator.SetTrigger("Die");
        }

        OnHealthChanged(health.MyCurrentValue);
    }
}
