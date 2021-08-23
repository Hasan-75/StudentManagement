namespace School

open Microsoft.FSharp.Core

type School = {
    SchoolCode: string
    SchoolName: string
}

module School =
    let parseFromString (row: string) =
        let cols = row.Split("\t")
        let school =
            {
                SchoolCode = cols.[0]
                SchoolName = cols.[1]
            }
        (cols.[0], school)

module Option =
    let defaultSchool (maybeSchool: option<School>, schoolCode: string) =
        match maybeSchool with
        | Some school -> school
        | None -> {
                SchoolCode = schoolCode
                SchoolName = "<Unknown>"
            }