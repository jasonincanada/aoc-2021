module AdventOfCode.FSharp.Day01
    
let doCase (numbers : int list) : int list = 
    [ for m in numbers do
        for n in numbers do
            if m + n = 2020 then yield m*n ]
