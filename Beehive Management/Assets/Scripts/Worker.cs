using UnityEngine;
using System.Collections;

public class Worker : MonoBehaviour 
{
    private string currentjob = "";
    public string Currentjob
    {
        get { return currentjob; }
    }

    private string[] jobsICanDo;

    private int shiftsToWork;
    private int shiftsWorked;

    //ShiftsLeft는 읽기 전용으로 지금하고 있는 일을 몇시간 단위를 계산해서 알려준다.
    public int ShiftsLeft
    {
        get
        {
            return shiftsToWork - shiftsWorked;
        }
    }

    public Worker(string[] jobsICanDo)
    {
        this.jobsICanDo = jobsICanDo;

        //확인 작업
        string checkJob = "";

        for (int i = 0; i < this.jobsICanDo.Length; i++)
        {
            checkJob += jobsICanDo[i] + ", ";            
        }
        print("hi  : " + checkJob);
    }

	// Use this for initialization
	public bool DoThisJob (string job, int numberOfShifts) 
    {
        if (!string.IsNullOrEmpty(currentjob))
        {
            return false;   
        }

        for (int i = 0; i < jobsICanDo.Length; i++)
        {
            if (jobsICanDo[i] == job)
            {
                currentjob = job;
                this.shiftsToWork = numberOfShifts;
                shiftsWorked = 0;
                return true;
            }
        }
        return false;
	}
    
    /// <summary>
    /// 여왕벌은 일벌의 WorkOneShift() 메소드를 호출해서 다음 시간단위에 할일을 처리하라는 지시를 내린다
    /// 마지막 시간 단위 분량인 경우에만 true를 반환한다
    /// </summary>
    /// <returns></returns>
	public bool WorkOneShift()
    {
        if (string.IsNullOrEmpty(currentjob))
        {
            return false;
        }
        shiftsWorked++;

        if (shiftsWorked > shiftsToWork)
        {
            shiftsWorked = 0;
            shiftsToWork = 0;
            currentjob = "";
            return true;
        }
        else
        {
            return false;
        }
    }
}
