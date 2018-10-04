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
namespace WebSharper.Peity.Bindings

open WebSharper.InterfaceGenerator

module Definition =
    
    let PeityConfig =
        Class "PeityConfig"
        |+> Static [
            ObjectConstructor Type.Parameters.Empty
        ]
        |+> Instance [
            "delimiter" =@ T<string>
            "fill" =@ T<string []>
            "height" =@ T<int>
            "radius" =@ T<int>
            "width" =@ T<int>
            "innerRadius" =@ T<int>
            "max" =@ T<float>
            "min" =@ T<float>
            "stroke" =@ T<string []>
            "strokeWidth" =@ T<int>
            "padding" =@ T<int>
        ]

    let Assembly =
        Assembly [
            Namespace "WebSharper.Peity" [
                 PeityConfig
            ]
            Namespace "WebSharper.Peity.Resources" [
                (Resource "Peity 3.2.0" "jquery.peity.min.js")
                |> RequiresExternal [ T<WebSharper.JQuery.Resources.JQuery> ]
                |> AssemblyWide
            ]
        ]

[<Sealed>]
type Extension() =
    interface IExtension with
        member ext.Assembly =
            Definition.Assembly

[<assembly: Extension(typeof<Extension>)>]
do ()
