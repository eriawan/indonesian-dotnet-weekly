// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;

Console.WriteLine($"Hello, .NET version: {Environment.Version.ToString()}!");
var currentDate = DateTime.Now;
Console.WriteLine($"Current date and time is: {currentDate.ToString("MM/dd/yyyy HH:mm:ss")}");