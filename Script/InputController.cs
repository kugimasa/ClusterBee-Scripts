using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool W_KEY, A_KEY, S_KEY, D_KEY, 
    UP_KEY_UP, DOWN_KEY_UP, RIGHT_KEY_UP, LEFT_KEY_UP,
    SHIFT, SPACE, SPACE_KEY_UP = false;

    // Update is called once per frame
    void Update()
    {
        W_KEY = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        A_KEY = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        S_KEY = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        D_KEY = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        SPACE = Input.GetKey(KeyCode.Space);
        SHIFT = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        UP_KEY_UP = Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
        DOWN_KEY_UP = Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
        RIGHT_KEY_UP = Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow);
        LEFT_KEY_UP = Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow);
        SPACE_KEY_UP = Input.GetKeyUp(KeyCode.Space);
    }
}
