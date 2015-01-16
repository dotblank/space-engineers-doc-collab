// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyPositionComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using System;
using VRage.Common.Utils;
using VRageMath;

namespace Sandbox.Common.Components
{
    public abstract class MyPositionComponentBase : MyComponentBase
    {
        private static BoundingBoxD m_invalidBox = BoundingBoxD.CreateInvalid();
        protected MatrixD m_worldMatrix = MatrixD.Identity;
        protected MatrixD m_previousParentWorldMatrix = MatrixD.Identity;
        protected MatrixD m_localMatrix = MatrixD.Identity;
        protected bool m_normalizedInvMatrixDirty = true;
        protected bool m_invScaledMatrixDirty = true;
        protected BoundingBox m_localAABB;
        protected BoundingSphere m_localVolume;
        protected Vector3 m_localVolumeOffset;
        protected BoundingBoxD m_worldAABB;
        protected BoundingSphereD m_worldVolume;
        private BoundingBoxD m_worldAABBHr;
        private BoundingSphereD m_worldVolumeHr;
        protected bool m_localMatrixChanged;
        private MatrixD m_normalizedWorldMatrixInv;
        private MatrixD m_worldMatrixInvScaled;
        private float? m_scale;

        public MatrixD WorldMatrix
        {
            get { return this.m_worldMatrix; }
            set { this.SetWorldMatrix(value, (object) null); }
        }

        public Matrix LocalMatrix
        {
            get { return (Matrix) this.m_localMatrix; }
            set { this.SetLocalMatrix((MatrixD) value, (object) null); }
        }

        public BoundingBoxD WorldAABB
        {
            get { return this.m_worldAABB; }
            set { this.m_worldAABB = value; }
        }

        public BoundingSphereD WorldVolume
        {
            get { return this.m_worldVolume; }
            set { this.m_worldVolume = value; }
        }

        public BoundingBoxD WorldAABBHr
        {
            get { return this.m_worldAABBHr; }
        }

        public BoundingSphereD WorldVolumeHr
        {
            get { return this.m_worldVolumeHr; }
        }

        public virtual BoundingBox LocalAABB
        {
            get { return this.m_localAABB; }
            set
            {
                this.m_localAABB = value;
                this.m_localVolume = BoundingSphere.CreateFromBoundingBox(this.m_localAABB);
                this.UpdateWorldVolume();
            }
        }

        public virtual BoundingBox LocalAABBHr
        {
            get { return this.m_localAABB; }
        }

        public BoundingSphere LocalVolume
        {
            get { return this.m_localVolume; }
            set
            {
                this.m_localVolume = value;
                this.m_localAABB = MyMath.CreateFromInsideRadius(value.Radius);
                this.m_localAABB = this.m_localAABB.Translate(value.Center);
                this.UpdateWorldVolume();
            }
        }

        public Vector3 LocalVolumeOffset
        {
            get { return this.m_localVolumeOffset; }
            set
            {
                this.m_localVolumeOffset = value;
                this.UpdateWorldVolume();
            }
        }

        protected virtual bool ShouldSync
        {
            get { return this.CurrentContainer.Get<MySyncComponentBase>() != null; }
        }

        public MatrixD WorldMatrixNormalizedInv
        {
            get
            {
                if (this.m_normalizedInvMatrixDirty)
                {
                    if (!MyVRageUtils.IsZero(this.m_worldMatrix.Left.LengthSquared() - 1.0, 1E-05f))
                    {
                        MatrixD matrix = MatrixD.Normalize(this.m_worldMatrix);
                        MatrixD.Invert(ref matrix, out this.m_normalizedWorldMatrixInv);
                    }
                    else
                        MatrixD.Invert(ref this.m_worldMatrix, out this.m_normalizedWorldMatrixInv);
                    this.m_normalizedInvMatrixDirty = false;
                }
                return this.m_normalizedWorldMatrixInv;
            }
            private set { this.m_normalizedWorldMatrixInv = value; }
        }

        public MatrixD WorldMatrixInvScaled
        {
            get
            {
                if (this.m_invScaledMatrixDirty)
                {
                    MatrixD matrix = this.m_worldMatrix;
                    if (!MyVRageUtils.IsZero(this.m_worldMatrix.Left.LengthSquared() - 1.0, 1E-05f))
                        matrix = MatrixD.Normalize(this.m_worldMatrix);
                    if (this.Scale.HasValue)
                        matrix *= Matrix.CreateScale(this.Scale.Value);
                    MatrixD.Invert(ref matrix, out this.m_worldMatrixInvScaled);
                    this.m_invScaledMatrixDirty = false;
                }
                return this.m_worldMatrixInvScaled;
            }
            private set { this.m_worldMatrixInvScaled = value; }
        }

        public float? Scale
        {
            get { return this.m_scale; }
            set
            {
                float? nullable1 = this.m_scale;
                float? nullable2 = value;
                if (((double) nullable1.GetValueOrDefault() != (double) nullable2.GetValueOrDefault()
                    ? 1
                    : (nullable1.HasValue != nullable2.HasValue ? 1 : 0)) == 0)
                    return;
                this.m_scale = value;
                Matrix normalized1 = this.LocalMatrix;
                if (this.m_scale.HasValue)
                {
                    MatrixD normalized2 = this.WorldMatrix;
                    if (this.Entity.Parent == null)
                    {
                        MyVRageUtils.Normalize(ref normalized2, out normalized2);
                        this.WorldMatrix = Matrix.CreateScale(this.m_scale.Value)*normalized2;
                    }
                    else
                    {
                        MyVRageUtils.Normalize(ref normalized1, out normalized1);
                        this.LocalMatrix = Matrix.CreateScale(this.m_scale.Value)*normalized1;
                    }
                }
                else
                {
                    MyVRageUtils.Normalize(ref normalized1, out normalized1);
                    this.LocalMatrix = normalized1;
                }
                this.UpdateWorldMatrix((object) null);
            }
        }

        public event Action<MyPositionComponentBase> OnPositionChanged;

        protected void RaiseOnPositionChanged(MyPositionComponentBase component)
        {
            Action<MyPositionComponentBase> action = this.OnPositionChanged;
            if (action == null)
                return;
            action(component);
        }

        public virtual void SetWorldMatrix(MatrixD worldMatrix, object source = null)
        {
            if (this.Scale.HasValue)
            {
                MyVRageUtils.Normalize(ref worldMatrix, out worldMatrix);
                worldMatrix = MatrixD.CreateScale((double) this.Scale.Value)*worldMatrix;
            }
            MatrixD other;
            if (this.CurrentContainer.Entity.Parent == null)
            {
                this.m_worldMatrix = worldMatrix;
                other = worldMatrix;
            }
            else
            {
                MatrixD matrixD = MatrixD.Invert(this.CurrentContainer.Entity.Parent.WorldMatrix);
                other = worldMatrix*matrixD;
            }
            if (this.m_localMatrix.EqualsFast(ref other, 0.0001))
                return;
            this.m_localMatrixChanged = true;
            this.m_localMatrix = other;
            this.UpdateWorldMatrix(source);
        }

        public void SetLocalMatrix(MatrixD localMatrix, object source = null)
        {
            if (!(this.m_localMatrix != localMatrix))
                return;
            this.m_localMatrixChanged = true;
            this.m_localMatrix = localMatrix;
            this.UpdateWorldMatrix(source);
        }

        public Vector3D GetPosition()
        {
            return this.m_worldMatrix.Translation;
        }

        public void SetPosition(Vector3D pos)
        {
            if (MyVRageUtils.IsZero(this.m_worldMatrix.Translation - pos, 1E-05f))
                return;
            this.m_worldMatrix.Translation = pos;
            this.UpdateWorldMatrix((object) null);
        }

        [Obsolete("Use WorldMatrixInverted property")]
        public MatrixD GetWorldMatrixNormalizedInv()
        {
            return this.WorldMatrixNormalizedInv;
        }

        public virtual MatrixD GetViewMatrix()
        {
            return this.WorldMatrixNormalizedInv;
        }

        public virtual void UpdateWorldMatrix(object source = null)
        {
            if (this.CurrentContainer.Entity.Parent != null)
            {
                MatrixD worldMatrix = this.CurrentContainer.Entity.Parent.WorldMatrix;
                this.UpdateWorldMatrix(ref worldMatrix, source);
            }
            else
            {
                this.OnWorldPositionChanged(source);
                this.m_normalizedInvMatrixDirty = true;
                this.m_invScaledMatrixDirty = true;
            }
        }

        public virtual void UpdateWorldMatrix(ref MatrixD parentWorldMatrix, object source = null)
        {
            MatrixD other = this.m_worldMatrix;
            MatrixD.Multiply(ref this.m_localMatrix, ref parentWorldMatrix, out this.m_worldMatrix);
            if (this.m_worldMatrix.EqualsFast(ref other, 0.0001))
                return;
            this.OnWorldPositionChanged(source);
            this.m_normalizedInvMatrixDirty = true;
            this.m_invScaledMatrixDirty = true;
        }

        public virtual void UpdateWorldVolume()
        {
            this.m_worldAABB = this.m_localAABB.Transform(ref this.m_worldMatrix);
            MatrixD result = MatrixD.CreateTranslation((Vector3D) this.m_localVolume.Center);
            MatrixD.Multiply(ref result, ref this.m_worldMatrix, out result);
            this.m_worldVolume = new BoundingSphereD(result.Translation, (double) this.m_localVolume.Radius);
        }

        private void UpdateAABBHr(ref BoundingBoxD volume)
        {
            this.UpdateWorldVolume();
            BoundingBoxD.CreateMerged(ref MyPositionComponentBase.m_invalidBox, ref this.m_worldAABB,
                out this.m_worldAABBHr);
            this.m_worldVolumeHr = BoundingSphereD.CreateFromBoundingBox(this.m_worldAABBHr);
            BoundingBoxD.CreateMerged(ref this.m_worldAABBHr, ref volume, out volume);
        }

        public void UpdateAABBHr()
        {
            this.UpdateAABBHr(ref MyPositionComponentBase.m_invalidBox);
        }

        public virtual void OnWorldPositionChanged(object source)
        {
            this.UpdateWorldVolume();
            this.RaiseOnPositionChanged(this);
        }
    }
}