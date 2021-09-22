namespace ProtobufServer

open ProtobufCommon

type TimeService() =
    interface ITimeService with
        member this.SubscribeAsync() =
            raise (System.NotImplementedException())
