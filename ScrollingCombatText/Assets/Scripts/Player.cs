using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{

    public float speed;
    
    private bool checkUp;

	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(horizontal, vertical) * (speed * Time.deltaTime));
	}


    public void OnTriggerStay2D(Collider2D collision)
    {
        
        int random = Random.Range(0, 2);

        if (!checkUp)
        {
            if (collision.transform.name == "Bonefire")
            {
         
                StartCoroutine("CheckTime");

                if (random == 0)
                {
                    int ranDag = Random.Range(1, 10);

                    CombatTextManager.Instance.CreateText(transform.position, "-" + ranDag.ToString(), Color.red, false);
                }
                else
                {
                    int ranDag = Random.Range(11, 20);

                    CombatTextManager.Instance.CreateText(transform.position, "-" + ranDag.ToString(), Color.red, true);
                }

            }
            else if (collision.transform.name == "Heart")
            {
                StartCoroutine("CheckTime");

                if (random == 0)
                {
                    int ranDag = Random.Range(1, 10);

                    CombatTextManager.Instance.CreateText(transform.position, "+" + ranDag.ToString(), Color.green, false);
                }
                else
                {
                    int ranDag = Random.Range(11, 20);

                    CombatTextManager.Instance.CreateText(transform.position, "+" + ranDag.ToString(), Color.green, true);
                }
            }
        }
        
    }

    private IEnumerator CheckTime()
    {
        checkUp = true;

        yield return new WaitForSeconds(2);

        checkUp = false;
    }
}
