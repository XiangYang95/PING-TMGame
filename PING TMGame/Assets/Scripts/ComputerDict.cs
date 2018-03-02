using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Computer { W, A, S, D};

public class ComputerDict : MonoBehaviour
{
    public static ComputerDict cd;

    public Dictionary<Computer, string> CompToString = new Dictionary<Computer, string>();

    void Awake()
    {
        if (cd == null)
            cd = this;
        else
            Destroy(this.gameObject);
        
        CompToString.Add(Computer.A, "A");
        CompToString.Add(Computer.W, "W");
        CompToString.Add(Computer.S, "S");
        CompToString.Add(Computer.D, "D");
    }
    
}