// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Input.MyInputRecording
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.Input
{
    [Obfuscation(Exclude = true, Feature = "cw symbol renaming")]
    [Serializable]
    public class MyInputRecording
    {
        public string Name;
        public string Description;
        public List<MyInputSnapshot> SnapshotSequence;
        public MyInputRecordingSession Session;
        public int OriginalWidth;
        public int OriginalHeight;
        private int m_currentSnapshotNumber;
        private int m_startScreenWidth;
        private int m_startScreenHeight;

        public MyInputRecording()
        {
            this.m_currentSnapshotNumber = 0;
            this.SnapshotSequence = new List<MyInputSnapshot>();
        }

        public bool IsDone()
        {
            return this.m_currentSnapshotNumber == this.SnapshotSequence.Count;
        }

        public void Save(string filename)
        {
            using (TextWriter textWriter = (TextWriter) new StreamWriter(filename))
                new XmlSerializer(typeof (MyInputRecording)).Serialize(textWriter, (object) this);
        }

        public void SetStartingScreenDimensions(int width, int height)
        {
            this.m_startScreenWidth = width;
            this.m_startScreenHeight = height;
        }

        public int GetStartingScreenWidth()
        {
            return this.m_startScreenWidth;
        }

        public int GetStartingScreenHeight()
        {
            return this.m_startScreenHeight;
        }

        public Vector2 GetMouseNormalizationFactor()
        {
            return new Vector2((float) this.m_startScreenWidth/(float) this.OriginalWidth,
                (float) this.m_startScreenHeight/(float) this.OriginalHeight);
        }

        public MyInputSnapshot GetNextSnapshot()
        {
            return this.SnapshotSequence[this.m_currentSnapshotNumber++];
        }

        public static MyInputRecording FromFile(string filename)
        {
            using (StreamReader streamReader = new StreamReader(filename))
                return
                    (MyInputRecording)
                        new XmlSerializer(typeof (MyInputRecording)).Deserialize((TextReader) streamReader);
        }

        public void AddSnapshot(MyInputSnapshot snapshot)
        {
            this.SnapshotSequence.Add(snapshot);
        }
    }
}