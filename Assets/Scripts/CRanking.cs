using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRanking
{
    public string name;
    public float record;

    public CRanking() { }

    public CRanking(string n, float r)
    {
        name = n;
        record = r;
    }
}
