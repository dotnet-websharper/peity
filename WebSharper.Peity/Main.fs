namespace WebSharper.Peity

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery

[<AutoOpen>]
module Extension =

    type JQuery with
        [<Inline "$0.peity($t)">]
        member x.Peity(t: string) = X<JQuery>

        [<Inline "$0.peity($t, $config)">]
        member x.Peity(t: string, config: PeityConfig) = X<JQuery>
