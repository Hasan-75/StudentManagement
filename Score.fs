namespace Score

module Score =
    [<Literal>]
    let PASS_THRESHOLD = 50.0

    type TestScore =
        | Absent
        | Excused
        | Scored of float

    type Result =
        | Passed
        | Failed

    let tryParseFromStr (s: string) : TestScore =
        if s.Equals "A" then
            Absent
        else if s.Equals "E" then
            Excused
        else
            s
            |> float
            |> Scored

    let calculateScore (testScore: TestScore) : Option<float> =
        match testScore with
        | Absent -> Some(0.0)
        | Excused -> None
        | Scored x -> Some(x)

    let determineResult (score: float) : Result =
        match score with
        | x when x > PASS_THRESHOLD -> Passed
        | _ -> Failed
