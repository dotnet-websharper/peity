namespace WebSharper.Peity.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.Peity

[<JavaScript>]
module Test0 =
    open WebSharper.Html.Client

    let chart =
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

[<JavaScript>]
module Test1 =
    
    let chart =
        UpdatingChart([], PeityConfig(Width = 64))

[<JavaScript>]
module Client =
    open WebSharper.UI.Next.Client
    open WebSharper.UI.Next.Html

    let main =
        (Html.Client.Tags.Div [ Test0.chart ]).AppendTo "body"
        
        async {
            for _ in 1 .. 10 do
                do! Async.Sleep 500
                (Test1.chart.AddValue << double) <| Math.Floor(Math.Random() * 10.0)
        }
        |> Async.Start

        div [ Test1.chart.Doc ]
        |> Doc.RunAppendById "body"
