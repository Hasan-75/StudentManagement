namespace Student

module Score =
    type TestScore =
        | Absent
        | Excused
        | Scored of float

    let calculateScore (testScore: TestScore) : Option<float> =
        match testScore with
        | Absent -> Some(0.0)
        | Excused -> None
        | Scored x -> Some(x)