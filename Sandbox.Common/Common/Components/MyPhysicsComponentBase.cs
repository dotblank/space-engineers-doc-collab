// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyPhysicsComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Engine.Physics;
using Sandbox.ModAPI;
using VRageMath;

namespace Sandbox.Common.Components
{
    public abstract class MyPhysicsComponentBase : MyComponentBase
    {
        public ushort ContactPointDelay = ushort.MaxValue;
        public static bool DebugDrawFlattenHierarchy;
        protected RigidBodyFlag Flags;
        public bool IsPhantom;
        private Vector3 m_lastLinearVelocity;
        private Vector3 m_lastAngularVelocity;
        protected bool m_enabled;

        public bool ReportAllContacts
        {
            get { return (int) this.ContactPointDelay == 0; }
            set { this.ContactPointDelay = value ? (ushort) 0 : ushort.MaxValue; }
        }

        public new IMyEntity Entity { get; set; }

        public bool CanUpdateAccelerations { get; set; }

        public MyMaterialType MaterialType { get; set; }

        public virtual bool IsStatic
        {
            get { return (this.Flags & RigidBodyFlag.RBF_STATIC) == RigidBodyFlag.RBF_STATIC; }
        }

        public bool IsKinematic
        {
            get
            {
                if ((this.Flags & RigidBodyFlag.RBF_KINEMATIC) != RigidBodyFlag.RBF_KINEMATIC)
                    return (this.Flags & RigidBodyFlag.RBF_DOUBLED_KINEMATIC) == RigidBodyFlag.RBF_DOUBLED_KINEMATIC;
                else
                    return true;
            }
        }

        public virtual bool Enabled
        {
            get { return this.m_enabled; }
            set
            {
                if (this.m_enabled == value)
                    return;
                this.m_enabled = value;
                if (value)
                {
                    if (!this.Entity.InScene)
                        return;
                    this.Activate();
                }
                else
                    this.Deactivate();
            }
        }

        public bool PlayCollisionCueEnabled { get; set; }

        public abstract float Mass { get; }

        public Vector3 Center { get; set; }

        public abstract Vector3 LinearVelocity { get; set; }

        public virtual Vector3 LinearAcceleration { get; protected set; }

        public virtual Vector3 AngularAcceleration { get; protected set; }

        public abstract float LinearDamping { get; set; }

        public abstract float AngularDamping { get; set; }

        public abstract Vector3 AngularVelocity { get; set; }

        public abstract float Speed { get; }

        public abstract float Friction { get; set; }

        public abstract bool HasRigidBody { get; }

        public abstract Vector3D CenterOfMassWorld { get; }

        public virtual bool IsInWorld { get; protected set; }

        public virtual void Close()
        {
            this.Deactivate();
            this.CloseRigidBody();
        }

        protected abstract void CloseRigidBody();

        public abstract void AddForce(MyPhysicsForceType type, Vector3? force, Vector3D? position, Vector3? torque);

        public abstract void ApplyImpulse(Vector3 dir, Vector3D pos);

        public abstract void ClearSpeed();

        public abstract void Clear();

        public abstract void CreateCharacterCollision(Vector3 center, float characterWidth, float characterHeight,
            float crouchHeight, float ladderHeight, float headSize, MatrixD worldTransform, float mass,
            ushort collisionLayer);

        public abstract void DebugDraw();

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void ForceActivate();

        public virtual void UpdateAccelerations()
        {
            Vector3 vector3_1 = this.LinearVelocity - this.m_lastLinearVelocity;
            this.m_lastLinearVelocity = this.LinearVelocity;
            this.LinearAcceleration = vector3_1/0.01666667f;
            Vector3 vector3_2 = this.AngularVelocity - this.m_lastAngularVelocity;
            this.m_lastAngularVelocity = this.AngularVelocity;
            this.AngularAcceleration = vector3_2/0.01666667f;
        }

        public abstract MatrixD GetWorldMatrix();

        public abstract void OnWorldPositionChanged(object source);
    }
}