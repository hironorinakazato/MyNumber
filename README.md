# MyNumber

A .NET library for validating Japanese My Number (Individual Number) check digits. Written in F# with full C# interoperability support.

## Features

- Calculate check digits for My Number
- Validate My Number check digits
- Full support for both F# and C# consumers
- Functional programming approach with Railway Oriented Programming (ROP)

## Installation / Getting Started

**Requirements:** .NET 10.0

### Build
```bash
dotnet build Hfn.MyNumber.sln
```

### Run tests
```bash
dotnet test Hfn.MyNumber.sln
```

## Usage

### F# Example

```fsharp
open Hfn

// Calculate check digit
let checkDigit = MyNumber.calculateCheckDigit "12345678901"
// Returns: Some 8

// Validate My Number with check digit
let isValid = MyNumber.isValidCheckDigit "123456789018"
// Returns: true
```

### C# Example

```csharp
using Hfn;
using Hfn.Interoperation.Extensions;

// Calculate check digit
var checkDigit = MyNumberModule.CalculateCheckDigit("12345678901");
if (checkDigit.IsSome())
{
    Console.WriteLine($"Check digit: {checkDigit.Value}");
    // Output: Check digit: 8
}

// Validate My Number with check digit
var isValid = MyNumberModule.IsValidCheckDigit("123456789018");
// Returns: true
```

## Project Structure

The solution consists of four projects:

1. **Hfn.MyNumber** - Core F# library implementing My Number validation logic
2. **Hfn.Interoperation** - F# library providing C# interoperability extensions
3. **Hfn.MyNumber.FSharpTest** - F# test project using MSTest
4. **Hfn.MyNumber.CSharpTest** - C# test project using MSTest

## Development

### Run F# tests only
```bash
dotnet test Hfn.MyNumber.FSharpTest/Hfn.MyNumber.FSharpTest.fsproj
```

### Run C# tests only
```bash
dotnet test Hfn.MyNumber.CSharpTest/Hfn.MyNumber.CSharpTest.csproj
```

### Run specific test by category
```bash
dotnet test --filter TestCategory=calculateCheckDigit
dotnet test --filter TestCategory=isValidCheckDigit
```

## Architecture

The library uses Railway Oriented Programming (ROP) for validation, composing multiple validation functions (null check → length validation → character validation) to ensure data integrity. F# functions are exposed to C# using the `[<CompiledName>]` attribute, and the Hfn.Interoperation project provides extension methods for working with F# `option` types in C#.
