open System
open System.IO
open Student


let processFile (filename: string) =
    File.ReadAllLines filename
    |> Array.skip 1
    |> Array.map Student.fromString
    |> Array.sortBy (fun x -> x.FirstName)
    |> Array.iter Student.printStudent

[<EntryPoint>]
let main argv =
    if argv.Length > 0 then
        let filename = argv.[0]
        try
            processFile filename |> ignore
            0
        with
        | :? FormatException as ex ->
            printfn "Format Exception: %s" ex.Message
            2
        | :? FileNotFoundException as ex ->
            printfn "File Not Found Exception: %s" ex.Message
            3
        | ex ->
            printfn "Something went wrong: %s" ex.Message
            4
    else
        printfn "Filename not given. Try dotnet run <filename>"
        1