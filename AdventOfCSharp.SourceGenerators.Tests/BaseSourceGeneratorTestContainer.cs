﻿using AdventOfCSharp.SourceGenerators.Tests.Verifiers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdventOfCSharp.SourceGenerators.Tests;

using VerifyCS = CSharpSourceGeneratorVerifier<BenchmarkSourceGenerator>;

public abstract class BaseSourceGeneratorTestContainer<TSourceGenerator>
    where TSourceGenerator : class, ISourceGenerator
{
    protected abstract TSourceGenerator InitializeGeneratorInstance();

    // TODO: Consider removing
    protected Compilation CreateCompilation(string source, out GeneratorDriver resultingGeneratorDriver)
    {
        var compilation = CSharpCompilation.Create(null, new[] { CSharpSyntaxTree.ParseText(source) });
        var generator = InitializeGeneratorInstance();
        var driver = CSharpGeneratorDriver.Create(generator);
        resultingGeneratorDriver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var resultCompilation, out _);
        return resultCompilation;
    }

    protected async Task VerifyAsync(string source, string generatedFileName, string generatedSource)
    {
        var mappings = new GeneratedSourceMappings()
        {
            { generatedFileName, generatedSource }
        };
        await VerifyAsync(source, mappings);
    }
    protected async Task VerifyAsync(string source, GeneratedSourceMappings mappings)
    {
        await VerifyAsync(new[] { source }, mappings);
    }
    protected async Task VerifyAsync(IEnumerable<string> sources, GeneratedSourceMappings mappings)
    {
        await VerifyAsync(sources, mappings, new VerifyCS.Test());
    }
    protected async Task VerifyAsync(IEnumerable<string> sources, GeneratedSourceMappings mappings, VerifyCS.Test test)
    {
        test.TestState.Sources.AddRange(sources);
        foreach (var mapping in mappings)
        {
            test.TestState.GeneratedSources.Add((typeof(TSourceGenerator), mapping.Key, mapping.Value));
        }

        await test.RunAsync();
    }

    protected sealed class GeneratedSourceMappings : SortedDictionary<string, string> { }
}
