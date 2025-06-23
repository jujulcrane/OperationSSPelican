using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNameController : MonoBehaviour
{
    public static int level = 1;
    public static int ghostCount = 0;
    public static int highScore = 0;
    public static bool hard = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void incrementScore()
    {
        ghostCount++;
    }
}
