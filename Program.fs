open System
open System.IO
open Student
open Score
open School

let processFile (studentFilePath: string, schoolCodesFilePath: string) =
    let schoolCodes =
        schoolCodesFilePath
        |> File.ReadLines
        |> Seq.skip 1
        |> Seq.map School.parseFromString
        |> Map.ofSeq

    File.ReadAllLines studentFilePath
    |> Array.skip 1
    |> Array.map (Student.fromString schoolCodes)
    |> Array.sortByDescending (fun x -> x.MeanScore)
    |> Array.groupBy
        (fun x ->
            x.MeanScore
            |> Score.determineResult
        )
    |> Array.iter Student.printGroup


//dotnet run .\resources\StudentScoresSchoolAlphaCodes.txt .\resources\SchoolCodesAlpha.txt
[<EntryPoint>]
let main argv =
    if argv.Length > 1 then
        let studentFilePath = argv.[0]
        let schoolCodeFilePath = argv.[1]
        try
            (studentFilePath, schoolCodeFilePath)
            |> processFile
            |> ignore
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
        printfn "Filenames not given. Try dotnet run <filename> <filename>"
        1