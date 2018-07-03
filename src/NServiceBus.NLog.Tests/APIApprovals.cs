﻿using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ApiApprover;
using ApprovalTests;
using Mono.Cecil;
using NServiceBus_6;
using NUnit.Framework;

[TestFixture]
public class APIApprovals
{
    [Test]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Approve()
    {
        Directory.SetCurrentDirectory(TestContext.CurrentContext.TestDirectory);
        var assemblyPath = Path.GetFullPath(typeof(NLogFactory).Assembly.Location);
        var asm = AssemblyDefinition.ReadAssembly(assemblyPath);
        var publicApi = Filter(PublicApiGenerator.CreatePublicApiForAssembly(asm));
        Approvals.Verify(publicApi);
    }

    string Filter(string text)
    {
        return string.Join(Environment.NewLine, text.Split(new[]
        {
            Environment.NewLine
        }, StringSplitOptions.RemoveEmptyEntries)
            .Where(l => !l.StartsWith("[assembly: ReleaseDateAttribute("))
            .Where(l => !string.IsNullOrWhiteSpace(l))
            );
    }

}