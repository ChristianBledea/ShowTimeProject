using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Services;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using ShowTime.DataAccess.Repositories.Implementations;
using ShowTime.Client.Pages;

// Removed the problematic line 'AddRazorComponents' as it is not a valid method for IServiceCollection.
// If Razor components are required, ensure the correct package is installed and use the appropriate method.

var builder = WebAssemblyHostBuilder.CreateDefault(args);




await builder.Build().RunAsync();
