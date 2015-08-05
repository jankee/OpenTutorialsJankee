using UnityEngine;
using System.Collections;
using common;

public class UnitBase : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public tk2dSpriteAnimator spr;

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

    void Start()
    {
        spr = this.GetComponent<tk2dSpriteAnimator>();
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
            return;
        }
        setNextPoint();
    }

    public void getPath()
    {
        pathArr = new Point[] { new Point(startPoint.x + 1, startPoint.y), new Point(startPoint.x + 2, startPoint.y), 
            new Point(startPoint.x + 2, startPoint.y + 1), new Point(startPoint.x + 2, startPoint.y) };
        pathIndex = 0;
    }

    private void setNextPoint()
    {
        startPoint = nextPoint;
        pathIndex++;
        if (pathIndex > 3)
        {
            pathIndex = 0;
        }
        nextPoint = pathArr[pathIndex];
        showCharDir();
    }

    private void showCharDir()
    {
        if (startPoint.x < nextPoint.x)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.RIGHT]);
        }
        else if (startPoint.x > nextPoint.x)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.LEFT]);
        }
        else if (startPoint.y > nextPoint.y)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.UP]);
        }
        else if (startPoint.y < nextPoint.y)
        {
            spr.Play(charAniStr[(int)CHAR_ANI.DOWN]);
        }

        spr.ClipFps *= moveSpeed;
    }
}
