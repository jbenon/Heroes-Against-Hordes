using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private HeroInputActions heroInputActions;

    private void Awake()
    {
        heroInputActions = new HeroInputActions();
        heroInputActions.Hero.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = heroInputActions.Hero.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
