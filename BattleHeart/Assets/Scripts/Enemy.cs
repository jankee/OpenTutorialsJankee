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
    protected override void Update()
    {
        Collider[] coll = Physics.OverlapSphere(transform.position, 3f, 256);

        if (coll.Length > 0)
        {
            print("Coll");
        }

        base.Update();
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

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger");

        if (other.tag == "Player")
        {
            print(other.tag);
        }
    }
}