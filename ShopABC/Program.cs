using ShopABC;
public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        Startup startup = new Startup(builder.Configuration);
        startup.services = builder.Services;
        startup.cauHinh_DichVu(); // calling ConfigureServices method
        startup.app = builder.Build();
        startup.cauHinh_Chung();
    }
}
