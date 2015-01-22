// Decompiled with JetBrains decompiler
// Type: DShowNET.VideoPlayer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;
using System.Threading;
using VRage.Collections;

namespace DShowNET
{
  public abstract class VideoPlayer : ISampleGrabberCB, IDisposable
  {
    private Guid MEDIATYPE_Video = new Guid(1935960438, (short) 0, (short) 16, (byte) byte.MinValue, (byte) 0, (byte) 0, (byte) 170, (byte) 0, (byte) 56, (byte) 155, (byte) 113);
    private Guid MEDIASUBTYPE_RGB24 = new Guid(3828804477U, (ushort) 21071, (ushort) 4558, (byte) 159, (byte) 83, (byte) 0, (byte) 32, (byte) 175, (byte) 11, (byte) 167, (byte) 112);
    private Guid MEDIASUBTYPE_RGB32 = new Guid(3828804478U, (ushort) 21071, (ushort) 4558, (byte) 159, (byte) 83, (byte) 0, (byte) 32, (byte) 175, (byte) 11, (byte) 167, (byte) 112);
    private Guid FORMAT_VideoInfo = new Guid(89694080U, (ushort) 50006, (ushort) 4558, (byte) 191, (byte) 1, (byte) 0, (byte) 170, (byte) 0, (byte) 85, (byte) 89, (byte) 90);
    private byte alphaTransparency = byte.MaxValue;
    private object m_comObject;
    private IGraphBuilder m_graphBuilder;
    private IMediaControl m_mediaControl;
    private IMediaEventEx m_mediaEvent;
    private IMediaPosition m_mediaPosition;
    private IBasicAudio m_basicAudio;
    private IMediaSeeking m_mediaSeeking;
    private Thread updateThread;
    private string filename;
    private MySwapQueue<byte[]> m_videoDataRgba;
    private int videoWidth;
    private int videoHeight;
    private long avgTimePerFrame;
    private int bitRate;
    private VideoState currentState;
    private bool isDisposed;
    private long currentPosition;
    private long videoDuration;

    public int VideoWidth
    {
      get
      {
        return this.videoWidth;
      }
    }

    public int VideoHeight
    {
      get
      {
        return this.videoHeight;
      }
    }

    public double CurrentPosition
    {
      get
      {
        return (double) this.currentPosition / 10000000.0;
      }
      set
      {
        if (value < 0.0)
          value = 0.0;
        if (value > this.Duration)
          value = this.Duration;
        this.m_mediaPosition.put_CurrentPosition(value);
        this.currentPosition = (long) value * 10000000L;
      }
    }

    public string CurrentPositionAsTimeString
    {
      get
      {
        double num1 = (double) this.currentPosition / 10000000.0;
        double num2 = num1 / 60.0;
        int num3 = (int) Math.Floor(num2 / 60.0);
        int num4 = (int) Math.Floor(num2 - (double) (num3 * 60));
        int num5 = (int) Math.Floor(num1 - (double) (num4 * 60));
        return (num3 < 10 ? "0" + num3.ToString() : num3.ToString()) + ":" + (num4 < 10 ? "0" + num4.ToString() : num4.ToString()) + ":" + (num5 < 10 ? "0" + num5.ToString() : num5.ToString());
      }
    }

    public double Duration
    {
      get
      {
        return (double) this.videoDuration / 10000000.0;
      }
    }

    public string DurationAsTimeString
    {
      get
      {
        double num1 = (double) this.videoDuration / 10000000.0;
        double num2 = num1 / 60.0;
        int num3 = (int) Math.Floor(num2 / 60.0);
        int num4 = (int) Math.Floor(num2 - (double) (num3 * 60));
        int num5 = (int) Math.Floor(num1 - (double) (num4 * 60));
        return (num3 < 10 ? "0" + num3.ToString() : num3.ToString()) + ":" + (num4 < 10 ? "0" + num4.ToString() : num4.ToString()) + ":" + (num5 < 10 ? "0" + num5.ToString() : num5.ToString());
      }
    }

    public string FileName
    {
      get
      {
        return this.filename;
      }
    }

    public VideoState CurrentState
    {
      get
      {
        return this.currentState;
      }
      set
      {
        switch (value)
        {
          case VideoState.Playing:
            this.Play();
            break;
          case VideoState.Paused:
            this.Pause();
            break;
          case VideoState.Stopped:
            this.Stop();
            break;
        }
      }
    }

    public bool IsDisposed
    {
      get
      {
        return this.isDisposed;
      }
    }

    public int FramesPerSecond
    {
      get
      {
        if (this.avgTimePerFrame == 0L)
          return -1;
        else
          return (int) Math.Round((double) (1f / ((float) this.avgTimePerFrame / 1E+07f)), 0, MidpointRounding.ToEven);
      }
    }

    public float MillisecondsPerFrame
    {
      get
      {
        if (this.avgTimePerFrame == 0L)
          return -1f;
        else
          return (float) this.avgTimePerFrame / 10000f;
      }
    }

    public byte AlphaTransparency
    {
      get
      {
        return this.alphaTransparency;
      }
      set
      {
        this.alphaTransparency = value;
      }
    }

    public float Volume
    {
      get
      {
        int plVolume;
        this.m_basicAudio.get_Volume(out plVolume);
        return (float) ((double) plVolume / 10000.0 + 1.0);
      }
      set
      {
        this.m_basicAudio.put_Volume((int) (((double) value - 1.0) * 10000.0));
      }
    }

    protected VideoPlayer(string FileName)
    {
      try
      {
        this.currentState = VideoState.Stopped;
        this.filename = FileName;
        this.InitInterfaces();
        Type typeFromClsid = Type.GetTypeFromCLSID(Clsid.SampleGrabber);
        if (typeFromClsid == (Type) null)
          throw new NotSupportedException("DirectX (8.1 or higher) not installed?");
        this.m_comObject = Activator.CreateInstance(typeFromClsid);
        ISampleGrabber sampleGrabber = (ISampleGrabber) this.m_comObject;
        this.m_graphBuilder.AddFilter((IBaseFilter) this.m_comObject, "Grabber");
        sampleGrabber.SetMediaType(new AMMediaType()
        {
          majorType = this.MEDIATYPE_Video,
          subType = this.MEDIASUBTYPE_RGB32,
          formatType = this.FORMAT_VideoInfo
        });
        this.m_graphBuilder.RenderFile(this.filename, (string) null);
        sampleGrabber.SetBufferSamples(true);
        sampleGrabber.SetOneShot(false);
        sampleGrabber.SetCallback((ISampleGrabberCB) this, 1);
        ((IVideoWindow) this.m_graphBuilder).put_AutoShow(0);
        AMMediaType pmt = new AMMediaType();
        sampleGrabber.GetConnectedMediaType(pmt);
        VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
        Marshal.PtrToStructure(pmt.formatPtr, (object) videoInfoHeader);
        this.videoHeight = videoInfoHeader.BmiHeader.Height;
        this.videoWidth = videoInfoHeader.BmiHeader.Width;
        this.avgTimePerFrame = videoInfoHeader.AvgTimePerFrame;
        this.bitRate = videoInfoHeader.BitRate;
        this.m_mediaSeeking.GetDuration(out this.videoDuration);
        this.m_videoDataRgba = new MySwapQueue<byte[]>((Func<byte[]>) (() => new byte[this.videoHeight * this.videoWidth * 4]));
      }
      catch (Exception ex)
      {
        throw new Exception("Unable to Load or Play the video file", ex);
      }
    }

    private void InitInterfaces()
    {
      Type typeFromClsid = Type.GetTypeFromCLSID(Clsid.FilterGraph);
      if (typeFromClsid == (Type) null)
        throw new NotSupportedException("DirectX (8.1 or higher) not installed?");
      object instance = Activator.CreateInstance(typeFromClsid);
      this.m_graphBuilder = (IGraphBuilder) instance;
      this.m_mediaControl = (IMediaControl) instance;
      this.m_mediaEvent = (IMediaEventEx) instance;
      this.m_mediaSeeking = (IMediaSeeking) instance;
      this.m_mediaPosition = (IMediaPosition) instance;
      this.m_basicAudio = (IBasicAudio) instance;
    }

    private void CloseInterfaces()
    {
      if (this.m_mediaEvent != null)
      {
        this.m_mediaControl.Stop();
        this.m_mediaEvent.SetNotifyWindow(IntPtr.Zero, 32769, IntPtr.Zero);
      }
      this.m_mediaControl = (IMediaControl) null;
      this.m_mediaEvent = (IMediaEventEx) null;
      this.m_graphBuilder = (IGraphBuilder) null;
      this.m_mediaSeeking = (IMediaSeeking) null;
      this.m_mediaPosition = (IMediaPosition) null;
      this.m_basicAudio = (IBasicAudio) null;
      if (this.m_comObject != null)
        Marshal.ReleaseComObject(this.m_comObject);
      this.m_comObject = (object) null;
    }

    public void Update()
    {
      if (this.m_videoDataRgba.RefreshRead())
        this.OnFrame(this.m_videoDataRgba.Read);
      this.m_mediaSeeking.GetCurrentPosition(out this.currentPosition);
      if (this.currentPosition < this.videoDuration)
        return;
      this.currentState = VideoState.Stopped;
    }

    protected abstract void OnFrame(byte[] frameData);

    public void Play()
    {
      if (this.currentState == VideoState.Playing)
        return;
      this.m_mediaControl.Run();
      this.currentState = VideoState.Playing;
    }

    public void Pause()
    {
      this.m_mediaControl.Stop();
      this.currentState = VideoState.Paused;
    }

    public void Stop()
    {
      this.m_mediaControl.Stop();
      this.m_mediaSeeking.SetPositions(new DsOptInt64(0L), SeekingFlags.AbsolutePositioning, new DsOptInt64(0L), SeekingFlags.NoPositioning);
      this.currentState = VideoState.Stopped;
    }

    public void Rewind()
    {
      this.Stop();
      this.Play();
    }

    public int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
    {
      byte[] write = this.m_videoDataRgba.Write;
      byte num = this.alphaTransparency;
      Marshal.Copy(pBuffer, write, 0, BufferLen);
      int index = 3;
      while (index < BufferLen)
      {
        write[index] = num;
        index += 4;
      }
      this.m_videoDataRgba.CommitWrite();
      return 0;
    }

    public int SampleCB(double SampleTime, IMediaSample pSample)
    {
      return 0;
    }

    public virtual void Dispose()
    {
      this.isDisposed = true;
      this.Stop();
      this.CloseInterfaces();
      this.m_videoDataRgba = (MySwapQueue<byte[]>) null;
    }
  }
}
