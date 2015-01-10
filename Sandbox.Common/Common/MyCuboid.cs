// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyCuboid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using VRageMath;

namespace Sandbox.Common
{
  public class MyCuboid
  {
    public MyCuboidSide[] Sides = new MyCuboidSide[6];

    public IEnumerable<Line> UniqueLines
    {
      get
      {
        yield return this.Sides[0].Lines[0];
        yield return this.Sides[0].Lines[1];
        yield return this.Sides[0].Lines[2];
        yield return this.Sides[0].Lines[3];
        yield return this.Sides[1].Lines[0];
        yield return this.Sides[1].Lines[1];
        yield return this.Sides[1].Lines[2];
        yield return this.Sides[1].Lines[3];
        yield return this.Sides[2].Lines[0];
        yield return this.Sides[2].Lines[2];
        yield return this.Sides[4].Lines[1];
        yield return this.Sides[5].Lines[2];
      }
    }

    public IEnumerable<Vector3> Vertices
    {
      get
      {
        yield return this.Sides[2].Lines[1].From;
        yield return this.Sides[2].Lines[1].To;
        yield return this.Sides[0].Lines[1].From;
        yield return this.Sides[0].Lines[1].To;
        yield return this.Sides[1].Lines[2].From;
        yield return this.Sides[1].Lines[2].To;
        yield return this.Sides[3].Lines[2].From;
        yield return this.Sides[3].Lines[2].To;
      }
    }

    public MyCuboid()
    {
      this.Sides[0] = new MyCuboidSide();
      this.Sides[1] = new MyCuboidSide();
      this.Sides[2] = new MyCuboidSide();
      this.Sides[3] = new MyCuboidSide();
      this.Sides[4] = new MyCuboidSide();
      this.Sides[5] = new MyCuboidSide();
    }

    public void CreateFromVertices(Vector3[] vertices)
    {
      Vector3 vector3_1 = new Vector3(float.MaxValue);
      Vector3 vector3_2 = new Vector3(float.MinValue);
      foreach (Vector3 vector3_3 in vertices)
      {
        vector3_1 = Vector3.Min(vector3_3, vector3_1);
        vector3_2 = Vector3.Min(vector3_3, vector3_2);
      }
      Line line1 = new Line(vertices[0], vertices[2], false);
      Line line2 = new Line(vertices[2], vertices[3], false);
      Line line3 = new Line(vertices[3], vertices[1], false);
      Line line4 = new Line(vertices[1], vertices[0], false);
      Line line5 = new Line(vertices[7], vertices[6], false);
      Line line6 = new Line(vertices[6], vertices[4], false);
      Line line7 = new Line(vertices[4], vertices[5], false);
      Line line8 = new Line(vertices[5], vertices[7], false);
      Line line9 = new Line(vertices[4], vertices[0], false);
      Line line10 = new Line(vertices[0], vertices[1], false);
      Line line11 = new Line(vertices[1], vertices[5], false);
      Line line12 = new Line(vertices[5], vertices[4], false);
      Line line13 = new Line(vertices[3], vertices[2], false);
      Line line14 = new Line(vertices[2], vertices[6], false);
      Line line15 = new Line(vertices[6], vertices[7], false);
      Line line16 = new Line(vertices[7], vertices[3], false);
      Line line17 = new Line(vertices[1], vertices[3], false);
      Line line18 = new Line(vertices[3], vertices[7], false);
      Line line19 = new Line(vertices[7], vertices[5], false);
      Line line20 = new Line(vertices[5], vertices[1], false);
      Line line21 = new Line(vertices[0], vertices[4], false);
      Line line22 = new Line(vertices[4], vertices[6], false);
      Line line23 = new Line(vertices[6], vertices[2], false);
      Line line24 = new Line(vertices[2], vertices[0], false);
      this.Sides[0].Lines[0] = line1;
      this.Sides[0].Lines[1] = line2;
      this.Sides[0].Lines[2] = line3;
      this.Sides[0].Lines[3] = line4;
      this.Sides[0].CreatePlaneFromLines();
      this.Sides[1].Lines[0] = line5;
      this.Sides[1].Lines[1] = line6;
      this.Sides[1].Lines[2] = line7;
      this.Sides[1].Lines[3] = line8;
      this.Sides[1].CreatePlaneFromLines();
      this.Sides[2].Lines[0] = line9;
      this.Sides[2].Lines[1] = line10;
      this.Sides[2].Lines[2] = line11;
      this.Sides[2].Lines[3] = line12;
      this.Sides[2].CreatePlaneFromLines();
      this.Sides[3].Lines[0] = line13;
      this.Sides[3].Lines[1] = line14;
      this.Sides[3].Lines[2] = line15;
      this.Sides[3].Lines[3] = line16;
      this.Sides[3].CreatePlaneFromLines();
      this.Sides[4].Lines[0] = line17;
      this.Sides[4].Lines[1] = line18;
      this.Sides[4].Lines[2] = line19;
      this.Sides[4].Lines[3] = line20;
      this.Sides[4].CreatePlaneFromLines();
      this.Sides[5].Lines[0] = line21;
      this.Sides[5].Lines[1] = line22;
      this.Sides[5].Lines[2] = line23;
      this.Sides[5].Lines[3] = line24;
      this.Sides[5].CreatePlaneFromLines();
    }

    public void CreateFromSizes(float width1, float depth1, float width2, float depth2, float length)
    {
      float y = length * 0.5f;
      float x1 = width1 * 0.5f;
      float x2 = width2 * 0.5f;
      float z1 = depth1 * 0.5f;
      float z2 = depth2 * 0.5f;
      this.CreateFromVertices(new Vector3[8]
      {
        new Vector3(-x2, -y, -z2),
        new Vector3(x2, -y, -z2),
        new Vector3(-x2, -y, z2),
        new Vector3(x2, -y, z2),
        new Vector3(-x1, y, -z1),
        new Vector3(x1, y, -z1),
        new Vector3(-x1, y, z1),
        new Vector3(x1, y, z1)
      });
    }

    public BoundingBox GetAABB()
    {
      BoundingBox boundingBox = BoundingBox.CreateInvalid();
      foreach (Line line in this.UniqueLines)
      {
        Vector3 point1 = line.From;
        Vector3 point2 = line.To;
        boundingBox = boundingBox.Include(ref point1);
        boundingBox = boundingBox.Include(ref point2);
      }
      return boundingBox;
    }

    public BoundingBox GetLocalAABB()
    {
      BoundingBox aabb = this.GetAABB();
      Vector3 center = aabb.Center;
      aabb.Min -= center;
      aabb.Max -= center;
      return aabb;
    }

    public MyCuboid CreateTransformed(ref Matrix worldMatrix)
    {
      Vector3[] vertices = new Vector3[8];
      int index = 0;
      foreach (Vector3 position in this.Vertices)
      {
        vertices[index] = Vector3.Transform(position, worldMatrix);
        ++index;
      }
      MyCuboid myCuboid = new MyCuboid();
      myCuboid.CreateFromVertices(vertices);
      return myCuboid;
    }
  }
}
