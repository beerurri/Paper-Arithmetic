using arithmetic;

Arithmetic arithmetic = new Arithmetic();
arithmetic.precision = 100;
//Console.WriteLine(arithmetic.Div(arithmetic.pi, "2"));

Console.WriteLine("=== Enter q to exit ===");
while (true) {
    Console.WriteLine("<<< ");
    Console.SetCursorPosition(4, Console.CursorTop - 1);
    string input = Console.ReadLine();
    if (input == "q")
        break;

    input = input.Replace('.', ',').Replace("pi", arithmetic.Constrain(arithmetic.pi)).Replace(
        "exp", arithmetic.Constrain(arithmetic.e));

    string result = string.Empty;

    Task.Run(() => {
        //Console.WriteLine(arithmetic.Sub(input.Replace('.', ',').Split(' ')[0], input.Replace('.', ',').Split(' ')[1]));
        if (input.Split(" + ").Length > 1)
            result = (arithmetic.Add(input.Split(" + ")[0], input.Split(" + ")[1]));
        else if (input.Split(" - ").Length > 1)
            result = (arithmetic.Sub(input.Split(" - ")[0], input.Split(" - ")[1]));
        else if (input.Split(" * ").Length > 1)
            result = (arithmetic.Mult(input.Split(" * ")[0], input.Split(" * ")[1]));
        else if (input.Split(" / ").Length > 1)
            result = (arithmetic.Div(input.Split(" / ")[0], input.Split(" / ")[1]));
        else if (input.Split("^").Length > 1)
            result = (arithmetic.Pow(input.Split("^")[0], input.Split("^")[1]));
        else if (input.Split("#").Length > 1)
            result = (arithmetic.Root(input.Split("#")[0], input.Split("#")[1]));
        else if (input.Split(" // ").Length > 1) {
            string[] output = arithmetic.EuDiv(input.Split(" // ")[0], input.Split(" // ")[1]);
            result = (output[0] + ", " + output[1]);
        } else if (input.Split('S').Length > 1) {
            string[] output = arithmetic.ToDecimalFraction(input.Split('S')[0]);
            result = (output[0] + " " + output[1]);
        } else if (input.Split('r').Length > 1)
            result = (arithmetic.Root(input.Split('r')[0], input.Split('r')[1]));
        else if (input.Split("acos").Length > 1)
            result = (arithmetic.ArcCos(input.Split("acos")[0],
                (uint)Int64.Parse(input.Split("acos")[1])));
        else if (input.Split("cos").Length > 1)
            result = (arithmetic.Cos(input.Split("cos")[0],
                (uint)Int64.Parse(input.Split("cos")[1])));
        else if (input.Split("atan").Length > 1)
            result = (arithmetic.ArcTan(input.Split("atan")[0],
                (uint)Int64.Parse(input.Split("atan")[1])));
        else if (input.Split("tan").Length > 1)
            result = (arithmetic.Tan(input.Split("tan")[0],
                (uint)Int64.Parse(input.Split("tan")[1])));
        else if (input.Split("asin").Length > 1)
            result = (arithmetic.ArcSin(input.Split("asin")[0],
                (uint)Int64.Parse(input.Split("asin")[1])));
        else if (input.Split("sin").Length > 1)
            result = (arithmetic.Sin(input.Split("sin")[0],
                (uint)Int64.Parse(input.Split("sin")[1])));
        else
            result = ("Retype pls");
    });
        
    Console.WriteLine("Computing...");
    while (result.Length == 0) { }

    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
    Console.WriteLine("               ");
    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
    Console.WriteLine(">>> " + result);
}

// 1200,12390
