using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    [SerializeField]
    private CanvasGroup healGroup;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public override Transform Select()
    {
        healGroup.alpha = 1f;

        return base.Select();
    }

    public override void DeSelect()
    {
        healGroup.alpha = 0f;

        base.DeSelect();
    }

    public void FindEnemy()
    {
    }
}