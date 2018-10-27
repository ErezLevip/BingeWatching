using BingeWatching.API.Client.Abstractions;
using BingeWatching.API.ViewModels;
using BingeWatching.Contracts.ViewModels;
using BingeWatching.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BingeWatching.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.Create();
            var serviceClient = container.GetService<IBingeWatchingApiClient>();
            var app = new CommandLineApplication();
            app.OnExecute(async () =>
            {
                while (true)
                {
                    try
                    {
                        var userId = ConsoleUtils.AskUserForData<string>("Please enter your user id:");
                        var selection = ConsoleUtils.AskUserForDataMultiLine<string>(new string[]
                        {
                            "What would you like to do?",
                            "S - swich user",
                            "E - exit",
                            "H - view movie history",
                            "C - content"
                        });

                        switch (selection.ToLower())
                        {
                            case "e": // exit
                                return 0;
                            case "h": // show history
                                await DisplayUserHistory(serviceClient, userId);
                                break;
                            case "c": // choose content
                                var content = await GetContentFromSelection(serviceClient, userId);
                                await Ratings(serviceClient, userId, content);
                                break;
                            default:
                                break;

                        }
                    }
                    catch { }
                }
            });
            app.Execute(args);
        }

        private static async Task DisplayUserHistory(IBingeWatchingApiClient serviceClient, string userId)
        {
            var results = await serviceClient.Get(new HistoryRequest
            {
                UserId = userId
            });
            if (results.Value.Content.Count == 0)
                Console.WriteLine("There is no history to show...");
            else
                ConsoleUtils.PrintMultipleObjects(results.Value.Content);
        }

        private static async Task<ContentViewModel> GetContentFromSelection(IBingeWatchingApiClient serviceClient, string userId)
        {
            (var contentSelection, var contentType) = ContentMenuSelection();

            var content = await serviceClient.Get(new ContentRequest
            {
                UserId = userId,
                ContentType = contentType
            });
            Console.WriteLine($"You are now watching {content.Value.Title}");
            return content.Value;
        }

        private static async Task Ratings(IBingeWatchingApiClient serviceClient, string userId, ContentViewModel content)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            await ContentStateChecker.WaitForCancellation(async () =>
            {
                var finished = ConsoleUtils.AskUserForData<string>($"Did you finish watching {content.Title}?(y/n):") == "y";
                if (finished)
                {
                    var rating = ConsoleUtils.AskUserForData<int>("Tell us what you thought, Rate from 0 to 10:");
                    var res = await serviceClient.Post(new RecommendationRequest
                    {
                        ContentId = content.Id,
                        Score = rating,
                        UserId = userId
                    });
                    if (res is BadRequestObjectResult)
                    {
                        Console.WriteLine(((BadRequestObjectResult)res).Value);
                        return false;
                    }
                    cancellationTokenSource.Cancel();
                    return false;
                }
                return true;
            }, cancellationTokenSource.Token);
            Console.WriteLine("Thank you!");
        }

        private static (string, ContentType) ContentMenuSelection()
        {
            var selection = ConsoleUtils.AskUserForDataMultiLine<string>(new string[]
                                             {
                                                "What kind of content do you want?",
                                                "1 - TV Show",
                                                "2 - Movie",
                                                "3 - Any"
                                             });

            var contentType = ContentType.Both;
            switch (selection)
            {
                case "1":
                    contentType = ContentType.Show;
                    break;
                case "2":
                    contentType = ContentType.Movie;
                    break;
                default:
                    break;
            }
            return (selection, contentType);
        }
    }
}
