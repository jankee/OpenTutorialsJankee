using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChanged(float health);

public delegate void CharacterRemove();

public class NPC : Character
{
    public event HealthChanged healthChanged;

    public event CharacterRemove characterRemoved;

    [SerializeField]
    private Sprite portrait;

    public Sprite MyPortrait
    {
        get
        {
            return portrait;
        }
    }

    public virtual void Deselect()
    {
        healthChanged -= new HealthChanged(UIManager.MyInstance.UpdateTatgetFrame);
        characterRemoved -= new CharacterRemove(UIManager.MyInstance.HideTargetFrame);
    }

    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
    }

    public void OnCharacterRemoved()
    {
        if (characterRemoved != null)
        {
            characterRemoved();
        }

        Destroy(gameObject);
    }
}
