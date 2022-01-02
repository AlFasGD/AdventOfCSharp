﻿using Microsoft.CodeAnalysis;
using RoseLynn.Analyzers;
using System.Resources;

namespace AdventOfCSharp.Analyzers;

internal sealed class AoCSDiagnosticDescriptorStorage : DiagnosticDescriptorStorageBase
{
    public static readonly AoCSDiagnosticDescriptorStorage Instance = new();

    protected override string BaseRuleDocsURI => "https://github.com/AlFasGD/AdventOfCSharp/blob/master/docs/analyzers/rules";
    protected override string DiagnosticIDPrefix => "AoCS";
    protected override ResourceManager ResourceManager => DiagnosticStringResources.ResourceManager;

    #region Category Constants
    public const string BrevityCategory = "Brevity";
    public const string ConventionCategory = "Convention";
    public const string DesignCategory = "Design";
    public const string InformationCategory = "Information";
    public const string ValidityCategory = "Validity";
    #endregion

    #region Rules
    private AoCSDiagnosticDescriptorStorage()
    {
        SetDefaultDiagnosticAnalyzer<PartSolutionAnalyzer>();

        CreateDiagnosticDescriptor(0001, ValidityCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0002, ValidityCategory, DiagnosticSeverity.Error);

        SetDefaultDiagnosticAnalyzer<ProblemInheritanceAnalyzer>();

        CreateDiagnosticDescriptor(0003, BrevityCategory, DiagnosticSeverity.Warning);

        SetDefaultDiagnosticAnalyzer<ProblemClassNamingAnalyzer>();

        CreateDiagnosticDescriptor(0004, ConventionCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0005, ConventionCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0006, ConventionCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0007, ConventionCategory, DiagnosticSeverity.Error);

        SetDefaultDiagnosticAnalyzer<SecretsContainerAnalyzer>();

        CreateDiagnosticDescriptor(0008, ValidityCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0009, ValidityCategory, DiagnosticSeverity.Error);
        CreateDiagnosticDescriptor(0010, ValidityCategory, DiagnosticSeverity.Error);

        SetDefaultDiagnosticAnalyzer<FinalDayAnalyzer>();

        CreateDiagnosticDescriptor(0011, DesignCategory, DiagnosticSeverity.Warning);
        CreateDiagnosticDescriptor(0012, ValidityCategory, DiagnosticSeverity.Error);

        SetDefaultDiagnosticAnalyzer<PartSolverAttributeAnalyzer>();

        CreateDiagnosticDescriptor(0013, ValidityCategory, DiagnosticSeverity.Error);
    }
    #endregion
}
