using Silk.NET.Input;

namespace SectEngine.Input;

public class InputManager : IDisposable
{
    private readonly IInputContext _inputContext;

    public InputManager(IInputContext inputContext)
    {
        _inputContext = inputContext;

        foreach (IKeyboard keyboard in _inputContext.Keyboards)
        {
            keyboard.KeyDown += OnKeyDown;
        }
    }

    private void OnKeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        Console.WriteLine($"[Input] KeyDown: {key}");
    }

    public void Update()
    {
        // Polling/State management
    }

    public void Dispose()
    {
        _inputContext.Dispose();
    }
}