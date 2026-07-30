using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.Input;
using SectEngine.Rendering;
using SectEngine.Input;

namespace SectEngine.Core;

public class App : IDisposable
{
    private readonly IWindow _window;
    private Renderer? _renderer;
    private InputManager? _input;

    public App(string title = "SectEngine", int width = 800, int height = 600)
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(width, height),
            Title = title,
            VSync = true
        };

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;
        _window.FramebufferResize += OnFramebufferResize;
        _window.Closing += OnClosing;
    }

    public void Run() => _window.Run();

    private void OnLoad()
    {
        // Initialize subsystems once GL/Window contexts are created
        _renderer = new Renderer(_window);
        
        IInputContext inputContext = _window.CreateInput();
        _input = new InputManager(inputContext);

        Console.WriteLine("[Application] Engine initialized successfully.");
    }

    private void OnUpdate(double deltaTime)
    {
        // Update input state, physics, game logic
        _input?.Update();
    }

    private void OnRender(double deltaTime)
    {
        if (_renderer == null) return;

        _renderer.BeginFrame();
        
        // Draw scene objects here in the future:
        _renderer.Draw();

        _renderer.EndFrame();
    }

    private void OnFramebufferResize(Vector2D<int> newSize)
    {
        _renderer?.SetViewport(0, 0, newSize.X, newSize.Y);
    }

    private void OnClosing()
    {
        Dispose();
    }

    public void Dispose()
    {
        _input?.Dispose();
        _renderer?.Dispose();
        _window.Dispose();
    }
}
