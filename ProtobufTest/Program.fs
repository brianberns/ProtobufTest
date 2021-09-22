open System.IO
open ProtoBuf

[<ProtoContract; CLIMutable>]
type Address =
    {
        [<ProtoMember(1)>] Line1 : string
        [<ProtoMember(2)>] Line2 : string
    }

[<ProtoContract; CLIMutable>]
type Person =
    {
        [<ProtoMember(1)>] Id : int
        [<ProtoMember(2)>] Name : string
        [<ProtoMember(3)>] Address : Address
    }

let serialize () =
    let person =
        {
            Id = 12345
            Name = "Fred"
            Address =
                {
                    Line1 = "Flat 1"
                    Line2 = "The Meadows"
                }
        }
    use file = File.Create("person.bin")
    Serializer.Serialize(file, person)

let deserialize () =
    use file = File.OpenRead("person.bin");
    let person = Serializer.Deserialize<Person>(file)
    printfn "%A" person

[<EntryPoint>]
let main argv =
    serialize ()
    deserialize ()
    0
