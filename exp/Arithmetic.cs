using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arithmetic {
    /// <summary>
    /// Works only with comma-separated numbers
    /// </summary>
    class Arithmetic {
        /// <summary>
        /// The number of decimal places
        /// </summary>
        public uint precision = 50; // max (uint.MaxValue / 2 - 1)
        public readonly string e = "2,7182818284590452353602874713526624977572470936999595749669676277240766303535475945713821785251664274274663919320030599218174135966290435729003342952605956307381323286279434907632338298807531952510190115738341879307021540891499348841675092447614606680822648001684774118537423454424371075390777449920695517027618386062613313845830007520449338265602976067371132007093287091274437470472306969772093101416928368190255151086574637721112523897844250569536967707854499699679468644549059879316368892300987931277361782154249992295763514822082698951936680331825288693984964651058209392398294887933203625094431173012381970684161403970198376793206832823764648042953118023287825098194558153017567173613320698112509961818815930416903515988885193458072738667385894228792284998920868058257492796104841984443634632449684875602336248270419786232090021609902353043699418491463140934317381436405462531520961836908887070167683964243781405927145635490613031072085103837505101157477041718986106873969655212671546889570350354";
        public readonly string pi = "3,1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679821480865132823066470938446095505822317253594081284811174502841027019385211055596446229489549303819644288109756659334461284756482337867831652712019091456485669234603486104543266482133936072602491412737245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094330572703657595919530921861173819326117931051185480744623799627495673518857527248912279381830119491298336733624406566430860213949463952247371907021798609437027705392171762931767523846748184676694051320005681271452635608277857713427577896091736371787214684409012249534301465495853710507922796892589235420199561121290219608640344181598136297747713099605187072113499999983729780499510597317328160963185950244594553469083026425223082533446850352619311881710100031378387528865875332083814206171776691473035982534904287554687311595628638823537875937519577818577805321712268066130019278766111959092164201989";
        public readonly string halfPi = "1,57079632679489661923132169163975144209858469968755291048747229615390820314310449931401741267105853399107404325664115332354692230477529111586267970406424055872514205135096926055277982231147447746519098221440548783296672306423782411689339158263560095457282428346173017430522716332410669680363012457063686229350330315779408744076046048141462704585768218394629518000566526527441023326069207347597075580471652863518287979597654609305869096630589655255927403723118998137478367594287636244561396909150597456491683668122032832154301069747319761236859535108993047185138526960858814658837619233740923383470256600028406357263178041389288567137889480458681858936073422045061247671507327479268552539613984462946177100997805606451098043201720907990681488738565498025935360567499999918648902497552986586640804815929751222972767345415132126115412667234251763096559408550500156891937644329376660419071030858883457365179912674521437773436557978143194117689379687597889092889026608561340330650096393830559795460821009945";

        public string ArcTan(string x, uint n) {
            x = FixNumber(x);
            char sign = Sign(x);
            x = Abs(x);
            char comp = Comp(x, "1");
            string arctan = string.Empty;
            uint userPrecision = precision;

            if (comp == 'l') { // if x < 1
                precision = (uint)(userPrecision * 1.5);
                bool a = false;
                arctan = x;
                uint counter = 3;
                string pow = Pow(x, "3");
                for (uint i = 0; i < n; i++) {
                    if (a)
                        arctan = Add(arctan, Div(pow, counter.ToString()));
                    else
                        arctan = Sub(arctan, Div(pow, counter.ToString()));
                    a = !a;
                    counter += 2;
                    pow = Mult(pow, Sqr(x));
                }
            } else if (comp == 'g') { // if x > 1
                precision = (uint)(userPrecision * 1.5);
                x = Div("1", x);
                bool a = false;
                arctan = x;
                uint counter = 3;
                string pow = Pow(x, "3");
                //Console.WriteLine("0/" + n.ToString());
                for (uint i = 0; i < n; i++) {
                    if (a)
                        arctan = Add(arctan, Div(pow, counter.ToString()));
                    else
                        arctan = Sub(arctan, Div(pow, counter.ToString()));
                    a = !a;
                    counter += 2;
                    pow = Mult(pow, Sqr(x));
                    //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    //Console.WriteLine("               ");
                    //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    //Console.WriteLine((i+1).ToString() + " / " + n.ToString());
                }
                arctan = Sub(Constrain(halfPi), arctan);
            } else
                return sign == 'p' ? // if x == 1
                    Div(Constrain(halfPi), "2") :
                    "-" + Div(Constrain(halfPi), "2");

            precision = userPrecision;
            return sign == 'p' ? Constrain(arctan) : "-" + Constrain(arctan);
        }

        public string ArcCos(string x, uint n) {
            return Sub(Constrain(halfPi), ArcSin(x, n));
        }

        /// <summary>
        /// The arcsine. The result is in radians.
        /// <para>The closer to 1, the less accurate the result:</para>
        /// <para><c>0.9...1: 0.99, 0.999 ...</c></para>
        /// </summary>
        /// <returns>The arcsine in radians</returns>
        /// <exception cref="Exception"></exception>
        public string ArcSin(string x, uint n) {
            x = FixNumber(x);

            char comp = Comp(Abs(x), "1");
            char sign = Sign(x);

            if (comp == 'g')
                throw new Exception("The arcsine of a number greater than 1.");

            if (comp == 'e' && precision <= 1000)
                return sign == 'p' ? Constrain(Div(pi, "2")) : "-" + Constrain(Div(pi, "2"));

            uint userPrecision = precision;
            precision = (uint)(userPrecision * 1.5);

            // OLD ALGO
            //string fact = "1"; // "1" (2)
            //string fact2 = "2"; // "1" (2)
            //string arcsin = x; // "0" (1)
            //string numerator = string.Empty;
            //string fourN = string.Empty;
            //string factSqr = string.Empty;
            //string denominator = string.Empty;
            //for (uint i = 2; i <= count * 2; i++) {
            //    numerator = Mult(fact2, Pow(x, (i * 2 - 1).ToString()));
            //    fourN = Pow("4", (i - 1).ToString()); // pox
            //    factSqr = Sqr(fact); // pox
            //    denominator = Mult(Mult(fourN, factSqr), (2 * i - 1).ToString());
            //    arcsin = Add(arcsin, Div(numerator, denominator));
            //    fact2 = Mult(fact2, (i * 2 - 1).ToString());
            //    fact2 = Mult(fact2, (i * 2).ToString());
            //    fact = Mult(fact, i.ToString());
            //}

            string numerator = x;
            string arcsin = x;
            for (uint i = 1; i <= n * 4; i++) {
                numerator = Div(Mult(Sqr(x), Mult(numerator, (i * 2 - 1).ToString())), (i * 2).ToString());
                arcsin = Add(arcsin, Div(numerator, (i * 2 + 1).ToString()));
            }

            precision = userPrecision;
            return Constrain(arcsin);
        }

        public string Tan(string x, uint n) {
            string sin = Sin(x, n);
            string cos = Cos(x, n);
            return Div(sin, cos);
        }

        public string Cos(string x, uint n) {
            x = FixNumber(x);
            uint userPrecision = precision;
            precision = userPrecision * 2;
            string fact = "1";
            string cos = "1";
            bool sign = true;
            for (uint i = 2; i <= n; i++) {
                fact = Mult(fact, i.ToString());
                if (i % 2 == 0) {
                    if (sign)
                        cos = Sub(cos, Div(Pow(x, i.ToString()), fact));
                    else
                        cos = Add(cos, Div(Pow(x, i.ToString()), fact));
                    sign = !sign;
                }
            }
            precision = userPrecision;
            return Constrain(cos);
        }

        public string Sin(string x, uint n) {
            x = FixNumber(x);
            uint userPrecision = precision;
            precision = userPrecision * 2;
            string fact = "1";
            string sin = "0";
            bool sign = true;
            for (uint i = 1; i <= n; i++) {
                fact = Mult(fact, i.ToString());
                if (i % 2 != 0) {
                    if (sign)
                        sin = Add(sin, Div(Pow(x, i.ToString()), fact));
                    else
                        sin = Sub(sin, Div(Pow(x, i.ToString()), fact));
                    sign = !sign;
                }
            }
            precision = userPrecision;
            return Constrain(sin);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Square root of negative number</exception>
        public string Sqrt(string x) {
            x = FixNumber(x);
            if (Sign(x) == 'n')
                throw new Exception(String.Format("Square root of negative number {0}\n>>> Sqrt() <<<", x));
            return Root(x, "2");
        }

        public string Root(string x, string power) {
            x = FixNumber(x);
            power = FixNumber(power);

            //var watch = System.Diagnostics.Stopwatch.StartNew();

            char sign = Sign(power);
            power = Abs(power);

            char signx = Sign(x);
            char compx = Comp("1", Abs(x));

            if (signx == 'p' && compx == 'e') // if x=1
                return "1";
            if (signx == 'n' && compx == 'e') { // if x=-1
                if (Comp(EuDiv(power, "2")[1], "0") == 'e') // if power is even
                    throw new Exception(String.Format(
                    "Even power root ({0}) of the negative number ({1})", power, x));
                else
                    return "-1";
            }

            if (Sign(x) == 'n' && Comp(EuDiv(power, "2")[1], "0") == 'e') // if x<0 and power % 2 == 0
                throw new Exception(String.Format(
                    "Even power root ({0}) of the negative number ({1})", power, x));
            if (Comp(x, "0") == 'e')
                return "0";

            uint userPrecision = precision;
            precision = userPrecision * 2;

            string root = Div(x, power);
            string power1 = Sub(power, "1");
            string a = Div("1", power);

            string t = x;
            string b = string.Empty;
            string c = string.Empty;
            string d = string.Empty;
            string e = string.Empty;
            string rootPrecision = "0," + new string('0', (int)userPrecision) + "1";
            //while (Comp(Pow(root, power), x) == 'g') {
            while (Comp(Sub(t, root), rootPrecision) != 'l') {
                t = root; // t>root

                //Task dt = Task.Run(() => { d = Mult(power1, t); });
                d = Mult(power1, t);

                b = Pow(t, power1);
                c = Div(x, b);
                //while (!dt.IsCompleted) { }
                e = Add(d, c);
                root = Mult(a, e);
                //char tRoot = Sign(Sub(t, root));
            }
            //watch.Stop();
            //Console.WriteLine(watch.ElapsedMilliseconds.ToString());

            precision = userPrecision;
            root = FixNumber(root);
            root = Constrain(root);

            if (sign == 'p')
                return root;
            else
                return Div("1", root);
        }

        /// <summary>
        /// Raises x to the power of 2
        /// </summary>
        /// <returns>x^2</returns>
        public string Sqr(string x) {
            return Pow(x, "2");
        }

        /// <summary>
        /// Raises x to the power of <c>power</c>
        /// </summary>
        /// <returns>x^{power}</returns>
        public string Pow(string x, string power) {
            x = FixNumber(x);
            power = FixNumber(power);

            if (Comp(x, "0") == 'e')
                return "0";
            char sign = Sign(power);
            power = Abs(power);

            string result = "1";

            if (IsInteger(power)) {
                for (uint i = 0; i < Int64.Parse(power); i++) {
                    result = Mult(result, x);
                }
            } else {
                string[] fracPower = ToDecimalFraction(power);
                for (uint i = 0; i < Int64.Parse(fracPower[0]); i++) {
                    result = Mult(result, x);
                }
                result = Root(result, fracPower[1]);
            }
            if (sign == 'p')
                return result;
            else
                return Div("1", result);
        }

        /// <summary>
        /// Euclidean division – or division with remainder
        /// </summary>
        /// <returns>string[2] = { integer_part, remainder }</returns>
        /// <exception cref="DivideByZeroException">Division of x by zero</exception>
        public string[] EuDiv(string x, string y) {
            x = Normalize(x);
            y = Normalize(y);

            if (Comp(y, "0") == 'e')
                throw new DivideByZeroException(String.Format("Division of {0} by zero.\n>>> EuDiv() <<<", x));

            string integer = "0";
            while (Comp(x, y) == 'g' || Comp(x, y) == 'e') {
                integer = Add(integer, "1");
                x = Sub(x, y);
            }
            return new string[] { integer, x };
        }

        /// <summary>
        /// Usual division. Before using, set the precision property (number of decimal places)
        /// </summary>
        /// <returns>(x / y) with the specified number of decimal places.</returns>
        /// <exception cref="DivideByZeroException">Division of x by zero</exception>
        public string Div(string x, string y) {
            x = FixNumber(x);
            y = FixNumber(y);

            string preproc = Route(x, y, "/");
            if (!string.IsNullOrEmpty(preproc))
                return preproc;

            x = Abs(x);
            y = Abs(y);

            if (Comp(x, "0") == 'e')
                return "0";
            if (Comp(y, "0") == 'e')
                throw new DivideByZeroException(String.Format("Division of {0} by zero.\n>>> Div() <<<", x));

            string result = string.Empty;
            string integer = "0";
            while (Comp(x, y) == 'g' || Comp(x, y) == 'e') {
                integer = Add(integer, "1");
                x = Sub(x, y);
            }

            result = integer + ",";
            if (Comp(x, "0") == 'e')
                return FixNumber(result);

            List<string[]> list = Prepare(x, y);
            x = list[0][0] + list[0][1];
            y = list[1][0] + list[1][1];
            x = x.TrimStart('0');
            y = y.TrimStart('0');

            uint precisionCounter = 0;
            while (precisionCounter <= precision) {
                x = x + "0";
                string[] temp = EuDiv(x, y);
                char comp = Comp(temp[0], "1"); // if more or equal
                if (comp == 'g' || comp == 'e') {
                    result += temp[0];
                    x = temp[1];
                    precisionCounter++;
                } else {
                    result += "0";
                    precisionCounter++;
                }
                if (Comp(temp[1], "0") == 'e')
                    break;
            }
            return FixNumber(result);
        }
        
        /// <summary>
        /// Compares x and y values. Warning: x and y must be >= 0
        /// </summary>
        /// <returns>
        /// <list type="bullet|number|table">
        ///     <item>
        ///         <term>char 'e'</term>
        ///         <description> if x == y</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'g'</term>
        ///         <description> if x greater than y</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'l'</term>
        ///         <description> if x less than y</description>
        ///     </item>
        /// </list>
        /// </returns>
        public char Comp(string x, string y) {
            x = FixNumber(x);
            y = FixNumber(y);

            string[] xs = x.Split(',');
            string[] ys = y.Split(',');
            if (xs[0].Length > ys[0].Length)
                return 'g'; // greater
            else if (ys[0].Length > xs[0].Length)
                return 'l'; // less
            else {
                for (int i = 0; i < xs[0].Length; i++) { // 9998. and 9999.
                    if (Int32.Parse(xs[0][i].ToString()) > Int32.Parse(ys[0][i].ToString()))
                        return 'g'; // greater
                    if (Int32.Parse(xs[0][i].ToString()) < Int32.Parse(ys[0][i].ToString()))
                        return 'l'; // less
                }
                List<string[]> list = Prepare(x, y);
                xs = list[0];
                ys = list[1];
                for (int i = 0; i < xs[1].Length && i < ys[1].Length; i++) {
                    if (Int32.Parse(xs[1][i].ToString()) > Int32.Parse(ys[1][i].ToString()))
                        return 'g'; // greater
                    if (Int32.Parse(xs[1][i].ToString()) < Int32.Parse(ys[1][i].ToString()))
                        return 'l'; // less
                }
            }
            return 'e';
        }
        
        /// <summary>
        /// Multiplies x and y
        /// </summary>
        /// <returns>(x * y)</returns>
        public string Mult(string x, string y) {
            x = FixNumber(x);
            y = FixNumber(y);
            string preproc = Route(x, y, "*");
            if (!string.IsNullOrEmpty(preproc))
                return preproc;

            x = Abs(x);
            y = Abs(y);

            string result = string.Empty;
            string[] xs = x.Split(',');
            string[] ys = y.Split(',');
            int pointPosition = 0;
            if (xs.Length > 1) {
                pointPosition += xs[1].Length;
                x = xs[0] + xs[1];
            }
            if (ys.Length > 1) {
                pointPosition += ys[1].Length;
                y = ys[0] + ys[1];
            }
            int[] A = String2IntArray(x);
            int[] B = String2IntArray(y);
            int[] C = new int[A.Length + B.Length + 1];

            for (int i = 0; i < A.Length; i++) {
                for (int j = 0; j < B.Length; j++) {
                    C[j + i] += A[i] * B[j];
                }
            }
            for (int i = 0; i < C.Length - 1; i++) {
                C[i + 1] += C[i] / 10;
                C[i] %= 10;
            }

            for (int i = C.Length - 1; i >= 0; i--) {
                result += C[i].ToString();
            }
            result = result.Substring(0, result.Length - pointPosition) + "," +
                result.Substring(result.Length - pointPosition, pointPosition);

            return FixNumber(result);
        }
        
        /// <summary>
        /// Subtract y from x
        /// </summary>
        /// <returns>(x - y)</returns>
        public string Sub(string x, string y) {
            x = FixNumber(x);
            y = FixNumber(y);
            string preproc = Route(x, y, "-");
            if (!string.IsNullOrEmpty(preproc))
                return preproc;

            string result = string.Empty;
            List<string[]> list = Prepare(x, y);
            string xs = list[0][0] + list[0][1];
            string ys = list[1][0] + list[1][1];
            int pointPosition = list[0][1].Length;
            for (int i = xs.Length - 1; i >= 0; i--) {
                int diff = Int32.Parse(xs[i].ToString()) - Int32.Parse(ys[i].ToString());
                if (diff >= 0) {
                    result = diff.ToString() + result;
                } else {
                    for (int l = i - 1; l >= 0; l--) {
                        if (xs[l] != '0') {
                            xs = xs.Substring(0, l) +
                                (Int32.Parse(xs[l].ToString()) - 1).ToString() +
                                xs.Substring(l + 1, xs.Length - l - 1);
                            break;
                        } else {
                            xs = xs.Substring(0, l) + "9" + xs.Substring(l + 1, xs.Length - l - 1);
                        }
                    }
                    result = (Int32.Parse(xs[i].ToString()) + 10 - Int32.Parse(ys[i].ToString())).ToString() + result;
                }
            }
            result = result.Substring(0, result.Length - pointPosition) + "," +
                result.Substring(result.Length - pointPosition, pointPosition);

            return FixNumber(result);
        }
        
        /// <summary>
        /// Adds x and y
        /// </summary>
        /// <returns>(x + y)</returns>
        public string Add(string x, string y) {
            x = FixNumber(x);
            y = FixNumber(y);
            string preproc = Route(x, y, "+");
            if (!string.IsNullOrEmpty(preproc))
                return preproc;

            string result = string.Empty;
            List<string[]> list = Prepare(x, y);
            string[] xs = list[0];
            string[] ys = list[1];
            int pointPosition = xs[1].Length;

            int remainder = 0;
            //var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = xs[1].Length - 1; i >= 0; i--) {
                string tempRes = (Int32.Parse(xs[1][i].ToString()) +
                    Int32.Parse(ys[1][i].ToString()) + remainder).ToString();
                if (tempRes.Length > 1) {
                    result = tempRes[1] + result;
                    remainder = Int32.Parse(tempRes[0].ToString());
                } else {
                    result = tempRes + result;
                    remainder = 0;
                }
            }
            result = "," + result;
            for (int i = xs[0].Length - 1; i >= 0; i--) {
                string tempRes = (Int32.Parse(xs[0][i].ToString()) +
                    Int32.Parse(ys[0][i].ToString()) + remainder).ToString();
                if (tempRes.Length > 1) {
                    result = tempRes[1] + result;
                    remainder = Int32.Parse(tempRes[0].ToString());
                } else {
                    result = tempRes + result;
                    remainder = 0;
                }
            }
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed.TotalMilliseconds.ToString());
            result = remainder.ToString() + result;
            //result = result.Substring(0, result.Length - pointPosition) + "," +
            //    result.Substring(result.Length - pointPosition, pointPosition);
            return FixNumber(result);
        }

        /// <summary>
        /// Sign of x
        /// </summary>
        /// <returns>
        /// <list type="bullet|number|table">
        ///     <item>
        ///         <term>char 'p'</term>
        ///         <description> if x greater than zero</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'n'</term>
        ///         <description> if x less than zero</description>
        ///     </item>
        /// </list>
        /// </returns>
        public char Sign(string x) {
            if (x[0] == '-')
                return 'n';
            else
                return 'p';
        }

        /// <summary>
        /// Absolute value of x
        /// </summary>
        /// <returns>Absolute value of x</returns>
        public string Abs(string x) {
            if (Sign(x) == 'n')
                return x.Remove(0, 1);
            else
                return x;
        }

        /// <summary>
        /// Returns a value indicating whether a number is integer
        /// </summary>
        /// <returns>true if the x is integer; otherwise, false.</returns>
        public bool IsInteger(string x) {
            x = FixNumber(x);
            if (x.Contains(','))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public string FixNumber(string x) {
            x = Normalize(x);
            x = FixNegZero(x); // for case "-0"
            x = Trim(x); // for case "0,0" or "0,"
            return x;
        }

        /// <summary>
        /// <para>Returns simple (aka common or vulgar) fraction</para>
        /// <para>Examples:</para>
        /// <para>ToSimple("1,2") -> { "12", "10" }</para>
        /// <para>ToSimple("0,03") -> { "3", "100" }</para>
        /// </summary>
        /// <returns>simple (aka common or vulgar) fraction</returns>
        public string[] ToDecimalFraction(string x) {
            if (IsInteger(x))
                return new string[] { FixNumber(x), "1" };

            x = FixNumber(x);
            string[] xs = x.Split(',');
            return new string[] { (xs[0] + xs[1]), Pow("10", xs[1].Length.ToString()) };
        }

        public string Constrain(string x) {
            x = FixNumber(x);
            if (IsInteger(x))
                return x;
            else {
                string[] xs = x.Split(',');
                if (xs[1].Length <= precision)
                    return x;
                else {
                    return FixNegZero(xs[0] + "," + xs[1].Remove((int)precision, xs[1].Length - (int)precision));
                }
            }
        }

        // ======== PRIVATE METHODS BELOW ========

        /// <summary>
        /// Converts a string to an array of digits
        /// </summary>
        /// <returns>Array of digits</returns>
        private int[] String2IntArray(string x) {
            List<int> result = new List<int>();
            for (int i = x.Length - 1; i >= 0; i--) {
                result.Add(Int32.Parse(x[i].ToString()));
            }
            return result.ToArray();
        }

        /// <summary>
        /// Determines which number x or y is negative
        /// </summary>
        /// <returns>
        /// <list type="bullet|number|table">
        ///     <item>
        ///         <term>char 'x'</term>
        ///         <description> if x less than zero and y greater than zero</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'y'</term>
        ///         <description> if x greater than zero and y less than zero</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'b'</term>
        ///         <description> both x and y less than zero</description>
        ///     </item>
        ///     <item>
        ///         <term>char 'n'</term>
        ///         <description> none of both x and y less than zero</description>
        ///     </item>
        /// </list>
        /// </returns>
        private char WhoIsNegative(string x, string y) {
            char xs = Sign(x);
            char ys = Sign(y);
            if (xs == 'n' && ys == 'n')
                return 'b'; // both
            else if (xs == 'n' && ys == 'p')
                return 'x'; // x
            else if (xs == 'p' && ys == 'p')
                return 'n'; // nobody
            else
                return 'y'; // y
        }
        
        /// <summary>
        /// Fixes "-0" to "0"
        /// </summary>
        /// <returns>
        /// "0" if x is ("-0" or "0"), else x
        /// </returns>
        private string FixNegZero(string x) {
            if (Sign(x) == 'n' && Comp(Abs(x), "0") == 'e')
                return "0";
            else
                return x;
        }

        /// <summary>
        /// <para>Converts a number from an exponential form to a normal one.</para>
        /// <para>Examples:</para>
        /// <para>Normalize("2.2e4") or Normalize("2.2E4") -> "2200"</para>
        /// <para>Normalize("3.3e-4") or Normalize("3.3E-4") -> "0,00033"</para>
        /// </summary>
        /// <returns>Converted (aka Normalized) x</returns>
        private string Normalize(string x) {
            string[] xse = x.Split('e');
            string[] xsE = x.Split('E');
            if (xse.Length > 1) {
                if (Sign(xse[1]) == 'n')
                    return Div(xse[0], Pow("10", Abs(xse[1])));
                else
                    return Mult(xse[0], Pow("10", xse[1]));
            } else if (xsE.Length > 1) {
                if (Sign(xsE[1]) == 'n')
                    return Div(xsE[0], Pow("10", Abs(xsE[1])));
                else
                    return Mult(xsE[0], Pow("10", xsE[1]));
            } else
                return x;
        }

        /// <summary>
        /// Some shit
        /// </summary>
        /// <returns>Some shit, sometimes the result</returns>
        /// <exception cref="Exception">There is no such arithmetic operation here!</exception>
        private string Route(string x, string y, string operation) {
            char who = WhoIsNegative(x, y);

            if (who == 'x' || who == 'b')
                x = Abs(x);
            if (who == 'y' || who == 'b')
                y = Abs(y);

            char comp = Comp(x, y);
            string empty = string.Empty;

            if (operation == "+") {
                if (who == 'b') { // x<0 and y<0
                    return "-" + Add(x, y);
                } else if (who == 'x') { // x<0
                    if (comp == 'g') // -100 + 20
                        return "-" + Sub(x, y);
                    if (comp == 'l') // -20 + 100
                        return Sub(y, x);
                    if (comp == 'e') // -20 + 20
                        return "0";
                    else
                        return empty;
                } else if (who == 'y') { // y<0
                    if (comp == 'e') // 100 + -100
                        return "0";
                    if (comp == 'l') // 20 + -100
                        return "-" + Sub(y, x);
                    if (comp == 'g') // 100 + -20
                        return Sub(x, y);
                    else
                        return empty;
                } else
                    return empty;
            } else if (operation == "-") {
                if (who == 'b') { // x<0 and y<0
                    if (comp == 'e') // -100 - -100
                        return "0";
                    if (comp == 'l') // -20 - -100
                        return Sub(y, x);
                    if (comp == 'g') // -100 - -20
                        return "-" + Sub(x, y);
                    else
                        return empty;
                } else if (who == 'x') { // x<0
                    return "-" + Add(x, y);
                } else if (who == 'y') { // y<0
                    return Add(x, y);
                } else if (who == 'n') {
                    if (comp == 'e') // 100 - 100
                        return "0";
                    if (comp == 'l') // 20 - 100
                        return "-" + Sub(y, x);
                    else // 100 - 20
                        return empty;
                } else
                    return empty;
            } else if (operation == "*") {
                if (who == 'b' || who == 'n') // -120 * -3 or 120 * 3
                    return "";
                else // -120 * 3 or 120 * -3
                    return "-" + Mult(x, y);
            } else if (operation == "/") {
                if (who == 'b' || who == 'n') // -120 / -3 or 120 / 3
                    return "";
                else // -120 / 3 or 120 / -3
                    return "-" + Div(x, y);
            } else
                throw new Exception("There is no such arithmetic operation here!\n>>> Route() <<<");
        }

        /// <summary>
        /// Removes all leading and trailing non-significant zeros from the current number.
        /// </summary>
        /// <returns>string castratedNumber</returns>
        private string Trim(string s) {
            string result = string.Empty;
            char sign = Sign(s);
            if (sign == 'n') {
                s = Abs(s);
            }
            string[] ss = s.Split(',');
            ss[0] = ss[0].TrimStart('0');

            if (ss.Length > 1) {
                ss[1] = ss[1].TrimEnd('0');
                if (ss[0].Length > 0 && ss[1].Length > 0)
                    result = ss[0] + "," + ss[1];
                else if (ss[0].Length > 0 && ss[1].Length == 0)
                    result = ss[0];
                else if (ss[0].Length == 0 && ss[1].Length == 0)
                    result = "0";
                else if (ss[0].Length == 0 && ss[1].Length > 0)
                    result = "0," + ss[1];
            } else if (ss.Length == 1) {
                if (ss[0].Length == 0)
                    result = "0";
                else
                    result = ss[0];
            }
            if (sign == 'n')
                result = "-" + result;
            return result;
        }
        
        /// <summary>
        /// Some kind of shit for internal use only.
        /// </summary>
        /// <returns>Who knows..?</returns>
        private List<string[]> Prepare(string x, string y) {
            /// <summary>
            /// Only positive numbers!
            /// </summary>
            string[] xs = x.Split(',').Length > 1 ? x.Split(',') : new string[] { x, string.Empty };
            string[] ys = y.Split(',').Length > 1 ? y.Split(',') : new string[] { y, string.Empty };

            if (xs[1].Length > ys[1].Length) {
                ys[1] = ys[1] + new string('0', xs[1].Length - ys[1].Length);
            } else if (ys[1].Length > xs[1].Length) {
                xs[1] = xs[1] + new string('0', ys[1].Length - xs[1].Length);
            }
            if (xs[0].Length > ys[0].Length) {
                ys[0] = new string('0', xs[0].Length - ys[0].Length) + ys[0];
            } else if (ys[0].Length > xs[0].Length) {
                xs[0] = new string('0', ys[0].Length - xs[0].Length) + xs[0];
            }
            return new List<string[]> { xs, ys };
        }
    }
}
