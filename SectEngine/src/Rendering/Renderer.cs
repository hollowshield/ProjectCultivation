using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SectEngine.Rendering;

public class Renderer : IDisposable
{
    private readonly GL _gl;
    private Buffering _buffering;
    private Shader _shader;

    public Renderer(IWindow window)
    {
        // Native GL context binding
        _gl = window.CreateOpenGL();
        
        // Default pipeline setup
        _gl.ClearColor(Color.CornflowerBlue);
        _gl.Enable(EnableCap.DepthTest);
        _gl.Enable(EnableCap.Blend);
        _gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        //Shader init
        _shader = new Shader(_gl);
        _shader.ProcessShader();
        
        //buffering init
        _buffering = new Buffering(_gl);
        _buffering.Process();
    }

    public void SetViewport(int x, int y, int width, int height)
    {
        _gl.Viewport(x, y, (uint)width, (uint)height);
    }

    public void BeginFrame()
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    public unsafe void Draw()
    {
        _buffering.Bind();
        _shader.Use();
        
        _gl.DrawElements(PrimitiveType.Triangles, (uint)_buffering.indices.Length, DrawElementsType.UnsignedInt, (void*)0);
    }

    public void EndFrame()
    {
        // SwapBuffers is handled automatically by Silk.NET's IWindow render loop,
        // but batch rendering flushing or debug metrics can happen here.
    }

    public void Dispose()
    {
        _shader.Dispose();
        _buffering.Dispose();
        _gl.Dispose();
    }
}