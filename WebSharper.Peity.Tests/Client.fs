namespace WebSharper.Peity.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.Peity
open WebSharper.Html.Client

[<JavaScript>]
module Client =
    
    let Main =
        Span [
            [|0 .. 10|]
            |> Array.map (fun _ ->
                string <| Math.Floor(Math.Random() * 10.0)
            )
            |> String.concat ","
            |> Text
        ]
        |>! OnAfterRender (fun data ->
            JQuery.Of(data.Dom).Peity("line", PeityConfig(Width = 64))
            |> ignore
        )
        |> (fun node ->
            node.AppendTo "body"
        )
