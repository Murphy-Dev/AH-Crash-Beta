using Newtonsoft.Json;
using System.Data;
using System.Reflection;

internal class Json_t {
     public string[] Tokens = [];
     public string CrashCmd = ":runc crash";
     public string Browser = "Chrome";
}

public static class Rblx {
     public static string[] Tokens = {};
     public static string Browser = "Chrome";
     public static string CrashCmd = ":runc crash";
     static string? readf(string fname) {
          string content = "";
          FileStream file = new FileStream(fname, FileMode.Open, FileAccess.Read, FileShare.Read);
          for (int bit = file.ReadByte(); bit != null;) { // accidentally put file instead of bit
               content.Append<char>(System.Convert.ToChar(bit));
          }
          return content != "" ? content : null;
     }

     static string? initprop(string prop) {
          string? content = readf("config.json");
          if (content == null) {
               throw new FileNotFoundException("Error! No file content found for config.json!");
          }
          Json_t js = JsonConvert.DeserializeObject<Json_t>(content) ?? new Json_t();
          try {
               FieldInfo? tip = typeof(Rblx).GetField(prop);
               if (tip == null)
                    return "";
               PropertyInfo? dat = typeof(Json_t).GetProperty(prop);
               if (dat == null)
                    return "";
               tip.SetValue(null, dat.GetValue(js));
          } catch (Exception){};
          return prop;
     }

     static Rblx() {
          (from str in new string[] { "Browser", "CrashCmd", "Tokens" } select initprop(str)).ToList();
     }
}
