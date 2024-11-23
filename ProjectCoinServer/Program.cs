namespace ProjectCoinServer;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        app.MapControllers();
        app.UseCors(builder => {
            builder
                .AllowAnyOrigin() // 모든 도메인 허용
                .AllowAnyMethod() // 모든 HTTP 메서드 허용
                .AllowAnyHeader();
        });
        app.Run();
    }
}
