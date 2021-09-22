namespace ProtobufServer

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Server.Kestrel.Core
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging

module Program =
    let createHostBuilder args =
        WebHost.CreateDefaultBuilder(args)
            .ConfigureKestrel(fun options ->
                options.ListenLocalhost(10042, fun listenOptions ->
                    listenOptions.Protocols <- HttpProtocols.Http2))
            .UseStartup<Startup>()

    [<EntryPoint>]
    let main args =
        createHostBuilder(args).Build().Run()

        0 // Exit code
