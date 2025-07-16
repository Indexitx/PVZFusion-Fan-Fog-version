using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternEffected : MonoBehaviour
{
    public int lightLevel;
    public List<Plantern> planterns;
    public void NeedAwake()
    {
        planterns = new List<Plantern>();
    }
    public int CheckLightLevel()
    {
        lightLevel = 0;
        foreach(Plantern plantern in planterns)
        {
            if (plantern == null) continue;
            else
            {
                lightLevel++;
            }
        }
        return lightLevel;
    }
    public void Upd()
    {
        planterns = new List<Plantern>();
    }
    public void AddPlantern(Plantern p)
    {
        planterns.Add(p);
    }
    public void RemovePlantern(Plantern p)
    {
        planterns.Remove(p);
    }
}
