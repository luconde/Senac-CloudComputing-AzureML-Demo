using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");

    // Personalizado para suportar alterações
    var pobjConfiguration = builder.Configuration;

    // Obtem o servidor de destino
    var strTarget = pobjConfiguration.GetValue<string>("Deploy:Target");
    Console.WriteLine($"Target : {pobjConfiguration.GetValue<string>("Deploy:Target")}");
    if (strTarget == "NGINX")
    {
        // Para funcionar em sub-diretorio
        app.UsePathBase(pobjConfiguration.GetValue<string>("Deploy:UsePathBase"));
        app.UsePathBase(pobjConfiguration.GetValue<string>("Deploy:UsePathBase"));
    }
}
else
{
    if(app.Environment.IsDevelopment())
    {
        //Deixado para referencia futura
    }
    else
    {
        if(app.Environment.IsStaging())
        {
            // Deixado para referencia futura
        }
    }
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
