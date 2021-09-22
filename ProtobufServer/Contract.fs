namespace ProtobufServer

open System
open System.Threading.Tasks

open ProtobufCommon

open FSharp.Control

type CalculatorService =
    interface ICalculator with
        member __.MultiplyAsync(req : MultiplyRequest) =
            ValueTask<MultiplyResult>({ Result = req.X * req.Y })

type TimeService() =
    interface ITimeService with
        member __.SubscribeAsync() =
            asyncSeq {
                while true do
                    yield { Time = DateTime.Now }
                    do! Async.Sleep 1000
            } |> AsyncSeq.toAsyncEnum
