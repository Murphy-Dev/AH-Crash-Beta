using System;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

public class Program {
     public static void Main(string[] args) {
          {
            ProcessStartInfo pri = new ProcessStartInfo("cmd.exe", "/c TASKKILL /IM /F RobloxPlayerBeta.exe")
            {
                CreateNoWindow = true,
                UseShellExecute = true
            };
            Process? pr = Process.Start(pri);
               bool newd = true;
               new Mutex(true, "Roblox_LSingletonMutex", out newd);
          }

          {
               Process[] processes = Process.GetProcessesByName("rbxfpsunlocker.exe");
               if (processes.Length >= 1) {
                    throw new Exception("Error! Roblox FPS Unlocker must be running for this crasher to work");
               }
          }

          if (Rblx.Tokens.Length >= 1) {
               throw new Exception("Error! Not enough tokens!");
          }
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Title = "AH Crasher";
          Console.WriteLine(@"
          ░█████╗░██╗░░██╗  ░█████╗░██████╗░░█████╗░░██████╗██╗░░██╗  ██████╗░░█████╗░████████╗
          ██╔══██╗██║░░██║  ██╔══██╗██╔══██╗██╔══██╗██╔════╝██║░░██║  ██╔══██╗██╔══██╗╚══██╔══╝
          ███████║███████║  ██║░░╚═╝██████╔╝███████║╚█████╗░███████║  ██████╦╝██║░░██║░░░██║░░░
          ██╔══██║██╔══██║  ██║░░██╗██╔══██╗██╔══██║░╚═══██╗██╔══██║  ██╔══██╗██║░░██║░░░██║░░░
          ██║░░██║██║░░██║  ╚█████╔╝██║░░██║██║░░██║██████╔╝██║░░██║  ██████╦╝╚█████╔╝░░░██║░░░
          ╚═╝░░╚═╝╚═╝░░╚═╝  ░╚════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═════╝░╚═╝░░╚═╝  ╚═════╝░░╚════╝░░░░╚═╝░░░
          ");
          Console.ResetColor();
          Console.WriteLine("Press any key to start");
          Console.ReadKey();
          while (true) {
               Crash.crashsrv();
          }

     }
}