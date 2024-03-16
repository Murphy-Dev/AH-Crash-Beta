using System.Security.Cryptography;
using OpenQA.Selenium.Chrome;
using System.Net;
using Newtonsoft.Json;
using System.Threading;
using System;
using WindowsInput;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
public static class Crash {
    static List<string> servers = new();
    static string[] unused = Rblx.Tokens;
    static bool completed = false;

    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);

    public static void joinsrv(string cookie) {
        string target = "";
        var driver = (new Browser()).browser;
        Type driverType = driver.GetType();

        MethodInfo minimizeMethod = driverType.GetMethod("Minimize");
        minimizeMethod.Invoke(driver, null);

        // I used ai to write this part because I'm not manually updating it to use reflection, base was made by me tho
        MethodInfo manageMethod = driverType.GetMethod("Manage");
        object manageObject = manageMethod.Invoke(driver, null);
        Type manageType = manageObject.GetType();
        MethodInfo cookiesMethod = manageType.GetMethod("Cookies");
        object cookiesObject = cookiesMethod.Invoke(manageObject, null);
        Type cookiesType = cookiesObject.GetType();
        MethodInfo addCookieMethod = cookiesType.GetMethod("AddCookie");
        addCookieMethod.Invoke(cookiesObject, new object[] { new OpenQA.Selenium.Cookie("RBLXSECURITY", cookie) });
        // ai part end
        using (HttpClient client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) {
            client.BaseAddress = new Uri("https://games.roblox.com/v1/games/333164326/servers/");
            HttpResponseMessage resp = client.GetAsync("Public?sortOrder=Desc&excludeFullGames=true&limit=10").Result;
            resp.EnsureSuccessStatusCode();
            string txt = resp.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(txt);
            List<string> possible = new();
            foreach (var srv in data["data"]) {
                foreach (var val in servers) {
                    if (val != srv["id"]) {
                        possible.append(srv["id"]);
                    }
                }
            }

            target = possible[RandomNumberGenerator.GetInt32(target.Length)];
        }
        Array.Resize(ref servers, servers.Length +  1);
        servers[servers.Length -  1] = target;
        object obj = driverType.GetMethod("Navigate").Invoke(driver, null);
        obj.GetType().GetMethod("GoToUrl").Invoke(obj, new object[] { $"roblox://experiences/start?placeId=333164326&gameInstanceId={target}" });
    }

    public static void crashsrv() {
        foreach (string cookie in Rblx.Tokens) {
            joinsrv(cookie);
        }

        Thread.Sleep(3000);
        foreach (Process process in Process.GetProcessesByName("RobloxPlayerBeta.exe")) {
            SetForegroundWindow(process.MainWindowHandle);
            var sim = new InputSimulator();
            sim.Keyboard.TextEntry('\'');
            Thread.Sleep(10);
            sim.Keyboard.TextEntry(Rblx.CrashCmd);
            Thread.Sleep(1200);
        }
        Thread.Sleep(10000);
        ProcessStartInfo info = new ProcessStartInfo("cmd.exe") { UseShellExecute = true, CreateNoWindow = true, ArgumentList = { "/c TASKKILL /IM /F RobloxPlayerBeta.exe" } };
        Process.Start(info);
    }
}
