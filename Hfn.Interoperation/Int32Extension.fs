namespace Hfn.Interoperation.Extensions

[<System.Runtime.CompilerServices.Extension>]
module Int32Extension =
    [<CompiledName("IsSome")>]
    [<System.Runtime.CompilerServices.Extension>]
    let isSome (opt: int option) = opt.IsSome

    [<CompiledName("IsNone")>]
    [<System.Runtime.CompilerServices.Extension>]
    let isNone (opt: int option) = opt.IsNone

