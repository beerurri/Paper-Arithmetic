using arithmetic;

//static int digits(decimal number) {
//    string str = number.ToString();
//    string[] splittedStr = str.Split(',');
//    return splittedStr[1].Length;
//}

//static string DecimalToBinary(uint decimalNumber) {
//    var binaryNumber = string.Empty;
//    while (decimalNumber > 0) {
//        binaryNumber = (decimalNumber % 2) + binaryNumber;
//        decimalNumber /= 2;
//    }

//    return binaryNumber;
//}

//int limit = 50;
//decimal myE = 1;
//double fact = 1;
//double counter = 1;
//bool flag = true;

//Console.WriteLine("Start");

//while (flag) {
//    myE += (decimal)(((decimal)(1e21 / fact) / (decimal)1e21));
//    counter++;
//    fact *= counter;
//    Console.WriteLine(counter.ToString() + ' ' + myE.ToString());
//    if (counter >= limit)
//        break;
//}
//Console.WriteLine((decimal)23225 / (decimal)8544);

//string x = "abcd";
//Console.WriteLine(Arithmetic.Add("987,12345", "2,222"));
// 987,12345
//   2,222
// 989,34545

//var a = 1200.123456789;
//var b = 99.22222;
//Console.WriteLine(Arithmetic.Sub(a.ToString(), b.ToString()) + " " + (a-b).ToString());

//a = 12;
//b = 11.124;
//Console.WriteLine(Arithmetic.Sub(a.ToString(), b.ToString()) + " " + (a - b).ToString());
// 1200,123
//   99,22222
// 1100,90078

//var a = 3.3345;
//var b = 97.120938;
//Console.WriteLine(Arithmetic.Mult(a.ToString(), b.ToString()) + " " + (a * b).ToString());

//while (true) {
//    string input = Console.ReadLine();
//    if (input == "q")
//        break;
//    Console.WriteLine(Arithmetic.Comp(input.Split(' ')[0].Replace('.', ','),
//        input.Split(' ')[1].Replace('.', ',')));
//}

//uint limit = 50000;
//string myE = "1";
//string fact = "1";
//uint counter = 1;
//Arithmetic arithmetic = new Arithmetic();
//arithmetic.precition = 10000;

//Console.WriteLine("Start");

//var watch = System.Diagnostics.Stopwatch.StartNew();
//while (counter < limit) {
//    string fraction = arithmetic.Div("1", fact);
//    if (arithmetic.Comp(fraction, "0") == 'e') {
//        Task.Run(() => {
//            Console.WriteLine("Counter: " + counter.ToString());
//        });
//        break;
//    }
//    myE = arithmetic.Add(myE, fraction);
//    counter++;
//    fact = arithmetic.Mult(fact, counter.ToString());
//    if (counter % 10 == 0) {
//        Task.Run(() => {
//            Console.WriteLine(counter.ToString() + " " + fact.Length.ToString());
//        });
//    }
//    //Console.WriteLine(counter.ToString() + ' ' + myE.ToString());
//}
//watch.Stop();
//Console.WriteLine("Result: " + myE);
//Console.WriteLine("Time: " + watch.ElapsedMilliseconds.ToString() + " ms");
//Console.ReadLine();

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
