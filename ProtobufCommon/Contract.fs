// https://protobuf-net.github.io/protobuf-net.Grpc/gettingstarted

namespace ProtobufCommon

open System
open System.Collections.Generic
open System.Runtime.Serialization
open System.ServiceModel
open System.Threading.Tasks

open ProtoBuf

[<DataContract; CLIMutable>]
type MultiplyRequest =
    {
        [<DataMember(Order = 1)>] X : int
        [<DataMember(Order = 2)>] Y : int
    }

[<DataContract; CLIMutable>]
type MultiplyResult =
    {
        [<DataMember(Order = 1)>] Result : int
    }

[<ServiceContract(Name = "Calculator")>]
type ICalculator =
    abstract member MultiplyAsync : MultiplyRequest -> ValueTask<MultiplyResult>

[<ProtoContract; CLIMutable>]
type TimeResult =
    {
        [<ProtoMember(1, DataFormat = DataFormat.WellKnown)>] Time : DateTime
    }

[<ServiceContract>]
type ITimeService =
    abstract member SubscribeAsync : unit -> IAsyncEnumerable<TimeResult>
