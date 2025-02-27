namespace Hfn

/// <summary>
/// Provides functions for working with My Number (Japanese Individual Number).
/// </summary>
/// <remarks>
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MyNumber =
    let private checkDigitIncludedLength = 12
    let private checkDigitExcludedLength = 11

    let private (|InvalidLength|_|) length input =
        if input |> Seq.length <> length then Some(input) else None
    let private (|InvalidNumber|_|) input =
        if input |> Seq.forall (fun x -> System.Char.IsNumber x) = false then Some(input) else None

    /// <summary>
    /// Calculates the check digit for a My Number (Japanese Individual Number).
    /// </summary>
    /// <param name="myNumberWithoutCheckDigit">A string representing the My Number without the check digit.</param>
    /// <returns>The calculated check digit as a single character string.</returns>
    [<CompiledName("CalculateCheckDigit")>]
    let calculateCheckDigit (myNumberWithoutCheckDigit: string) =
        match myNumberWithoutCheckDigit with
        | null -> None
        | InvalidLength checkDigitExcludedLength _ -> None
        | InvalidNumber _ -> None
        | _ ->
            let source =
                myNumberWithoutCheckDigit
                |> int64
                |> Seq.unfold (fun x -> Some((%) x 10L, (/) x 10L))
                |> Seq.take checkDigitExcludedLength
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
        match myNumber with
        | null -> false
        | InvalidLength checkDigitIncludedLength _ -> false
        | InvalidNumber _ -> false
        | _ ->
            let checkDigit = myNumber.Substring(0, checkDigitExcludedLength) |> calculateCheckDigit
            if checkDigit.IsNone then false
            else
                myNumber.Substring(checkDigitExcludedLength) |> int |> (=) checkDigit.Value

