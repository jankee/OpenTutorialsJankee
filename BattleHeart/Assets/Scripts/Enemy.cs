using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healthCanvas;

    private Transform target;

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    protected override void Update()
    {
        FollowTarget();

        base.Update();
    }

    public override Transform Select()
    {
        healthCanvas.alpha = 1f;

        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

        return base.Select();
    }

    public override void Deselect()
    {
        if (healthCanvas != null)
        {
            healthCanvas.alpha = 0;

            this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
        base.Deselect();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        OnHealthChanged(health.MyCurrentValue);
    }

    private void FollowTarget()
    {
        if (target != null)
        {
            direction = (target.transform.position - this.transform.position).normalized;

            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            direction = Vector3.zero;
        }
    }
}
