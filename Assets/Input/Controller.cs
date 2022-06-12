using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private InputActions InputActions;
    [SerializeField] private PlayerInput PlayerInput;

    private const string GamepadScheme = "Gamepad";
    private const string MouseScheme = "Keyboard&Mouse";

    public static Vector2 Movement;
    public static Vector2 Aiming;

    public static Button JumpButton;
    public static Button InteractButton;
    public static Button ThrowButton;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InputActions = new InputActions();

        //Create actions
        JumpButton = new Button(InputActions.Gameplay.Jump);
        InteractButton = new Button(InputActions.Gameplay.Interact);
        ThrowButton = new Button(InputActions.Gameplay.Throw);

        InputActions.Gameplay.Enable();
    }

    private void Update()
    {
        //Movement
        Movement = InputActions.Gameplay.Movement.ReadValue<Vector2>();

        //Sword rotating
        //If the player is on controller
        if (PlayerInput.currentControlScheme == GamepadScheme)
        {
            //Get the aim value (right stick)
            Aiming = InputActions.Gameplay.GamepadAim.ReadValue<Vector2>();

        }
        //If the player is on keyboard and mouse
        else if (PlayerInput.currentControlScheme == MouseScheme)
        {
            //Get mouse position
            Vector3 mouseworldpos = Camera.main.ScreenToWorldPoint(InputActions.Gameplay.MouseAim.ReadValue<Vector2>());

            //Make it relative to player position & Normalize it
            Aiming = (mouseworldpos - Player.Instance.SwordController.Pivot.position).normalized;
        }

        InteractButton.SetState();
        ThrowButton.SetState();
        JumpButton.SetState();
    }
}

public class Button
{
    /// <summary>
    /// Returns true if the button is being held
    /// </summary>
    public bool Held = false;
    /// <summary>
    /// Returns true when the player presses the button
    /// </summary>
    public bool Pressed = false;
    /// <summary>
    /// Returns true when the player lets go of the button
    /// </summary>
    public bool LetGo = false;

    private InputAction Input;

    public Button(InputAction input)
    {
        Input = input;
    }

    /// <summary>
    /// Set the state of the button depending on how the player is interacting with the relevant input
    /// </summary>
    public void SetState()
    {
        //Get the current input value
        float value = Input.ReadValue<float>();

        //If the button is pressed
        if (value == 1)
        {
            //Button just got pressed
            if (!Pressed && !Held)
            {
                Pressed = true;
                Held = true;
            }
            //Button is being held
            else
            {
                Pressed = false;
                Held = true;
            }
        }
        else
        {
            //Button just got let go
            if (Held)
            {
                Pressed = false;
                Held = false;
                LetGo = true;
            }
            //Button is not being held
            else
            {
                LetGo = false;
            }
        }
    }
}