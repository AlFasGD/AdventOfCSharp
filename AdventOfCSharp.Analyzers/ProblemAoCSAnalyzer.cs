﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AdventOfCSharp.Analyzers;

#nullable enable

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public abstract class ProblemAoCSAnalyzer : AoCSAnalyzer
{
    protected static bool IsProblemSolutionClass(ClassDeclarationSyntax? classDeclaration, SemanticModel semanticModel)
    {
        return IsProblemSolutionClass(classDeclaration, semanticModel, out _);
    }
    protected static bool IsProblemSolutionClass(ClassDeclarationSyntax? classDeclaration, SemanticModel semanticModel, out INamedTypeSymbol? classSymbol)
    {
        classSymbol = null;
        if (classDeclaration is null)
            return false;
        classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
        return IsProblemSolutionClass(classSymbol!);
    }

    protected static bool IsProblemSolutionClass(INamedTypeSymbol classSymbol)
    {
        return IsImportantAoCSClass(classSymbol, KnownSymbolNames.Problem);
    }
}
