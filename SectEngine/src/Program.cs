

namespace SectEngine;

public static class Program
{
    public static void Main(string[] args)
    {
        using var app = new App("Project Cultivation", 1280, 720);
        app.Run();
    }
}