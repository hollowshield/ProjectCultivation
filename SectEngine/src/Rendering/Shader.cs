using Silk.NET.OpenGL;

namespace SectEngine.Rendering;

public class Shader: IDisposable
{
    private uint _program;
    private uint _vertexShader;
    private uint _fragmentShader;
    private readonly GL _gl;
    const string vertexCode = @"
#version 330 core

layout (location = 0) in vec3 aPosition;

void main()
{
    gl_Position = vec4(aPosition, 1.0);
}";
    const string fragmentCode = @"
#version 330 core

out vec4 out_color;

void main()
{
    out_color = vec4(1.0, 0.5, 0.2, 1.0);
}";

    public Shader(GL gl)
    {
        _gl = gl;
        uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);

        _vertexShader = vertexShader;
        _fragmentShader = fragmentShader;

        
        _program =  _gl.CreateProgram();
    }


    public void ProcessShader()
    {   
        
      
        _gl.ShaderSource(_vertexShader, vertexCode);
        _gl.CompileShader(_vertexShader);
        
        _gl.GetShader(_vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
        if (vStatus != (int) GLEnum.True)
            throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(_vertexShader));
        
        
        _gl.ShaderSource(_fragmentShader, fragmentCode);

        _gl.CompileShader(_fragmentShader);

        _gl.GetShader(_fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
        if (fStatus != (int) GLEnum.True)
            throw new Exception("Fragment shader failed to compile: " + _gl.GetShaderInfoLog(_fragmentShader));
        _gl.AttachShader(_program, _vertexShader);
        _gl.AttachShader(_program, _fragmentShader);

        _gl.LinkProgram(_program);

        _gl.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);
        if (lStatus != (int) GLEnum.True)
            throw new Exception("Program failed to link: " + _gl.GetProgramInfoLog(_program));
        _gl.DetachShader(_program, _vertexShader);
        _gl.DetachShader(_program, _fragmentShader);
        _gl.DeleteShader(_vertexShader);
        _gl.DeleteShader(_fragmentShader);
    }

    public void Use()
    {
        _gl.UseProgram(_program);
    }

    public void Dispose()
    {
        
        _gl.DeleteProgram(_program);
    }
    
}