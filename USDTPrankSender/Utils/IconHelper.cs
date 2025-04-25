using System;
using System.Drawing;
using System.Reflection;
using System.Linq;

namespace USDTSender.Utils
{
  public static class IconHelper
  {
    public static Icon LoadIcon(string fileName)
    {
      try
      {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(name => name.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

        Console.WriteLine("Available resources:");
        foreach (var name in assembly.GetManifestResourceNames())
        {
          Console.WriteLine(name);
        }

        Console.WriteLine($"Trying to load: {resourceName}");

        if (resourceName == null)
          throw new Exception($"Embedded resource '{fileName}' not found.");

        using var stream = assembly.GetManifestResourceStream(resourceName);
        return new Icon(stream);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading icon: {ex.Message}");
        return SystemIcons.Application; // Fallback icon
      }
    }
  }
}
