using System;
using System.Reflection;

public class Browser : OpenQA.Selenium.WebDriver {
     object? browser;
     public static Browser browser() {
          object? options;
          string? path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
          if (path == null) {
               throw new FileNotFoundException("Error! No directory found.");
          }
          Type? driver_t = Type.GetType(String.Format($"OpenQA.Selenium.{Rblx.Browser}.{Rblx.Browser}Driver"));
          Type? options_t = Type.GetType(String.Format($"OpenQA.Selenium.{Rblx.Browser}.{Rblx.Browser}Options"));
          if (driver_t == null || options_t == null) {
               throw new InvalidDataException("Error! Invalid browser! Try to put a different description or check selenium documentation.");
          }
          options = Activator.CreateInstance(options_t);
          options.GetType().GetMethod("AddArgument").Invoke(options, new object[]{"headless"});
          this.browser = driver_t.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, CallingConventions.HasThis, new[] {typeof(string)}, null).Invoke(new object[] {path + @"/drivers/"});
     }
}