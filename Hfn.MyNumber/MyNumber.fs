namespace Hfn

/// <summary>
/// "My number" is an individual number given to each person living in Japan.
/// </summary>
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MyNumber =
    let private length = 12
    let private notIncludeCheckDigitLength = 11

    let private (|IsLengthCheckInvalid|_|) length input =
        if input |> Seq.length <> length then Some(input) else None
    let private (|IsNumberCheckInvalid|_|) input =
        if input |> Seq.forall (fun x -> System.Char.IsNumber x) = false then Some(input) else None

    /// <summary>Calculate the check digit.</summary>
    [<CompiledName("CalculateCheckDigit")>]
    let calculateCheckDigit (myNumberNotIncludeCheckDigit: string) =
        match myNumberNotIncludeCheckDigit with
        | null -> None
        | IsLengthCheckInvalid notIncludeCheckDigitLength _ -> None
        | IsNumberCheckInvalid _ -> None
        | _ ->
            let source =
                myNumberNotIncludeCheckDigit
                |> int64
                |> Seq.unfold (fun x -> Some((%) x 10L, (/) x 10L))
                |> Seq.take notIncludeCheckDigitLength
            let weights = [2L; 3L; 4L; 5L; 6L; 7L; 2L; 3L; 4L; 5L; 6L;]
            let result = (%) (source |> Seq.map2 (*) weights |> Seq.sum) 11L |> (-) 11L
            if result < 10L then result else 0L
            |> int
            |> Some

    ///<summary>Verify the check digit valid.</summary>
    [<CompiledName("IsCheckDigitValid")>]
    let isCheckDigitValid (myNumber: string) =
        match myNumber with
        | null -> false
        | IsLengthCheckInvalid length _ -> false
        | IsNumberCheckInvalid _ -> false
        | _ ->
            let checkDigit = myNumber.Substring(0, notIncludeCheckDigitLength) |> calculateCheckDigit
            if checkDigit.IsNone then false
            else
                myNumber.Substring(notIncludeCheckDigitLength) |> int |> (=) checkDigit.Value

