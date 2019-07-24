using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SuperKonsole
{
    /// <summary>
    /// An easy to use colored <see cref="Console"/> helper class
    /// Use with: <see langword="using"/> Console = <see cref="SuperKonsole"/>
    /// </summary>
    public class SuperKonsole
    {
        /// <summary>
        /// Color change stack to keep track of colors
        /// </summary>
        private static Stack<ConsoleColor> ColorStack = new Stack<ConsoleColor>();

        // Used to search for colors based on their names
        private static Dictionary<string, ConsoleColor> colorDict = new Dictionary<string, ConsoleColor>() {
            { "black", ConsoleColor.Black },        { "darkblue", ConsoleColor.DarkBlue },          { "darkgreen", ConsoleColor.DarkGreen },     { "darkcyan", ConsoleColor.DarkCyan },
            { "darkred", ConsoleColor.DarkRed },    { "darkmagenta", ConsoleColor.DarkMagenta },    { "darkyellow", ConsoleColor.DarkYellow },   { "gray", ConsoleColor.Gray },
            { "darkgray", ConsoleColor.DarkGray },  { "blue", ConsoleColor.Blue },                  { "green", ConsoleColor.Green },             { "cyan", ConsoleColor.Cyan },
            { "red", ConsoleColor.Red },            { "magenta", ConsoleColor.Magenta },            { "yellow", ConsoleColor.Yellow },           { "white", ConsoleColor.White },
        };

        /// <summary>
        /// Gets or sets current text color
        /// </summary>
        public static ConsoleColor TextColor { get { return Console.ForegroundColor; } set { Console.ForegroundColor = value; } }

        /// <summary>
        /// Gets or sets current background color
        /// </summary>
        public static ConsoleColor BackColor { get { return Console.BackgroundColor; } set { Console.BackgroundColor = value; } }

        /// <summary>
        /// Gets or sets console window title
        /// </summary>
        public static string Title { get { return Console.Title; } set { Console.Title = value; } }

        /// <summary>
        /// Gets or sets standard console error stream
        /// </summary>
        public static TextWriter Error { get { return Console.Error; } set { Console.SetError(value); } }

        /// <summary>
        /// Gets or sets standard console output stream
        /// </summary>
        public static TextWriter Out { get { return Console.Out; } set { Console.SetOut(value); } }

        /// <summary>
        /// Gets or sets standard console input stream
        /// </summary>
        public static TextReader In { get { return Console.In; } set { Console.SetIn(value); } }

        /// <summary>
        /// Write a colored, formatted and line breaked message to standard output stream
        /// </summary>
        /// <param name="msg">Message to write</param>
        /// <param name="format">Format parameters</param>
        public static void WriteLine(object msg, params object[] format)
        {
            var sect = GenerateSections(msg.ToString());
            int f = 0;
            foreach (var s in sect)
            {
                // Hello, &cyan;world&back;!
                if (s.ColorChange)
                {
                    ColorStack.Push(TextColor);
                    TextColor = s.Color;
                }

                int b = s.Back;
                while ((b--) > 0 && ColorStack.Count > 0)
                    TextColor = ColorStack.Pop();

                // Get format
                var fmt = new object[s.Formats];
                for (int i = 0; i < fmt.Length && f < format.Length; i++, f++)
                    fmt[i] = format[f];

                var newmsg = FixFormats(s);
                Console.Write(newmsg, fmt);
            }
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Write a colored and formatted message to standard output stream
        /// </summary>
        /// <param name="msg">Message to write</param>
        /// <param name="format">Format parameters</param>
        public static void Write(object msg, params object[] format)
        {
            var sect = GenerateSections(msg.ToString());
            int f = 0;
            foreach (var s in sect)
            {
                // Hello, &cyan;world&back;!
                if (s.ColorChange)
                {
                    ColorStack.Push(TextColor);
                    TextColor = s.Color;
                }

                int b = s.Back;
                while ((b--) > 0 && ColorStack.Count > 0)
                    TextColor = ColorStack.Pop();

                // Get format
                var fmt = new object[s.Formats];
                for (int i = 0; i < fmt.Length && f < format.Length; i++, f++)
                    fmt[i] = format[f];

                var newmsg = FixFormats(s);
                Console.Write(newmsg, fmt);
            }
        }

        /// <summary>
        /// Write a colored and line breaked message to standard output stream
        /// </summary>
        /// <param name="msg">Message to write</param>
        public static void WriteLine(object msg)
        {
            var sect = GenerateSections(msg.ToString());
            int f = 0;
            foreach (var s in sect)
            {
                // Hello, &cyan;world&back;!
                if (s.ColorChange)
                {
                    ColorStack.Push(TextColor);
                    TextColor = s.Color;
                }

                int b = s.Back;
                while ((b--) > 0 && ColorStack.Count > 0)
                    TextColor = ColorStack.Pop();

                Console.Write(s.Message);
            }
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Write a colored message to standard output stream
        /// </summary>
        /// <param name="msg">Message to write</param>
        public static void Write(object msg)
        {
            var sect = GenerateSections(msg.ToString());
            int f = 0;
            foreach (var s in sect)
            {
                // Hello, &cyan;world&back;!
                if (s.ColorChange)
                {
                    ColorStack.Push(TextColor);
                    TextColor = s.Color;
                }

                int b = s.Back;
                while ((b--) > 0 && ColorStack.Count > 0)
                    TextColor = ColorStack.Pop();

                Console.Write(s.Message);
            }
        }

        /// <summary>
        /// Clears console buffer
        /// </summary>
        public static void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Resets console color back to original
        /// </summary>
        public static void ResetColor()
        {
            Console.ResetColor();
            ColorStack.Clear();
        }

        /// <summary>
        /// Reads key obtained from user
        /// </summary>
        /// <param name="intercept">If set to true, character will not be displayed on console</param>
        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        /// <summary>
        /// Reads next line obtained from user
        /// </summary>
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Reads next character obtained from user
        /// </summary>
        public int Read()
        {
            return Console.Read();
        }

        /// <summary>
        /// Changes console standard error stream to specified stream
        /// </summary>
        /// <param name="error">Stream to write on</param>
        public void SetError(TextWriter error)
        {
            Console.SetError(error);
        }

        /// <summary>
        /// Changes console standard output stream to specified stream
        /// </summary>
        /// <param name="output">Stream to write on</param>
        public void SetOut(TextWriter output)
        {
            Console.SetOut(output);
        }

        /// <summary>
        /// Changes console standard input stream to specified stream
        /// </summary>
        /// <param name="input">Stream to read from</param>
        public void SetIn(TextReader input)
        {
            Console.SetIn(input);
        }

        /// <summary>
        /// Tries to find a <see cref="ConsoleColor"/> based on color name
        /// </summary>
        /// <param name="name">Color to find</param>
        /// <param name="c">Output parameter</param>
        public static bool TryFindColor(string name, out ConsoleColor c)
        {
            return colorDict.TryGetValue(name, out c);
        }

        /// <summary>
        /// Tries to find a <see cref="ConsoleColor"/> based on color code
        /// </summary>
        /// <param name="code">Color to find</param>
        /// <param name="c">Output parameter</param>
        public static bool TryFindColor(int code, out ConsoleColor c)
        {
            c = ConsoleColor.Gray;
            if (code < 0 || code > 15)
                return false;
            c = (ConsoleColor)code;
            return true;
        }

        /// <summary>
        /// Generates colored message sections based on given message string
        /// </summary>
        /// <param name="msg">Message string to generate sections</param>
        public static List<KolorSection> GenerateSections(string msg)
        {
            var lst = new List<KolorSection>();
            KolorSection sect = new KolorSection("");

            var ch = ' ';
            var i = 0;
            StringBuilder part = new StringBuilder();

            while (i < msg.Length)
            {
                ch = msg[i++];

                if (ch == '{') // A format part
                {
                    if (i < msg.Length && msg[i] == '{') // Format skip
                    {
                        i++;
                        part.Append("{{");
                        continue;
                    }
                    sect.Formats++;
                }

                if (ch == '&') // A new section
                {
                    if (i < msg.Length && msg[i] == '&') // A '&' escape
                    {
                        i++;
                        part.Append('%');
                        continue;
                    }

                    sect.Message = part.ToString();
                    part = new StringBuilder();
                    lst.Add(sect);

                    string cl = "";
                    while (i < msg.Length && (ch = msg[i]) != ';')
                        cl += msg[i++];
                    cl = cl.Trim();

                    if (cl != "back")
                    {
                        if (!TryFindColor(cl, out ConsoleColor color))
                            color = ConsoleColor.Gray; // fallback to gray

                        sect = new KolorSection("");
                        sect.Color = color;
                        sect.ColorChange = true;
                    }
                    else
                    {
                        sect = new KolorSection("");
                        sect.Back = 1;
                    }

                    i++;
                    continue;
                }

                if (ch == '%') // A new section
                {
                    if (i < msg.Length && msg[i] == '%') // A '%' escape
                    {
                        i++;
                        part.Append('%');
                        continue;
                    }

                    sect.Message = part.ToString();
                    part = new StringBuilder();
                    lst.Add(sect);

                    string cl = "";
                    while (i < msg.Length && (ch = msg[i]) != ';')
                        cl += msg[i++];
                    cl = cl.Trim();


                    if (!cl.Contains("*"))
                    {
                        if (!int.TryParse(cl, out int code))
                            code = 7; // fallback to gray

                        if (!TryFindColor(code, out ConsoleColor color))
                            color = ConsoleColor.Gray;

                        sect = new KolorSection("");
                        sect.Color = color;
                        sect.ColorChange = true;
                    }
                    else
                    {
                        sect = new KolorSection("");
                        sect.Back = Count(cl, '*');
                    }

                    i++;
                    continue;
                }

                part.Append(ch); // Nothing special, save it to message
            }

            var str = part.ToString();
            if (str != "") // Non-empty section
            {
                sect.Message = str;
                lst.Add(sect);
            }
            else if (sect.Back > 0) // Revert color section
            {
                lst.Add(sect);
            }

            return lst;
        }

        /// <summary>
        /// Fixes format numbers in <see cref="KolorSection.Message"/>
        /// </summary>
        /// <param name="sect">Section to fix</param>
        public static string FixFormats(KolorSection sect)
        {
            var ch = ' ';
            int i = 0;
            int f = 0;
            StringBuilder newmsg = new StringBuilder();
            while (i < sect.Message.Length)
            {
                ch = sect.Message[i++];

                if (ch == '{') // Incoming format
                {
                    if (i < sect.Message.Length && sect.Message[i] == '{') // Format escape
                    {
                        newmsg.Append("{{");
                        i++;
                        continue;
                    }
                    // We will skip over this and write ours
                    string fmt = "";
                    while (i < sect.Message.Length && (ch = sect.Message[i]) != '}') fmt += sect.Message[i++];

                    fmt = fmt.Trim();

                    var split = fmt.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length == 2) // {2:X4}
                    {
                        newmsg.Append('{');
                        newmsg.Append(f++);
                        newmsg.Append(':');
                        newmsg.Append(split[1]);
                        newmsg.Append('}');
                    }
                    else // {2}
                    {
                        newmsg.Append('{');
                        newmsg.Append(f++);
                        newmsg.Append('}');
                    }

                    i++; // Skip '}'
                    continue;
                }

                newmsg.Append(ch);
            }
            return newmsg.ToString();
        }

        private static int Count(string str, char c)
        {
            int i = 0; int count = 0;
            while (i < str.Length) { if (str[i] == c) count++; i++; }
            return count;
        }
    }

    /// <summary>
    /// A part of message with color and format information needed for <see cref="SuperKonsole"/>
    /// </summary>
    public struct KolorSection
    {
        public int Back;
        public bool ColorChange;
        public ConsoleColor Color;
        public string Message;
        public int Formats;

        public KolorSection(string msg)
        {
            Color = ConsoleColor.Gray;
            Message = msg;
            Formats = 0;
            Back = 0;
            ColorChange = false;
        }

        public override string ToString()
        {
            return $"Message: '{ Message }', Number of formats: { Formats }, Color: { Color }, Revert back color: { Back }";
        }
    }
}
