using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour 
{
    public float speed;

    private Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public GameObject resetBnt;

    private int score;

    public Text scoreText;

    public Animator gameOverAnim;

    public Text lastTextScore;

    public Text bestTextScore;

    //private static PlayerScript instance;

    //public static PlayerScript Instance
    //{
    //    get 
    //    {
    //        if (instance == null)
    //        {
    //            instance = GameObject.FindObjectOfType<PlayerScript>();
    //        }

    //        return instance; 
    //    }
    //}

	// Use this for initialization
	void Start () 
    {
        isDead = false;

        dir = Vector3.zero;	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            score++;
            scoreText.text = score.ToString();

            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }
        }

        float amountToMove = speed * Time.deltaTime;

        transform.Translate(dir * amountToMove);
	}

    public void SpeedAdd()
    {
        print("speedUp");
    }

    private void FixedUpdate()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.SetActive(false);

            Instantiate(ps, other.transform.position, Quaternion.identity);

            score += 3;
            scoreText.text = score.ToString();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tile")
        {
            RaycastHit hit;

            Ray downRay = new Ray(this.transform.position, -Vector3.up);

            if (!Physics.Raycast(downRay, out hit))
            {
                isDead = true;

                

                if (transform.childCount > 0)
                {
                    transform.GetChild(0).transform.parent = null;    
                }

                StartCoroutine("ResetGame");
            }
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(0);

        lastTextScore.text = scoreText.text;

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        bestTextScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        gameOverAnim.SetTrigger("GameOver");

        resetBnt.SetActive(true);


    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -Vector3.up);
    }





    public void TimePause()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;    
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

}
