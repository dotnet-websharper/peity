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
