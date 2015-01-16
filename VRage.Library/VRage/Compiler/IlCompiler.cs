// Decompiled with JetBrains decompiler
// Type: VRage.Compiler.IlCompiler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VRage.Compiler
{
    public class IlCompiler
    {
        private static CSharpCodeProvider m_cp = new CSharpCodeProvider();
        private static IlReader m_reader = new IlReader();
        private static StringBuilder m_cache = new StringBuilder();
        public static StringBuilder Buffer = new StringBuilder();

        public static CompilerParameters Options = new CompilerParameters(new string[11]
        {
            "System.Xml.dll",
            "Sandbox.Game.dll",
            "Sandbox.Common.dll",
            "Sandbox.Audio.dll",
            "Sandbox.Graphics.dll",
            "Sandbox.Input.dll",
            "VRage.Common.dll",
            "VRage.Library.dll",
            "VRage.Math.dll",
            "System.Core.dll",
            "System.dll"
        });

        private const string invokeWrapper =
            "public static class wrapclass{{ public static object run() {{ {0} return null;}} }}";

        static IlCompiler()
        {
            IlCompiler.Options.GenerateInMemory = true;
        }

        public static bool CompileFile(string assemblyName, string[] files, out Assembly assembly, List<string> errors)
        {
            assembly = (Assembly) null;
            IlCompiler.Options.OutputAssembly = assemblyName;
            IlCompiler.Options.GenerateInMemory = true;
            CompilerResults result = IlCompiler.m_cp.CompileAssemblyFromFile(IlCompiler.Options, files);
            return IlCompiler.CheckResultInternal(ref assembly, errors, result, false);
        }

        public static bool CompileString(string assemblyName, string[] source, out Assembly assembly,
            List<string> errors)
        {
            assembly = (Assembly) null;
            IlCompiler.Options.OutputAssembly = assemblyName;
            IlCompiler.Options.GenerateInMemory = true;
            CompilerResults result = IlCompiler.m_cp.CompileAssemblyFromSource(IlCompiler.Options, source);
            return IlCompiler.CheckResultInternal(ref assembly, errors, result, true);
        }

        private static bool CheckResultInternal(ref Assembly assembly, List<string> errors, CompilerResults result,
            bool isIngameScript)
        {
            if (result.Errors.HasErrors)
            {
                IEnumerator enumerator = result.Errors.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (!(enumerator.Current as CompilerError).IsWarning)
                        errors.Add((enumerator.Current as CompilerError).ToString());
                }
                return false;
            }
            else
            {
                assembly = result.CompiledAssembly;
                Dictionary<Type, List<MemberInfo>> allowedTypes = new Dictionary<Type, List<MemberInfo>>();
                foreach (Type key in assembly.GetTypes())
                    allowedTypes.Add(key, (List<MemberInfo>) null);
                List<MethodBase> list = new List<MethodBase>();
                BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                           BindingFlags.NonPublic;
                foreach (Type classType in assembly.GetTypes())
                {
                    list.Clear();
                    ListExtensions.AddArray<MethodBase>(list, (MethodBase[]) classType.GetMethods(bindingAttr));
                    ListExtensions.AddArray<MethodBase>(list, (MethodBase[]) classType.GetConstructors(bindingAttr));
                    foreach (MethodBase method in list)
                    {
                        if (IlChecker.IsMethodFromParent(classType, method))
                        {
                            if (!IlChecker.CheckTypeAndMember(method.DeclaringType, isIngameScript, (MemberInfo) null))
                            {
                                errors.Add(
                                    string.Format("Class {0} derives from class {1} that is not allowed in script",
                                        (object) classType.Name, (object) method.DeclaringType.Name));
                                return false;
                            }
                        }
                        else
                        {
                            Type failed;
                            if (
                                !IlChecker.CheckIl(IlCompiler.m_reader.ReadInstructions(method), out failed,
                                    isIngameScript, allowedTypes) ||
                                IlChecker.HasMethodInvalidAtrributes(method.Attributes))
                            {
                                errors.Add(string.Format("Type {0} used in {1} not allowed in script",
                                    failed == (Type) null ? (object) "FIXME" : (object) failed.FullName,
                                    (object) method.Name));
                                assembly = (Assembly) null;
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
        }

        public static bool Compile(string assemblyName, string[] fileContents, out Assembly assembly,
            List<string> errors, bool isIngameScript)
        {
            assembly = (Assembly) null;
            IlCompiler.Options.OutputAssembly = assemblyName;
            CompilerResults result = IlCompiler.m_cp.CompileAssemblyFromSource(IlCompiler.Options, fileContents);
            return IlCompiler.CheckResultInternal(ref assembly, errors, result, isIngameScript);
        }

        public static bool Compile(string[] instructions, out Assembly assembly, bool isIngameScript, bool wrap = true)
        {
            assembly = (Assembly) null;
            IlCompiler.m_cache.Clear();
            if (wrap)
                IlCompiler.m_cache.AppendFormat(
                    "public static class wrapclass{{ public static object run() {{ {0} return null;}} }}",
                    (object[]) instructions);
            else
                IlCompiler.m_cache.Append(instructions[0]);
            CompilerResults compilerResults = IlCompiler.m_cp.CompileAssemblyFromSource(IlCompiler.Options,
                ((object) IlCompiler.m_cache).ToString());
            if (compilerResults.Errors.HasErrors)
                return false;
            assembly = compilerResults.CompiledAssembly;
            Dictionary<Type, List<MemberInfo>> allowedTypes = new Dictionary<Type, List<MemberInfo>>();
            foreach (Type key in assembly.GetTypes())
                allowedTypes.Add(key, (List<MemberInfo>) null);
            foreach (Type type in assembly.GetTypes())
            {
                foreach (MethodInfo methodInfo in type.GetMethods())
                {
                    Type failed;
                    if (!(type == typeof (MulticastDelegate)) &&
                        !IlChecker.CheckIl(IlCompiler.m_reader.ReadInstructions((MethodBase) methodInfo), out failed,
                            isIngameScript, allowedTypes))
                    {
                        assembly = (Assembly) null;
                        return false;
                    }
                }
            }
            return true;
        }
    }
}