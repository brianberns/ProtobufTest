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
    use http = GrpcChannel.ForAddress("http://localhost:10042")
    let client = http.CreateGrpcService<ITimeService>()
    async {
        for (min, sec) in client.SubscribeTupleAsync() |> AsyncSeq.ofAsyncEnum do
            printfn "%s, %s" min sec
    } |> Async.RunSynchronously

[<EntryPoint>]
let main _ =
    GrpcClientFactory.AllowUnencryptedHttp2 <- true
    calc ()
    clock ()
    0
