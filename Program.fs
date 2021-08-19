
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
        if File.Exists filename then
            processFile filename |> ignore
            0
        else
            printfn "File not found"
            2
    else
        printfn "Filename not given. Try dotnet run <filename>"
        1