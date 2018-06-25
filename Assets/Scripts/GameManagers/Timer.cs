using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public int m_Timer = 0;
    public int m_seconds = 0;
    public int m_minutes = 0;
    public int m_hours = 0;
    public int m_days = 0;
    public bool m_IsRunning = true;

    void Start () {
        StartCoroutine(Playtimer());
	}

    private IEnumerator Playtimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (m_IsRunning)
            {
                m_Timer++;
                m_seconds = (m_Timer % 60);
                m_minutes = (m_Timer / 60) % 60;
                m_hours = (m_Timer / 3600) % 24;
                m_days = (m_Timer / 86400) % 365;
            }
        }
    }
    /// <summary>
    /// Timer count ON
    /// </summary>    
    public void StartTimer()
    {
        m_IsRunning = true;
    }
    /// <summary>
    /// Timer count OFF
    /// </summary> 
    public void PauseTimer()
    {
        m_IsRunning = false;
    }

    public void ToggleTimer()
    {
        m_IsRunning = !m_IsRunning;
    }

    /// <summary>
    /// Timer count back to 0 and On/Off 
    /// </summary> 
    public void ResetTimer(bool startOn)
    {
        m_Timer = 0;
        if (startOn)
            m_IsRunning = true;
        else
            m_IsRunning = false;
    }



}
