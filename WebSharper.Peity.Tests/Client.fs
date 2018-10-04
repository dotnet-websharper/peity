// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2018 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}
namespace WebSharper.Peity.Tests

open WebSharper
open WebSharper.JavaScript
open WebSharper.Peity

[<JavaScript>]
module Client =
    open WebSharper.UI.Client
    open WebSharper.UI.Html

    [<SPAEntryPoint>]
    let main() =
        div [] [
            let chart1 = UpdatingChart([], config = PeityConfig(Width = 64))
            
            yield chart1.Doc
            yield div [] [
                Doc.Button "Button1" [] (fun () ->
                    chart1.AddValue <| Math.Random()
                )
            ] :> _

            yield hr [] [] :> _
            yield div [] [
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
