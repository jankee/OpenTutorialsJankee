using UnityEngine;
using System.Collections;

public class StingPatrol : Worker 
{

    private int stingerLength;
    private bool enemyAlert;

    public bool SharpenStinger(int length)
    {
        return false;
    }
    
    public bool LookForEnemies()
    {
        return false;
    }

    public void Sting(string Enemy)
    {

    }

    public override int shiftsLeft
    {
        get
        {
            return base.shiftsLeft;
        }
    }
}
