global using System;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Linq;
global using System.Threading;
global using Microsoft.CodeAnalysis;
global using Microsoft.CodeAnalysis.CSharp;
global using Microsoft.CodeAnalysis.CSharp.Syntax;
global using Puzzles.SourceGeneration.Handlers;
global using static Microsoft.CodeAnalysis.NullableAnnotation;
global using static Microsoft.CodeAnalysis.SpecialType;
global using static SolutionVersion;
global using DeclaredAccessibility = Microsoft.CodeAnalysis.Accessibility;
global using NamedArgs = System.Collections.Immutable.ImmutableArray<System.Collections.Generic.KeyValuePair<string, Microsoft.CodeAnalysis.TypedConstant>>;
