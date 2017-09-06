#load "tools/includes.fsx"

open IntelliFactory.Build

let bt =
    BuildTool().PackageId("WebSharper.Peity")
        .VersionFrom("WebSharper", versionSpec = "(,4.0)")

let extensions =
    bt.WebSharper.Extension("WebSharper.Peity.Bindings")
        .SourcesFromProject()
        .Embed(["jquery.peity.min.js"])

let wrapper =
    bt.WebSharper.Library("WebSharper.Peity")
        .SourcesFromProject()
        .References(fun ref -> 
            [
                ref.Project extensions
                (ref.NuGet "WebSharper.UI.Next").Reference()
            ])

let tests =
    bt.WebSharper.BundleWebsite("WebSharper.Peity.Tests")
        .SourcesFromProject()
        .References(fun ref -> 
            [
                ref.Project extensions
                ref.Project wrapper
                (ref.NuGet "WebSharper.UI.Next").Reference()
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
