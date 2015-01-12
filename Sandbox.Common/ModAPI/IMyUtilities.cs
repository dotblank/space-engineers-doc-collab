// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyUtilities
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using System;
using System.IO;

namespace Sandbox.ModAPI
{
    public interface IMyUtilities
    {
        IMyConfigDedicated ConfigDedicated { get; }

        IMyGamePaths GamePaths { get; }

        bool IsDedicated { get; }

        event MessageEnteredDel MessageEntered;

        string GetTypeName(Type type);

        void ShowNotification(string message, int disappearTimeMs = 2000, MyFontEnum font = MyFontEnum.White);

        void ShowMessage(string sender, string messageText);

        void SendMessage(string messageText);

        bool FileExistsInGlobalStorage(string file);

        bool FileExistsInLocalStorage(string file, Type callingType);

        void DeleteFileInLocalStorage(string file, Type callingType);

        void DeleteFileInGlobalStorage(string file);

        TextReader ReadFileInGlobalStorage(string file);

        TextReader ReadFileInLocalStorage(string file, Type callingType);

        TextWriter WriteFileInGlobalStorage(string file);

        TextWriter WriteFileInLocalStorage(string file, Type callingType);

        string SerializeToXML<T>(T objToSerialize);

        T SerializeFromXML<T>(string buffer);

        void InvokeOnGameThread(Action action);

        void ShowMissionScreen(string screenTitle = null, string currentObjectivePrefix = null,
            string currentObjective = null, string screenDescription = null, Action<ResultEnum> callback = null,
            string okButtonCaption = null);

        IMyHudObjectiveLine GetObjectiveLine();

        BinaryReader ReadBinaryFileInGlobalStorage(string file);

        BinaryReader ReadBinaryFileInLocalStorage(string file, Type callingType);

        BinaryWriter WriteBinaryFileInGlobalStorage(string file);

        BinaryWriter WriteBinaryFileInLocalStorage(string file, Type callingType);
    }
}