namespace ProtobufCommon

open System.IO

open ProtoBuf.Grpc.Reflection
open ProtoBuf.Meta

module Program =

    [<EntryPoint>]
    let main args =
        let generator = SchemaGenerator(ProtoSyntax = ProtoSyntax.Proto3)
        let schema = generator.GetSchema<ITimeService>()
        use wtr = new StreamWriter("services.proto")
        wtr.Write(schema)
        0
