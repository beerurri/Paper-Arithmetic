# Paper Arithmetic
Paper Arithmetic (C#/.NET), something like 'Big Numbers'

Performs arithmetic operations on numbers that are stored in string variables (<b><i>hence, can be as large and accurate as you like</i></b>).

You can find an example usage in <code>Program.cs</code>, all arithmetic methods are placed in a separate <code>Arithmetic</code> class in <code>Arithmetic.cs</code>

A comma <code>,</code> is used everywhere as a separator between integer part and fractional part.
<hr>
<h2>Arithmetic methods:</h2>

<code>string Add(string x, string y)</code> – Adds <code>x</code> and <code>y</code>

<code>string Sub(string x, string y)</code> – Subtract <code>y</code> from <code>x</code>

<code>string Mult(string x, string y)</code> – Multiplies <code>x</code> and <code>y</code>

<code>string Div(string x, string y)</code> – Usual division: (<code>x÷y</code>). <i>Before using, set the <code>[Arithmetic].precision</code> property (number of decimal places)</i>
- exception: <code>DivideByZeroException</code> Division of <code>x</code> by zero

<code>string[] EuDiv(string x, string y)</code> – Euclidean division – or division with remainder
- returns: <code>string[2] = { integer_part, remainder }</code>
- exception: <code>DivideByZeroException</code> Division of <code>x</code> by zero

<code>string Pow(string x, string power)</code> – Raises <code>x</code> to the power of <code>power</code>

<code>string Sqr(string x)</code> – Raises <code>x</code> to the power of <code>2</code>

<code>string Root(string x, string power)</code> – Root of <code>x</code> degree <code>power</code>

<code>string Sqrt(string x)</code> – Square root of <code>x</code>

<hr>

<code>string Abs(string x)</code> – Absolute value of <code>x</code>

<code>char Sign(string x)</code> – Sign of <code>x</code>
- returns <code>(char)'p'</code> if <code>x</code> greater than zero
- returns <code>(char)'n'</code> if <code>x</code> less than zero

<code>char Comp(string x, string y)</code> – Compares <code>x</code> and <code>y</code> values. <i>WARNING: <code>x</code> and <code>y</code> must be <code>>= 0</code></i>
- returns <code>(char)'e'</code>: if x equal y
- returns <code>(char)'g'</code>: if x greater than y
- returns <code>(char)'l'</code>: if x less than y

<code>string[] ToDecimalFraction(string x)</code> – Returns simple (aka common or vulgar) fraction. Like so:
- <code>ToSimple("1,2")</code> -> <code>{ "12", "10" }</code>
- <code>ToSimple("0,03")</code> -> <code>{ "3", "100" }</code>

<hr>

<code>string FixNumber(string x)</code> – Fixes the number. Replaces <code>.</code> to <code>,</code> and removes non-significant zeros.

<code>bool IsInteger(string x)</code> – Returns a value indicating whether a number is integer

<h2>Trigonometric methods (<i>radians only</i>)</h2>
Methods below use series. <code>(uint) n</code> is the number of members of the series.

<code>string Sin(string x, uint n)</code>

<code>string Cos(string x, uint n)</code>

<code>string Tan(string x, uint n)</code>

<code>string ArcSin(string x, uint n)</code>

<code>string ArcCos(string x, uint n)</code>

<code>string ArcTan(string x, uint n)</code>
