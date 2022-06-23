using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPanel : MonoBehaviour
{
    private Joystick joystick;

    void Awake()
    {
        joystick = GetComponentInChildren<Joystick>();
    }

    void Update()
    {
        float x = Input.GetAxis(StringAxis.Horizontal);
        float y = Input.GetAxis(StringAxis.Vertical);
        if (!Mathf.Approximately(x, 0f) || !Mathf.Approximately(y, 0f))
        {
            PlayerManager.Instance.PlayerCharacter.InputMove(x, y);
        }
        else
        {
            PlayerManager.Instance.PlayerCharacter.InputMove(joystick.Horizontal, joystick.Vertical);
        }
    }
}
