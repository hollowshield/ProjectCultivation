using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SectEngine.Rendering;

public class Renderer : IDisposable
{
    private readonly GL _gl;

    public Renderer(IWindow window)
    {
        // Native GL context binding
        _gl = window.CreateOpenGL();
        
        // Default pipeline setup
        _gl.ClearColor(Color.CornflowerBlue);
        _gl.Enable(EnableCap.DepthTest);
        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    public void SetViewport(int x, int y, int width, int height)
    {
        _gl.Viewport(x, y, (uint)width, (uint)height);
    }

    public void BeginFrame()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public void EndFrame()
    {
        // SwapBuffers is handled automatically by Silk.NET's IWindow render loop,
        // but batch rendering flushing or debug metrics can happen here.
    }

    public void Dispose()
    {
        _gl.Dispose();
    }
}