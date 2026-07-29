using System.Drawing;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.OpenGL;
namespace SectEngine.Rendering;

public class Renderer
{
    private static IWindow _window;
    private static GL _gl;

    private static void OnLoad()
    {
        Console.WriteLine("Loading...");
        _gl = _window.CreateOpenGL();
        _gl.ClearColor(Color.CornflowerBlue);
        
    }

    private static void OnUpdate(double deltaTime) {}

    private static void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    public static void Main(string[] args)
    {
        WindowOptions options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "Cultivation"
        };

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }
}