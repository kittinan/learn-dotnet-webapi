using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CustomHostedService : IHostedService
{
    private readonly ILogger<CustomHostedService> _logger;

    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IServiceScopeFactory _scopeFactory;

    public CustomHostedService(IHostApplicationLifetime appLifetime, ILogger<CustomHostedService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CustomHostedService is starting.");
        _appLifetime.ApplicationStarted.Register(() =>
        {
            Task.Run(async () =>
            {
                try
                {
                    Console.WriteLine("Running custom management commands...");

                    // Example: Fetch command and arguments
                    var args = Environment.GetCommandLineArgs();

                    // length of args should be at least 2
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Please provide a command.");
                        return;
                    }

                    var command = args[1];

                    switch (command)
                    {
                        case "create-user":
                            if (args.Length == 4) // Expecting 3 arguments: command, username, and password
                            {
                                var username = args[2];
                                var password = args[3];
                                await CreateUser(username, password);
                            }
                            else
                            {
                                Console.WriteLine("Please provide both username and password.");
                            }
                            break;
                        default:
                            Console.WriteLine("Unknown command.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // Gracefully shut down the application after running commands
                    //_appLifetime.StopApplication();
                }
            });
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("CustomHostedService is stopping.");
        return Task.CompletedTask;
    }

    private async Task CreateUser(string username, string password)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = new IdentityUser { UserName = username, Email = username };
            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                Console.WriteLine($"User '{username}' created successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to create user '{username}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}