#load "tools/includes.fsx"

open IntelliFactory.Build

let bt =
    BuildTool().PackageId("Zafir.Peity")
        .VersionFrom("Zafir")

let extensions =
    bt.Zafir.Extension("WebSharper.Peity.Bindings")
        .SourcesFromProject()
        .Embed(["jquery.peity.min.js"])

let wrapper =
    bt.Zafir.Library("WebSharper.Peity")
        .SourcesFromProject()
        .References(fun ref -> 
            [
                ref.Project extensions
                (ref.NuGet "Zafir.UI.Next").Latest(true).ForceFoundVersion().Reference()
            ])

let tests =
    bt.Zafir.BundleWebsite("WebSharper.Peity.Tests")
        .SourcesFromProject()
        .References(fun ref -> 
            [
                ref.Project extensions
                ref.Project wrapper
                (ref.NuGet "Zafir.UI.Next").Latest(true).Reference()
            ])

bt.Solution [
    extensions
    wrapper
    tests

    bt.NuGet.CreatePackage()
        .Configure(fun configuration ->
            { configuration with
                Title = Some "WebSharper.Peity"
                LicenseUrl = Some "http://websharper.com/licensing"
                ProjectUrl = Some "https://bitbucket.org/intellifactory/websharper.react"
                Description = "WebSharper bindings for Peity (http://benpickles.github.io/peity/)."
                Authors = [ "IntelliFactory" ]
                RequiresLicenseAcceptance = true })
        .Add(extensions)
        .Add(wrapper)
]
|> bt.Dispatch
