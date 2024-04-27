using Blazored.LocalStorage;
using DsK.ITSM.Client.Pages;
using DsK.ITSM.Client.Services;
using DsK.ITSM.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DsK.ITSM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddInteractiveWebAssemblyComponents();

            string serverlessBaseURI = builder.Configuration["ApiUrl"];
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(serverlessBaseURI) });

            //Add Authorization Core - To be able to use [CascadingAuthenticationState, AuthorizeRouteView, Authorizing], [AuthorizeView, NotAuthorized, Authorized], @attribute [Authorize]
            builder.Services.AddAuthorizationCore();
            
            //The CustomAuthenticationStateProvider is to be able to use tokens as the mode of authentication.
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<SecurityServiceClient>();

            /* ---Manages saving to local storage--- */
            builder.Services.AddBlazoredLocalStorage();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

            app.Run();
        }
    }
}
