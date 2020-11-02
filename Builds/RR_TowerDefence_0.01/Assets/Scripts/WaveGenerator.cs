﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class WaveAction
{
    public string name;
    public float delay;
    public GameObject prefab;
    public int spawnCount;
    public string message;
}

[System.Serializable]
public class Wave
{
    public string name;
    public List<WaveAction> actions;
}



public class WaveGenerator : MonoBehaviour
{
    public float difficultyFactor = 0.9f;
    public List<Wave> waves;
    private Wave m_CurrentWave;
    public Wave CurrentWave { get { return m_CurrentWave; } }
    private float m_DelayFactor = 1.0f;

    IEnumerator SpawnLoop()
    {
        m_DelayFactor = 1.0f;
        while (true)
        {
            foreach (Wave W in waves)
            {
                m_CurrentWave = W;
                foreach (WaveAction A in W.actions)
                {
                    if (A.delay > 0)
                        yield return new WaitForSeconds(A.delay * m_DelayFactor);
                    if (A.message != "")
                    {
                        // dev print
                    }
                    if (A.prefab != null && A.spawnCount > 0)
                    {
                        for (int i = 0; i < A.spawnCount; i++)
                        {
                            //Instantiate(A.prefab, new Vector3(0,0,0), Quaternion.identity);
                            //still need to instantiate, keep fucking it up
                        }
                    }
                }
                yield return null;  // just in case its 0
            }
            m_DelayFactor *= difficultyFactor;
            yield return null;  // same
        }
    }
    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

}