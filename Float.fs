namespace Student
open Score

module Float =
    let tryParseFromStr (s: string) : TestScore =
        if s.Equals "A" then
            Absent
        else if s.Equals "E" then
            Excused
        else
            s
            |> float
            |> Scored

    // let tryParseFromStrOr (def: float) (s: string) =
    //     s
    //     |> tryParseFromStr
    //     |> Option.defaultValue def
