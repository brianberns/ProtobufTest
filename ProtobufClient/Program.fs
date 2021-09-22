open ProtoBuf.Grpc.Client
open Grpc.Net.Client

open FSharp.Control
open FSharp.Control.Tasks

open ProtobufCommon

let calc () =
    task {
        use http = GrpcChannel.ForAddress("http://localhost:10042")

        let client = http.CreateGrpcService<ICalculator>()
        let! result = client.MultiplyAsync { X = 12; Y = 4 }
        printfn "%i" result.Result
    } |> fun t -> t.Result

let clock () =
    task {
        use http = GrpcChannel.ForAddress("http://localhost:10042")

        let client = http.CreateGrpcService<ITimeService>()
        async {
            for result in client.SubscribeAsync() |> AsyncSeq.ofAsyncEnum do
                printfn "%A" result.Time
        } |> Async.RunSynchronously
    } |> fun t -> t.Result

[<EntryPoint>]
let main _ =
    GrpcClientFactory.AllowUnencryptedHttp2 <- true
    calc ()
    clock ()
    0
