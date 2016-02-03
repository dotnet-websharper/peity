namespace WebSharper.Peity

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<AutoOpen>]
module JQueryExtensions =

    type JQuery with
        [<Inline "$0.peity($t)">]
        member x.Peity(t: string) = X<JQuery>

        [<Inline "$0.peity($t, $config)">]
        member x.Peity(t: string, config: PeityConfig) = X<JQuery>

[<JavaScript>]
module Option =
    
    let toObj (option: 'T option) =
        match option with
        | Some o -> o
        | _      -> null

[<JavaScript>]
module Seq =
    
    let inline indexed (source: seq<'T>) = Seq.mapi (fun i v -> (i, v)) source

[<JavaScript>]
type UpdatingChart(data: seq<double>, ?config: PeityConfig) =
    let data = ListModel.Create fst (Seq.indexed data)
    
    member val Doc =
        data.View
        |> Doc.BindView (fun s ->
            spanAttr [
                on.afterRender (fun e ->
                    JQuery.Of(e).Peity("line", (Option.toObj config)) |> ignore
                )
            ] [
                let delimiter = 
                    match config with
                    | Some c -> c.Delimiter
                    | None   -> ","
                
                yield Seq.map (fun (_, v) -> string v) s
                    |> String.concat delimiter
                    |> text
            ]
        ) with get

    member this.AddValue v = data.Add (data.Length, v)

    member this.Update s = data.Set (Seq.indexed s)
