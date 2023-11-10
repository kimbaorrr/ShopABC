using ShopABC;
public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        Startup startup = new Startup(builder.Configuration);
        startup.DichVuWeb = builder.Services;
        startup.cauHinh_DichVu(); // calling ConfigureServices method
        startup.UngDungWeb = builder.Build();
        startup.cauHinh_Chung();
    }
}
