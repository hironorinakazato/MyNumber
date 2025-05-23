namespace Hfn

/// <summary>
/// Provides functions for working with My Number (Japanese Individual Number).
/// </summary>
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MyNumber =
    let private checkDigitIncludedLength = 12
    let private checkDigitExcludedLength = 11

    [<RequireQualifiedAccess>]
    type ValidationError =
        | InvalidLength
        | InvalidCharacters
        | NullInput

    let private validateNotNull (input: string) =
        if isNull input then Error ValidationError.NullInput
        else Ok input

    let private validateLength (expected: int) (input: string) =
        if input.Length = expected then Ok input
        else Error ValidationError.InvalidLength

    let private validateCharacters (input: string) =
        if input |> Seq.forall System.Char.IsDigit then Ok input
        else Error ValidationError.InvalidCharacters

    let private validateWithoutCheckDigit (input: string) : Result<string, ValidationError> =
        input
        |> validateNotNull
        |> Result.bind (validateLength checkDigitExcludedLength)
        |> Result.bind validateCharacters

    let private validateWithCheckDigit (input: string) : Result<string, ValidationError> =
        input
        |> validateNotNull
        |> Result.bind (validateLength checkDigitIncludedLength)
        |> Result.bind validateCharacters

    let private extractDigitsReversed (input: string) =
        input
        |> int64
        |> Seq.unfold (fun x -> Some((%) x 10L, (/) x 10L))
        |> Seq.take checkDigitExcludedLength

    /// <summary>
    /// Calculates the check digit for a My Number (Japanese Individual Number).
    /// </summary>
    /// <param name="myNumberWithoutCheckDigit">A string representing the My Number without the check digit.</param>
    /// <returns>The calculated check digit as a single character string, or None if the input is invalid.</returns>
    [<CompiledName("CalculateCheckDigit")>]
    let calculateCheckDigit (myNumberWithoutCheckDigit: string) =
        match validateWithoutCheckDigit myNumberWithoutCheckDigit with
        | Error _ -> None
        | Ok validInput ->
            let source = extractDigitsReversed validInput
            let weights = [2L; 3L; 4L; 5L; 6L; 7L; 2L; 3L; 4L; 5L; 6L;]
            let result = (%) (source |> Seq.map2 (*) weights |> Seq.sum) 11L |> (-) 11L
            if result < 10L then result else 0L
            |> int
            |> Some

    /// <summary>
    /// Determines whether the check digit of a My Number (Japanese Individual Number) is valid.
    /// </summary>
    /// <param name="myNumber">A string representing the My Number (Japanese Individual Number).</param>
    /// <returns>`true` if the check digit is valid; otherwise, `false`.</returns>
    [<CompiledName("IsValidCheckDigit")>]
    let isValidCheckDigit (myNumber: string) =
        match validateWithCheckDigit myNumber with
        | Error _ -> false
        | Ok valid ->
            match calculateCheckDigit (valid.Substring(0, checkDigitExcludedLength)) with
            | None -> false
            | Some checkDigit ->
                valid[checkDigitExcludedLength].ToString()
                |> int
                |> (=) checkDigit

