using System;
using System.IO;

//Based on the code from here, but I added the ability to just read the .ENV from the project root
//https://dusted.codes/dotenv-in-dotnet
public static class DotEnv
{
    public static void Load()
    {
        //when called from Program.cs with ASP.NET it's the current project directory
        //not the build one... which... eek
        var execDirectory = Directory.GetCurrentDirectory();
        var filePath = Path.Combine(execDirectory, ".env");
        if (!File.Exists(filePath)) return;
        Console.WriteLine("Loading .env file", filePath);
        foreach (var line in File.ReadAllLines(filePath))
        {

          if(line.IndexOf("#") == 0) continue; //it's a comment

          //get the first index of = and split on that; the values might have = in them so split won't work
          var idx = line.IndexOf('=');
          if(idx <= 0) continue; //empty line skip it
          //var parts = line.Split('=',StringSplitOptions.RemoveEmptyEntries);
          var key = line.Substring(0, idx);
          var val = line.Substring(idx + 1);

          //if (parts.Length != 2)continue;
          Console.WriteLine("Setting {0} to {1}", key, val);
          Environment.SetEnvironmentVariable(key, val.Replace("\"", ""));
        }
    }
}
