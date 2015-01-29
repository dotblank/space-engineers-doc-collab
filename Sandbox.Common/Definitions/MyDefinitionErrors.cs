// Decompiled with JetBrains decompiler
// Type: Sandbox.Definitions.MyDefinitionErrors
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Collections.Generic;
using SysUtils.Utils;
using VRage.Collections;
using VRageMath;

namespace Sandbox.Definitions
{
  public static class MyDefinitionErrors
  {
    private static readonly List<MyDefinitionErrors.Error> m_errors = new List<MyDefinitionErrors.Error>();
    private static readonly MyDefinitionErrors.ErrorComparer m_comparer = new MyDefinitionErrors.ErrorComparer();

    public static bool ShouldShowModErrors { get; set; }

    public static void Clear()
    {
      MyDefinitionErrors.m_errors.Clear();
    }

    public static void Add(MyModContext context, string message, ErrorSeverity severity, bool writeToLog = true)
    {
      MyDefinitionErrors.Error e = new MyDefinitionErrors.Error()
      {
        ModName = context.ModName,
        ErrorFile = context.CurrentFile,
        Message = message,
        Severity = severity
      };
      MyDefinitionErrors.m_errors.Add(e);
      string modName = context.ModName;
      if (writeToLog)
        MyDefinitionErrors.WriteError(e);
      if (severity != ErrorSeverity.Critical)
        return;
      MyDefinitionErrors.ShouldShowModErrors = true;
    }

    public static ListReader<MyDefinitionErrors.Error> GetErrors()
    {
      MyDefinitionErrors.m_errors.Sort((IComparer<MyDefinitionErrors.Error>) MyDefinitionErrors.m_comparer);
      return new ListReader<MyDefinitionErrors.Error>(MyDefinitionErrors.m_errors);
    }

    public static void WriteError(MyDefinitionErrors.Error e)
    {
      MyLog.Default.WriteLine(string.Format("{0}: {1}", (object) e.ErrorSeverity, (object) (e.ModName ?? string.Empty)));
      MyLog.Default.WriteLine("  in file: " + e.ErrorFile ?? string.Empty);
      MyLog.Default.WriteLine("  " + e.Message);
    }

    public class Error
    {
      private static Color[] severityColors = new Color[4]
      {
        Color.Gray,
        Color.Gray,
        Color.White,
        new Color(1f, 0.25f, 0.1f)
      };
      private static string[] severityName = new string[4]
      {
        "notice",
        "warning",
        "error",
        "critical error"
      };
      private static string[] severityNamePlural = new string[4]
      {
        "notices",
        "warnings",
        "errors",
        "critical errors"
      };
      public string ModName;
      public string ErrorFile;
      public string Message;
      public ErrorSeverity Severity;

      public string ErrorId
      {
        get
        {
          return this.ModName != null ? "mod_" : "definition_";
        }
      }

      public string ErrorSeverity
      {
        get
        {
          string str = this.ErrorId;
          switch (this.Severity)
          {
              case Sandbox.Definitions.ErrorSeverity.Notice:
              str = str + "notice";
              break;
              case Sandbox.Definitions.ErrorSeverity.Warning:
              str = str + "warning";
              break;
              case Sandbox.Definitions.ErrorSeverity.Error:
              str = (str + "error").ToUpperInvariant();
              break;
              case Sandbox.Definitions.ErrorSeverity.Critical:
              str = (str + "critical_error").ToUpperInvariant();
              break;
          }
          return str;
        }
      }

      public override string ToString()
      {
        return string.Format("{0}: {1}, in file: {2}\n{3}", (object) this.ErrorSeverity, (object) (this.ModName ?? string.Empty), (object) this.ErrorFile, (object) this.Message);
      }

      public static Color GetSeverityColor(ErrorSeverity severity)
      {
        try
        {
          return MyDefinitionErrors.Error.severityColors[(int) severity];
        }
        catch (Exception ex)
        {
          MyLog.Default.WriteLine(string.Format("Error type does not have color assigned: message: {0}, stack:{1}", (object) ex.Message, (object) ex.StackTrace));
          return Color.White;
        }
      }

      public static string GetSeverityName(ErrorSeverity severity, bool plural)
      {
        try
        {
          if (plural)
            return MyDefinitionErrors.Error.severityNamePlural[(int) severity];
          else
            return MyDefinitionErrors.Error.severityName[(int) severity];
        }
        catch (Exception ex)
        {
          MyLog.Default.WriteLine(string.Format("Error type does not have name assigned: message: {0}, stack:{1}", (object) ex.Message, (object) ex.StackTrace));
          return plural ? "Errors" : "Error";
        }
      }

      public Color GetSeverityColor()
      {
        return MyDefinitionErrors.Error.GetSeverityColor(this.Severity);
      }
    }

    public class ErrorComparer : IComparer<MyDefinitionErrors.Error>
    {
      public int Compare(MyDefinitionErrors.Error x, MyDefinitionErrors.Error y)
      {
        return y.Severity - x.Severity;
      }
    }
  }
}
