using System;
using UnityEngine;

/// <summary>
/// <br>Provides a set of <see cref="Action"/>s. Some of them accept <see cref="float"/> as an argument.</br>
/// <br><see cref="Time.deltaTime"/> is passed to this argument when the event is invoked.</br>
/// </summary>
public sealed class InputHandler : MonoBehaviour
{
	public event Action RestartPressed;

	public event Action<float> UpPressed;
    public event Action<float> DownPressed;
    public event Action<float> LeftPressed;
    public event Action<float> RightPressed;

    [Header("Actions")]
    [SerializeField] private KeyCode _restartKey = KeyCode.R;

    [Header("Movement")]
    [SerializeField] private KeyCode _upKey = KeyCode.W;
    [SerializeField] private KeyCode _downKey = KeyCode.S;
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;

    public bool ScansInput { get; set; } = true;

	private void Update()
	{
        if (ScansInput == false)
            return;

		if (Input.GetKeyDown(_restartKey))
            RestartPressed?.Invoke();

        if (Input.GetKey(_upKey))
            UpPressed?.Invoke(Time.deltaTime);

        if (Input.GetKey(_downKey))
            DownPressed?.Invoke(Time.deltaTime); 

        if (Input.GetKey(_leftKey))
            LeftPressed?.Invoke(Time.deltaTime);
		
        if (Input.GetKey(_rightKey))
			RightPressed?.Invoke(Time.deltaTime);
	}
}
