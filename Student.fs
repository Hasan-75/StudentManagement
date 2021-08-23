namespace Student

open System
open Score
open School

module Student =

    //let calculateScore (resultData: Test)

    type Student = {
        FirstName:  string
        LastName:   string
        School:     School
        Id:         string
        MeanScore:  float
        MaxScore:   float
        MinScore:   float
    }

    let parseName (fullname: string) =
        match fullname.Split(',') with
        | [| firstName; lastName |]
            -> {|
                    FirstName = firstName
                    LastName  = lastName
                |}
        | [| firstName |]
            -> {|
                    FirstName = firstName
                    LastName  = "(Not given)"
                |}
        | _
            -> raise(FormatException(sprintf "Invalid name format: %s" fullname))


    let fromString (schoolCodes: Map<string, School>) (row: string) =
        let cols = row.Split('\t')
        let scores =
            cols
            |> Array.skip 3
            |> Array.choose
                (Score.tryParseFromStr >> Score.calculateScore)

        let name = cols.[0] |> parseName
        let schoolCode = cols.[2]

        let school =
            (schoolCodes.TryFind(schoolCode), schoolCode)
            |> Option.defaultSchool

        match Array.length scores with
        | 0 ->
            {
                FirstName  = name.FirstName
                LastName   = name.LastName
                School     = school
                Id         = cols.[1]
                MeanScore  = 0.0
                MaxScore   = 0.0
                MinScore   = 0.0
            }
        | _ ->
            {
                FirstName  = name.FirstName
                LastName   = name.LastName
                School     = school
                Id         = cols.[1]
                MeanScore  = scores |> Array.average
                MaxScore   = scores |> Array.max
                MinScore   = scores |> Array.min
            }

    let printStudent (student: Student) =
        printfn "\t%s %s    %s    %s    %.2f    %.2f    %.2f"
            student.FirstName student.LastName
            student.School.SchoolName student.Id student.MeanScore
            student.MaxScore student.MinScore

    let printGroup (groupTitle: 'T, students: Student[]) =
        printfn "%O:" groupTitle
        printfn "======================================="
        students
        |> Array.sortByDescending (fun x -> x.MeanScore)
        |> Array.iter printStudent
        printfn "\n\n"
