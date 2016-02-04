namespace WebSharper.Peity.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.Peity

[<JavaScript>]
module Client =
    open WebSharper.UI.Next.Client
    open WebSharper.UI.Next.Html

    let main =
        div [
            let chart1 = UpdatingChart([], config = PeityConfig(Width = 64))
            
            yield chart1.Doc
            yield div [
                Doc.Button "Button1" [] (fun () ->
                    chart1.AddValue <| Math.Random()
                )
            ] :> _

            yield hr [] :> _
            yield div [
                let config =
                    PeityConfig(
                        Width = 64,
                        Fill  = [| "rgba(255,0,0,0.5)" |],
                        Stroke = [| "red" |]
                    )
                let chart2 = UpdatingChart([], 10, config)

                let rec update (c: UpdatingChart) =
                    async {
                        let! _ = Async.Sleep 1000

                        c.AddValue <| Math.Random()
                        update c
                    }
                    |> Async.Start

                yield chart2.Doc
                update chart2
            ] :> _
        ]
        |> Doc.RunById "body"
