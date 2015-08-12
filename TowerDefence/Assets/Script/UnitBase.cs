using UnityEngine;
using System.Collections;
using common;

public class UnitBase : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public tk2dSpriteAnimator spr;

    private float timeCheck = 1;
    private bool isMoveAble = false;
    private Point nextPoint;
    //현재 유닛이 속해있는 타일맵 좌표
    private Point startPoint;
    //유닛이 다음가야할 타일맵 좌표
    private int pathIndex = 0;
    //유닛이 지나갈 경로, 차후 pathfinder를 통해 path경로를 미리 받는다, 맵이 변경될때 마다 새로 갱신한다
    private Point[] pathArr;

    //최초 유닛의 startPoint를 정해주고, 움직일 경로를 생성한다.
    public void setStartPoint(Point p)
    {
        startPoint = p;
        getPath();
        nextPoint = pathArr[pathIndex];
        showCharDir();
        isMoveAble = true;
    }

    private enum CHAR_ANI
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        DESTROY,
    };

    private string[] charAniStr = new string[] { "walk_up", "walk_down", "walk_left", "walk_right", "destroy" };

    void Awake()
    {
        nextPoint = new Point(0, 0);
        //spr = this.GetComponent<tk2dSpriteAnimator>();
    }

    void Update()
    {
            
        if (!isMoveAble)
        {
            return;
        }

        float _speed = GameManager.instance.cellSize * Time.deltaTime * moveSpeed;

        int tx = nextPoint.x - startPoint.x;
        int ty = nextPoint.y - startPoint.y;

        float dx = _speed * tx;
        float dy = _speed * ty;
        float rx = (nextPoint.x * GameManager.instance.cellSize + GameManager.instance.cellSize / 2.0f) - this.transform.localPosition.x;
        float ry = (-nextPoint.y * GameManager.instance.cellSize - GameManager.instance.cellSize / 2.0f) - this.transform.localPosition.y;

        bool isCloseX = false;
        bool isCloseY = false;

       
        if (Mathf.Abs(dx) > Mathf.Abs(rx) || dx == 0)
        {
            dx = rx;
            isCloseX = true;
        }

        if (Mathf.Abs(dy) > Mathf.Abs(ry) || dy == 0)
        {
            dy = ry;
            isCloseY = true;
        }

        this.transform.localPosition += new Vector3(dx, dy, 0);

        if (isCloseX && isCloseY)
        {
            isMoveAble = false;
            GameManager.instance.removeUnit(this);
            //Destroy(this.gameObject);
            return;
        }
        //setNextPoint();
        StopCoroutine("setNextPoint");
        StartCoroutine("setNextPoint");
    }

    int makeNomal(float f)
    {
        float k = .1f;
        if (f > k)
        {
            return 1;
        }
        else if (f < -k)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public bool getPath()
    {
        float cellSize = GameManager.instance.cellSize;
        startPoint = new Point((int)(this.transform.localPosition.x / cellSize), -(int)(this.transform.localPosition.y / cellSize));
        int wallMapIndex = GameManager.instance.wallMap[startPoint.x, startPoint.y];
        if (wallMapIndex > 0 && wallMapIndex < 10)
        {
            return true;
        }

        Point[] pArr = PathFinder.instance.getPath(startPoint, 111);

        if (pArr == null)
        {
            Debug.Log("NULL path");
            return false;
        }

        pathArr = pArr;

        if (nextPoint != null && pathArr.Length > 1 && nextPoint.isEqual(pathArr[1]))
        {
            pathIndex = 1;
        }
        else
        {
            pathIndex = 0;
        }

        showCharDir();
        return true;

    }
    IEnumerator setNextPoint()
    {
        startPoint = nextPoint;
        pathIndex++;

        nextPoint = pathArr[pathIndex];
        showCharDir();
        yield return new WaitForSeconds(1.0f);
    }

    //private void setNextPoint()
    //{
    //    startPoint = nextPoint;
    //    pathIndex++;

    //    nextPoint = pathArr[pathIndex];
    //    showCharDir();
    //}

    private void showCharDir()
    {
        float cellSize = GameManager.instance.cellSize;
        //float nx = nextPoint.x;
        float nx = (nextPoint.x * cellSize + cellSize / 2.0f);
        float ny = (-nextPoint.y * cellSize - cellSize / 2.0f);

        if (this.transform.localPosition.x < nx)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.RIGHT]);
        }
        else if (this.transform.localPosition.x > nx)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.LEFT]);
        }
        else if (this.transform.localPosition.y < ny)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.UP]);
        }
        else if (this.transform.localPosition.y > ny)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.DOWN]);
        }

        spr.ClipFps *= moveSpeed;
    }
}
