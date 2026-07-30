using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
namespace SectEngine.Rendering;

public class Buffering : IDisposable
{
   
    private uint _vbo;
    private readonly GL _gl;
    private uint _vao;
    private uint _ebo;
    
    
    public float[] vertices =
    {
        0.5f,  0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        -0.5f,  0.5f, 0.0f
    };
    public uint[] indices =
    {
        0u, 1u, 3u,
        1u, 2u, 3u
    };
    
    public Buffering(GL gl)
    {
        _gl = gl;
        _vao = _gl.GenVertexArray();
        _vbo = _gl.GenBuffer();
        _ebo = _gl.GenBuffer();

    }

    public void Bind()
    {
        _gl.BindVertexArray(_vao);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
    }

    public void Unbind()
    {
        _gl.BindVertexArray(0);
    }

    public unsafe void Process()
    {
        Bind();
        // Tell OpenGL: Location 0, 3 floats per vertex, stride is 3 * sizeof(float), offset 0
        _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);
        _gl.EnableVertexAttribArray(0);
        // buffers the array vertices and indices
        fixed (float* buf = vertices)
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint) (vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
        fixed (uint* buf = indices)
            _gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint) (indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);
        Unbind();
        
    }






    public void Dispose()
    {
        _gl.DeleteVertexArray(_vao);
        _gl.DeleteBuffer(_vbo);
        _gl.DeleteBuffer(_ebo);
    }

    
}