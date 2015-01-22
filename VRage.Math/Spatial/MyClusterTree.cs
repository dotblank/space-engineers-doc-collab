// Decompiled with JetBrains decompiler
// Type: VRageMath.Spatial.MyClusterTree
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Collections.Generic;
using System.Linq;
using VRage.Collections;
using VRageMath;

namespace VRageMath.Spatial
{
  public class MyClusterTree
  {
    public static Vector3 IdealClusterSize = new Vector3(20000f);
    public static Vector3 MinimumDistanceFromBorder = MyClusterTree.IdealClusterSize / 10f;
    public static Vector3 MaximumForSplit = MyClusterTree.IdealClusterSize * 2f;
    private MyDynamicAABBTreeD m_clusterTree = new MyDynamicAABBTreeD(Vector3D.Zero, 1.0);
    private MyDynamicAABBTreeD m_staticTree = new MyDynamicAABBTreeD(Vector3D.Zero, 1.0);
    private Dictionary<ulong, MyClusterTree.MyObjectData> m_objectsData = new Dictionary<ulong, MyClusterTree.MyObjectData>();
    private List<MyClusterTree.MyCluster> m_clusters = new List<MyClusterTree.MyCluster>();
    private List<MyClusterTree.MyCluster> m_returnedClusters = new List<MyClusterTree.MyCluster>(1);
    private List<object> m_userObjects = new List<object>();
    private List<MyLineSegmentOverlapResult<MyClusterTree.MyCluster>> m_lineResultList = new List<MyLineSegmentOverlapResult<MyClusterTree.MyCluster>>();
    private List<MyClusterTree.MyCluster> m_resultList = new List<MyClusterTree.MyCluster>();
    private List<ulong> m_objectDataResultList = new List<ulong>();
    public const ulong CLUSTERED_OBJECT_ID_UNITIALIZED = 18446744073709551615UL;
    public Func<int, BoundingBoxD, object> OnClusterCreated;
    public Action<object> OnClusterRemoved;
    public Action<object> OnFinishBatch;
    public readonly BoundingBoxD? SingleCluster;
    private ulong m_clusterObjectCounter;

    public MyClusterTree(BoundingBoxD? singleCluster)
    {
      this.SingleCluster = singleCluster;
    }

    public ulong AddObject(BoundingBoxD bbox, Vector3 velocity, MyClusterTree.IMyActivationHandler activationHandler, ulong? customId)
    {
      if (this.SingleCluster.HasValue && this.m_clusters.Count == 0)
      {
        BoundingBoxD clusterBB = this.SingleCluster.Value;
        this.CreateCluster(ref clusterBB);
      }
      BoundingBoxD bbox1 = !this.SingleCluster.HasValue ? bbox.GetInflated(MyClusterTree.MinimumDistanceFromBorder) : bbox;
      this.m_clusterTree.OverlapAllBoundingBox<MyClusterTree.MyCluster>(ref bbox1, this.m_returnedClusters, 0U, true);
      MyClusterTree.MyCluster cluster = (MyClusterTree.MyCluster) null;
      bool flag = false;
      if (this.m_returnedClusters.Count == 1)
      {
        if (this.m_returnedClusters[0].AABB.Contains(bbox1) == ContainmentType.Contains)
          cluster = this.m_returnedClusters[0];
        else if (this.m_returnedClusters[0].AABB.Contains(bbox1) == ContainmentType.Intersects && activationHandler.IsStaticForCluster)
        {
          if (this.m_returnedClusters[0].AABB.Contains(bbox) != ContainmentType.Disjoint)
            cluster = this.m_returnedClusters[0];
        }
        else
          flag = true;
      }
      else if (this.m_returnedClusters.Count > 1)
        flag = true;
      else if (this.m_returnedClusters.Count == 0 && !activationHandler.IsStaticForCluster)
      {
        BoundingBoxD boundingBoxD = new BoundingBoxD(bbox.Center - MyClusterTree.IdealClusterSize / 2f, bbox.Center + MyClusterTree.IdealClusterSize / 2f);
        this.m_clusterTree.OverlapAllBoundingBox<MyClusterTree.MyCluster>(ref boundingBoxD, this.m_returnedClusters, 0U, true);
        if (this.m_returnedClusters.Count == 0)
        {
          this.m_staticTree.OverlapAllBoundingBox<ulong>(ref boundingBoxD, this.m_objectDataResultList, 0U, true);
          cluster = this.CreateCluster(ref boundingBoxD);
          foreach (ulong objectId in this.m_objectDataResultList)
          {
            long num = (long) this.AddObjectToCluster(cluster, objectId, false);
          }
        }
        else
          flag = true;
      }
      ulong objectId1 = customId.HasValue ? customId.Value : this.m_clusterObjectCounter++;
      int num1 = -1;
      this.m_objectsData[objectId1] = new MyClusterTree.MyObjectData()
      {
        Id = objectId1,
        Cluster = cluster,
        ActivationHandler = activationHandler,
        AABB = bbox,
        StaticId = num1
      };
      if (flag && !this.SingleCluster.HasValue)
      {
        this.ReorderClusters(bbox, objectId1);
        int num2 = this.m_objectsData[objectId1].ActivationHandler.IsStaticForCluster ? 1 : 0;
      }
      if (activationHandler.IsStaticForCluster)
      {
        int num2 = this.m_staticTree.AddProxy(ref bbox, (object) objectId1, 0U, true);
        this.m_objectsData[objectId1].StaticId = num2;
      }
      if (cluster != null)
        return this.AddObjectToCluster(cluster, objectId1, false);
      else
        return objectId1;
    }

    private ulong AddObjectToCluster(MyClusterTree.MyCluster cluster, ulong objectId, bool batch)
    {
      cluster.Objects.Add(objectId);
      MyClusterTree.MyObjectData myObjectData = this.m_objectsData[objectId];
      this.m_objectsData[objectId].Id = objectId;
      this.m_objectsData[objectId].Cluster = cluster;
      if (batch)
      {
        if (myObjectData.ActivationHandler != null)
          myObjectData.ActivationHandler.ActivateBatch(cluster.UserData, objectId);
      }
      else if (myObjectData.ActivationHandler != null)
        myObjectData.ActivationHandler.Activate(cluster.UserData, objectId);
      return objectId;
    }

    private MyClusterTree.MyCluster CreateCluster(ref BoundingBoxD clusterBB)
    {
      MyClusterTree.MyCluster myCluster = new MyClusterTree.MyCluster()
      {
        AABB = clusterBB,
        Objects = new HashSet<ulong>()
      };
      myCluster.ClusterId = this.m_clusterTree.AddProxy(ref myCluster.AABB, (object) myCluster, 0U, true);
      if (this.OnClusterCreated != null)
        myCluster.UserData = this.OnClusterCreated(myCluster.ClusterId, myCluster.AABB);
      this.m_clusters.Add(myCluster);
      this.m_userObjects.Add(myCluster.UserData);
      return myCluster;
    }

    public void MoveObject(ulong id, BoundingBoxD oldAabb, BoundingBoxD aabb, Vector3 velocity)
    {
      MyClusterTree.MyObjectData myObjectData;
      if (!this.m_objectsData.TryGetValue(id, out myObjectData))
        return;
      BoundingBoxD box1 = myObjectData.AABB;
      this.m_objectsData[id].AABB = aabb;
      BoundingBoxD boundingBoxD = aabb;
      Vector3 vector3 = Vector3.Normalize(velocity);
      BoundingBoxD box2 = aabb.Include(aabb.Center + vector3 * 2000f);
      if (myObjectData.Cluster.AABB.Contains(box2) == ContainmentType.Contains || this.SingleCluster.HasValue)
        return;
      this.ReorderClusters(boundingBoxD.Include(box1), id);
    }

    public void RemoveObject(ulong id)
    {
      MyClusterTree.MyObjectData objectData;
      if (!this.m_objectsData.TryGetValue(id, out objectData))
        return;
      MyClusterTree.MyCluster cluster = objectData.Cluster;
      if (cluster != null)
      {
        this.RemoveObjectFromCluster(objectData, false);
        if (cluster.Objects.Count == 0)
          this.RemoveCluster(cluster);
      }
      if (objectData.StaticId != -1)
      {
        this.m_staticTree.RemoveProxy(objectData.StaticId);
        objectData.StaticId = -1;
      }
      this.m_objectsData.Remove(id);
    }

    private void RemoveObjectFromCluster(MyClusterTree.MyObjectData objectData, bool batch)
    {
      objectData.Cluster.Objects.Remove(objectData.Id);
      if (batch)
      {
        if (objectData.ActivationHandler == null)
          return;
        objectData.ActivationHandler.DeactivateBatch(objectData.Cluster.UserData);
      }
      else
      {
        if (objectData.ActivationHandler != null)
          objectData.ActivationHandler.Deactivate(objectData.Cluster.UserData);
        this.m_objectsData[objectData.Id].Cluster = (MyClusterTree.MyCluster) null;
      }
    }

    private void RemoveCluster(MyClusterTree.MyCluster cluster)
    {
      this.m_clusterTree.RemoveProxy(cluster.ClusterId);
      this.m_clusters.Remove(cluster);
      this.m_userObjects.Remove(cluster.UserData);
      if (this.OnClusterRemoved == null)
        return;
      this.OnClusterRemoved(cluster.UserData);
    }

    public Vector3D GetObjectOffset(ulong id)
    {
      MyClusterTree.MyObjectData myObjectData;
      if (this.m_objectsData.TryGetValue(id, out myObjectData) && myObjectData.Cluster != null)
        return myObjectData.Cluster.AABB.Center;
      else
        return Vector3D.Zero;
    }

    public void Dispose()
    {
      foreach (MyClusterTree.MyCluster myCluster in this.m_clusters)
      {
        if (this.OnClusterRemoved != null)
          this.OnClusterRemoved(myCluster.UserData);
      }
      this.m_clusters.Clear();
      this.m_userObjects.Clear();
      this.m_clusterTree.Clear();
      this.m_objectsData.Clear();
      this.m_staticTree.Clear();
      this.m_clusterObjectCounter = 0UL;
    }

    public ListReader<object> GetList()
    {
      return new ListReader<object>(this.m_userObjects);
    }

    public void CastRay(Vector3D from, Vector3D to, List<MyClusterTree.MyClusterQueryResult> results)
    {
      LineD line = new LineD(from, to);
      this.m_clusterTree.OverlapAllLineSegment<MyClusterTree.MyCluster>(ref line, this.m_lineResultList);
      foreach (MyLineSegmentOverlapResult<MyClusterTree.MyCluster> segmentOverlapResult in this.m_lineResultList)
        results.Add(new MyClusterTree.MyClusterQueryResult()
        {
          AABB = segmentOverlapResult.Element.AABB,
          UserData = segmentOverlapResult.Element.UserData
        });
    }

    public void Intersects(Vector3D translation, List<MyClusterTree.MyClusterQueryResult> results)
    {
      BoundingBoxD bbox = new BoundingBoxD(translation - new Vector3D(1.0), translation + new Vector3D(1.0));
      this.m_clusterTree.OverlapAllBoundingBox<MyClusterTree.MyCluster>(ref bbox, this.m_resultList, 0U, true);
      foreach (MyClusterTree.MyCluster myCluster in this.m_resultList)
        results.Add(new MyClusterTree.MyClusterQueryResult()
        {
          AABB = myCluster.AABB,
          UserData = myCluster.UserData
        });
    }

    public void GetAll(List<MyClusterTree.MyClusterQueryResult> results)
    {
      this.m_clusterTree.GetAll<MyClusterTree.MyCluster>(this.m_resultList, true, (List<BoundingBoxD>) null);
      foreach (MyClusterTree.MyCluster myCluster in this.m_resultList)
        results.Add(new MyClusterTree.MyClusterQueryResult()
        {
          AABB = myCluster.AABB,
          UserData = myCluster.UserData
        });
    }

    public void ReorderClusters(BoundingBoxD aabb, ulong objectId)
    {
      aabb.InflateToMinimum((Vector3D) MyClusterTree.IdealClusterSize);
      bool flag1 = false;
      BoundingBoxD bbox = aabb;
      this.m_clusterTree.OverlapAllBoundingBox<MyClusterTree.MyCluster>(ref bbox, this.m_resultList, 0U, true);
      HashSet<MyClusterTree.MyObjectData> hashSet1 = new HashSet<MyClusterTree.MyObjectData>();
      while (!flag1)
      {
        hashSet1.Clear();
        hashSet1.Add(this.m_objectsData[objectId]);
        foreach (MyClusterTree.MyCluster myCluster in this.m_resultList)
        {
          MyClusterTree.MyCluster collidedCluster = myCluster;
          bbox.Include(collidedCluster.AABB);
          foreach (MyClusterTree.MyObjectData myObjectData in Enumerable.Select<KeyValuePair<ulong, MyClusterTree.MyObjectData>, MyClusterTree.MyObjectData>(Enumerable.Where<KeyValuePair<ulong, MyClusterTree.MyObjectData>>((IEnumerable<KeyValuePair<ulong, MyClusterTree.MyObjectData>>) this.m_objectsData, (Func<KeyValuePair<ulong, MyClusterTree.MyObjectData>, bool>) (x => collidedCluster.Objects.Contains(x.Key))), (Func<KeyValuePair<ulong, MyClusterTree.MyObjectData>, MyClusterTree.MyObjectData>) (x => x.Value)))
            hashSet1.Add(myObjectData);
        }
        int count = this.m_resultList.Count;
        this.m_clusterTree.OverlapAllBoundingBox<MyClusterTree.MyCluster>(ref bbox, this.m_resultList, 0U, true);
        flag1 = count == this.m_resultList.Count;
        this.m_staticTree.OverlapAllBoundingBox<ulong>(ref bbox, this.m_objectDataResultList, 0U, true);
        foreach (ulong index in this.m_objectDataResultList)
        {
          if (this.m_objectsData[index].Cluster != null && !this.m_resultList.Contains(this.m_objectsData[index].Cluster))
          {
            bbox.Include(this.m_objectsData[index].Cluster.AABB);
            flag1 = false;
          }
        }
      }
      this.m_staticTree.OverlapAllBoundingBox<ulong>(ref bbox, this.m_objectDataResultList, 0U, true);
      foreach (ulong index in this.m_objectDataResultList)
        hashSet1.Add(this.m_objectsData[index]);
      Stack<MyClusterTree.MyClusterDescription> stack = new Stack<MyClusterTree.MyClusterDescription>();
      List<MyClusterTree.MyClusterDescription> list1 = new List<MyClusterTree.MyClusterDescription>();
      MyClusterTree.MyClusterDescription clusterDescription1 = new MyClusterTree.MyClusterDescription()
      {
        AABB = bbox,
        DynamicObjects = Enumerable.ToList<MyClusterTree.MyObjectData>(Enumerable.Where<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) hashSet1, (Func<MyClusterTree.MyObjectData, bool>) (x => !x.ActivationHandler.IsStaticForCluster))),
        StaticObjects = Enumerable.ToList<MyClusterTree.MyObjectData>(Enumerable.Where<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) hashSet1, (Func<MyClusterTree.MyObjectData, bool>) (x => x.ActivationHandler.IsStaticForCluster)))
      };
      stack.Push(clusterDescription1);
      List<MyClusterTree.MyObjectData> list2 = Enumerable.ToList<MyClusterTree.MyObjectData>(Enumerable.Where<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) clusterDescription1.StaticObjects, (Func<MyClusterTree.MyObjectData, bool>) (x => x.Cluster != null)));
      int count1 = clusterDescription1.StaticObjects.Count;
      while (stack.Count > 0)
      {
        MyClusterTree.MyClusterDescription clusterDescription2 = stack.Pop();
        if (clusterDescription2.DynamicObjects.Count != 0)
        {
          BoundingBoxD invalid1 = BoundingBoxD.CreateInvalid();
          for (int index = 0; index < clusterDescription2.DynamicObjects.Count; ++index)
          {
            BoundingBoxD inflated = clusterDescription2.DynamicObjects[index].AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f);
            invalid1.Include(inflated);
          }
          BoundingBoxD boundingBoxD = invalid1;
          Vector3D vector3D = invalid1.Max;
          int i = invalid1.Size().AbsMaxComponent();
          switch (i)
          {
            case 0:
              clusterDescription2.DynamicObjects.Sort((IComparer<MyClusterTree.MyObjectData>) MyClusterTree.AABBComparerX.Static);
              break;
            case 1:
              clusterDescription2.DynamicObjects.Sort((IComparer<MyClusterTree.MyObjectData>) MyClusterTree.AABBComparerY.Static);
              break;
            case 2:
              clusterDescription2.DynamicObjects.Sort((IComparer<MyClusterTree.MyObjectData>) MyClusterTree.AABBComparerZ.Static);
              break;
          }
          bool flag2 = false;
          if (invalid1.Size().AbsMax() >= (double) MyClusterTree.MaximumForSplit.AbsMax())
          {
            for (int index = 1; index < clusterDescription2.DynamicObjects.Count; ++index)
            {
              MyClusterTree.MyObjectData myObjectData1 = clusterDescription2.DynamicObjects[index - 1];
              MyClusterTree.MyObjectData myObjectData2 = clusterDescription2.DynamicObjects[index];
              BoundingBoxD inflated = myObjectData1.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f);
              if (myObjectData2.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f).Min.GetDim(i) - inflated.Max.GetDim(i) > 0.0)
              {
                flag2 = true;
                vector3D.SetDim(i, inflated.Max.GetDim(i));
                break;
              }
            }
          }
          boundingBoxD.Max = vector3D;
          boundingBoxD.InflateToMinimum((Vector3D) MyClusterTree.IdealClusterSize);
          MyClusterTree.MyClusterDescription clusterDescription3 = new MyClusterTree.MyClusterDescription()
          {
            AABB = boundingBoxD,
            DynamicObjects = new List<MyClusterTree.MyObjectData>(),
            StaticObjects = new List<MyClusterTree.MyObjectData>()
          };
          foreach (MyClusterTree.MyObjectData myObjectData in Enumerable.ToList<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) clusterDescription2.DynamicObjects))
          {
            if (boundingBoxD.Contains(myObjectData.AABB) == ContainmentType.Contains)
            {
              clusterDescription3.DynamicObjects.Add(myObjectData);
              clusterDescription2.DynamicObjects.Remove(myObjectData);
            }
          }
          foreach (MyClusterTree.MyObjectData myObjectData in Enumerable.ToList<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) clusterDescription2.StaticObjects))
          {
            switch (boundingBoxD.Contains(myObjectData.AABB))
            {
              case ContainmentType.Contains:
              case ContainmentType.Intersects:
                clusterDescription3.StaticObjects.Add(myObjectData);
                clusterDescription2.StaticObjects.Remove(myObjectData);
                continue;
              default:
                continue;
            }
          }
          clusterDescription3.AABB = boundingBoxD;
          if (clusterDescription2.DynamicObjects.Count > 0)
          {
            BoundingBoxD invalid2 = BoundingBoxD.CreateInvalid();
            foreach (MyClusterTree.MyObjectData myObjectData in clusterDescription2.DynamicObjects)
              invalid2.Include(myObjectData.AABB.GetInflated(MyClusterTree.MinimumDistanceFromBorder));
            invalid2.InflateToMinimum((Vector3D) MyClusterTree.IdealClusterSize);
            MyClusterTree.MyClusterDescription clusterDescription4 = new MyClusterTree.MyClusterDescription()
            {
              AABB = invalid2,
              DynamicObjects = Enumerable.ToList<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) clusterDescription2.DynamicObjects),
              StaticObjects = Enumerable.ToList<MyClusterTree.MyObjectData>((IEnumerable<MyClusterTree.MyObjectData>) clusterDescription2.StaticObjects)
            };
            if (clusterDescription4.AABB.Size().AbsMax() > 2.0 * (double) MyClusterTree.IdealClusterSize.AbsMax())
              stack.Push(clusterDescription4);
            else
              list1.Add(clusterDescription4);
          }
          if (clusterDescription3.AABB.Size().AbsMax() > 2.0 * (double) MyClusterTree.IdealClusterSize.AbsMax() && flag2)
            stack.Push(clusterDescription3);
          else
            list1.Add(clusterDescription3);
        }
      }
      HashSet<MyClusterTree.MyCluster> hashSet2 = new HashSet<MyClusterTree.MyCluster>();
      HashSet<MyClusterTree.MyCluster> hashSet3 = new HashSet<MyClusterTree.MyCluster>();
      foreach (MyClusterTree.MyObjectData objectData in list2)
      {
        if (objectData.Cluster != null)
        {
          hashSet2.Add(objectData.Cluster);
          this.RemoveObjectFromCluster(objectData, true);
        }
      }
      foreach (MyClusterTree.MyObjectData myObjectData in list2)
      {
        if (myObjectData.Cluster != null)
        {
          myObjectData.ActivationHandler.FinishRemoveBatch(myObjectData.Cluster.UserData);
          myObjectData.Cluster = (MyClusterTree.MyCluster) null;
        }
      }
      int num1 = 0;
      foreach (MyClusterTree.MyClusterDescription clusterDescription2 in list1)
      {
        BoundingBoxD clusterBB = clusterDescription2.AABB;
        MyClusterTree.MyCluster cluster = this.CreateCluster(ref clusterBB);
        foreach (MyClusterTree.MyObjectData objectData in clusterDescription2.DynamicObjects)
        {
          if (objectData.Cluster != null)
          {
            hashSet2.Add(objectData.Cluster);
            this.RemoveObjectFromCluster(objectData, true);
          }
        }
        foreach (MyClusterTree.MyObjectData myObjectData in clusterDescription2.DynamicObjects)
        {
          if (myObjectData.Cluster != null)
          {
            myObjectData.ActivationHandler.FinishRemoveBatch(myObjectData.Cluster.UserData);
            myObjectData.Cluster = (MyClusterTree.MyCluster) null;
          }
        }
        foreach (MyClusterTree.MyObjectData myObjectData in clusterDescription2.DynamicObjects)
        {
          long num2 = (long) this.AddObjectToCluster(cluster, myObjectData.Id, true);
        }
        foreach (MyClusterTree.MyObjectData myObjectData in clusterDescription2.StaticObjects)
        {
          if (cluster.AABB.Contains(myObjectData.AABB) != ContainmentType.Disjoint)
          {
            long num2 = (long) this.AddObjectToCluster(cluster, myObjectData.Id, true);
            ++num1;
          }
        }
        hashSet3.Add(cluster);
      }
      foreach (MyClusterTree.MyCluster cluster in hashSet2)
      {
        if (this.OnFinishBatch != null)
          this.OnFinishBatch(cluster.UserData);
        this.RemoveCluster(cluster);
      }
      foreach (MyClusterTree.MyCluster myCluster in hashSet3)
      {
        if (this.OnFinishBatch != null)
          this.OnFinishBatch(myCluster.UserData);
        foreach (ulong index in myCluster.Objects)
          this.m_objectsData[index].ActivationHandler.FinishAddBatch();
      }
    }

    public void GetAllStaticObjects(List<BoundingBoxD> staticObjects)
    {
      this.m_staticTree.GetAll<ulong>(this.m_objectDataResultList, true, (List<BoundingBoxD>) null);
      staticObjects.Clear();
      foreach (ulong index in this.m_objectDataResultList)
        staticObjects.Add(this.m_objectsData[index].AABB);
    }

    public interface IMyActivationHandler
    {
      bool IsStaticForCluster { get; }

      void Activate(object userData, ulong clusterObjectID);

      void Deactivate(object userData);

      void ActivateBatch(object userData, ulong clusterObjectID);

      void DeactivateBatch(object userData);

      void FinishAddBatch();

      void FinishRemoveBatch(object userData);
    }

    private class MyCluster
    {
      public int ClusterId;
      public BoundingBoxD AABB;
      public HashSet<ulong> Objects;
      public object UserData;

      public override string ToString()
      {
        return string.Concat(new object[4]
        {
          (object) "MyCluster",
          (object) this.ClusterId,
          (object) ": ",
          (object) this.AABB.Center
        });
      }
    }

    private class MyObjectData
    {
      public ulong Id;
      public MyClusterTree.MyCluster Cluster;
      public MyClusterTree.IMyActivationHandler ActivationHandler;
      public BoundingBoxD AABB;
      public int StaticId;
    }

    public struct MyClusterQueryResult
    {
      public BoundingBoxD AABB;
      public object UserData;
    }

    private class AABBComparerX : IComparer<MyClusterTree.MyObjectData>
    {
      public static MyClusterTree.AABBComparerX Static = new MyClusterTree.AABBComparerX();

      public int Compare(MyClusterTree.MyObjectData x, MyClusterTree.MyObjectData y)
      {
        return x.AABB.Min.X.CompareTo(y.AABB.Min.X);
      }
    }

    private class AABBComparerY : IComparer<MyClusterTree.MyObjectData>
    {
      public static MyClusterTree.AABBComparerY Static = new MyClusterTree.AABBComparerY();

      public int Compare(MyClusterTree.MyObjectData x, MyClusterTree.MyObjectData y)
      {
        return x.AABB.Min.Y.CompareTo(y.AABB.Min.Y);
      }
    }

    private class AABBComparerZ : IComparer<MyClusterTree.MyObjectData>
    {
      public static MyClusterTree.AABBComparerZ Static = new MyClusterTree.AABBComparerZ();

      public int Compare(MyClusterTree.MyObjectData x, MyClusterTree.MyObjectData y)
      {
        return x.AABB.Min.Z.CompareTo(y.AABB.Min.Z);
      }
    }

    private struct MyClusterDescription
    {
      public BoundingBoxD AABB;
      public List<MyClusterTree.MyObjectData> DynamicObjects;
      public List<MyClusterTree.MyObjectData> StaticObjects;
    }
  }
}
