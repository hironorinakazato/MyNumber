namespace Hfn

/// <summary>
/// "My number" is an individual number given to each person living in Japan.
/// </summary>
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MyNumber =
    /// <summary>Calculate the check digit.</summary>
    [<CompiledName("CalculateCheckDigit")>]
    let calculateCheckDigit (myNumberNotIncludeCheckDigit: string) =
        if myNumberNotIncludeCheckDigit = null then None
        elif myNumberNotIncludeCheckDigit |> Seq.length <> 11 then None
        elif myNumberNotIncludeCheckDigit |> Seq.forall (fun x -> System.Char.IsNumber x) = false then None
        else
            let source =
                myNumberNotIncludeCheckDigit
                |> int64
                |> Seq.unfold (fun x -> Some((%) x 10L, (/) x 10L))
                |> Seq.take 11
            let weights = [2L; 3L; 4L; 5L; 6L; 7L; 2L; 3L; 4L; 5L; 6L;]
            let result = (%) (source |> Seq.map2 (*) weights |> Seq.sum) 11L |> (-) 11L
            if result < 10L then result else 0L
            |> int
            |> Some

    ///<summary>Verify the check digit valid.</summary>
    [<CompiledName("IsCheckDigitValid")>]
    let isCheckDigitValid (myNumber: string) =
        if myNumber = null then false
        elif myNumber |> Seq.length <> 12 then false
        elif myNumber |> Seq.forall (fun x -> System.Char.IsNumber x) = false then false
        else
            let checkDigit = myNumber.Substring(0, 11) |> calculateCheckDigit
            if checkDigit.IsNone then false
            else
                myNumber.Substring(11) |> int |> (=) checkDigit.Value

