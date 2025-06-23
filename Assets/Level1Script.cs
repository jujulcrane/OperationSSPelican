using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour
{
    private bool up;
    private bool left;
    private bool right;

    // Start is called before the first frame update
    void Start()
    {
         up = false;
        left = false;
        right = false;
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            right = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            left = true;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            up = true;
        }
        if (up)
        {
            if (left || right)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
