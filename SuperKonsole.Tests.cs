using System;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SuperKonsole.Tests
{
    /// <summary>
    /// A small test class for speed comparions between <see cref="Console"/> and <see cref="SuperKonsole"/>.
    /// This comparison should not be taken seriously. Even if <see cref="Samples"/> is so big, jittery results may be produced.
    /// </summary>
    public static class SuperKonsoleTests
    {
        private static StringWriter Output;
        private static TextWriter StandardOutput;

        /// <summary>
        /// Number of samples collected for each task, more better.
        /// Default: 50
        /// </summary>
        public static int Samples { get; set; }

        /// <summary>
        /// Total output of all tasks
        /// </summary>
        public static StringBuilder OutputString { get; set; }

        public static void Initialize()
        {
            Samples = 50;
            OutputString = new StringBuilder("");
            Output = new StringWriter(OutputString);
            StandardOutput = Console.Out;
        }

        /// <summary>
        /// Configure and run all tests
        /// </summary>
        public static void RunAll()
        {
            // Warmup
            var sw = Stopwatch.StartNew();
            sw.Stop();
            sw.Reset();
            sw.Restart();
            sw.Stop();

            TestWrite();
            TestWriteLine();

            TestFormattedWrite();
            TestFormattedWriteLine();

            TestColored();

            TestColoredFormatted();
        }

        /// <summary>
        /// Compare <see cref="Console.WriteLine(string, object[])"/> and <see cref="SuperKonsole.WriteLine(object, object[])"/>
        /// </summary>
        public static void TestWriteLine()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.WriteLine("Hello, World!");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
                Console.WriteLine("Hello, World!");
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, World!', Test 'WriteLine'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }

        /// <summary>
        /// Compare <see cref="Console.Write(object)"/> and <see cref="SuperKonsole.Write(object)"/>
        /// </summary>
        public static void TestWrite()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.Write("Hello, World!\n");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
                Console.Write("Hello, World!\n");
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, World!\\n', Test 'Write'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }

        /// <summary>
        /// Compare <see cref="Console.WriteLine(string, object[])"/> and <see cref="SuperKonsole.WriteLine(object, object[])"/>
        /// </summary>
        public static void TestFormattedWriteLine()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.WriteLine("Hello, {0}!", "World");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
                Console.WriteLine("Hello, {0}!", "World");
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, {{0}}!', Format: 'World', Test 'WriteLine Formatted'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }

        /// <summary>
        /// Compare <see cref="Console.Write(string, object[])"/> and <see cref="SuperKonsole.WriteLine(object, object[])"/>
        /// </summary>
        public static void TestFormattedWrite()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.Write("Hello, {0}!\n", "World");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
                Console.Write("Hello, {0}!\n", "World");
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, {{0}}!\\n' Test 'Write Formatted'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }

        /// <summary>
        /// Compare <see cref="Console.WriteLine(string, object[])"/> and <see cref="SuperKonsole.WriteLine(object, object[])"/>
        /// </summary>
        public static void TestColored()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.WriteLine("Hello, %3;World%*;!");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
            {
                Console.Write("Hello, ");
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("World");
                Console.ForegroundColor = c;
                Console.WriteLine("!");
            }
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, World!', Colored Part: 'World', Test 'Colored'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }

        /// <summary>
        /// Compare <see cref="Console.WriteLine(string, object[])"/> and <see cref="SuperKonsole.WriteLine(object, object[])"/>
        /// </summary>
        public static void TestColoredFormatted()
        {
            Console.SetOut(Output);
            
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < Samples; i++)
                SuperKonsole.WriteLine("Hello, %3;{0}%*;!", "World");
            sw.Stop();
            var ms_super = sw.ElapsedTicks / Samples;
            sw.Restart();
            for (int i = 0; i < Samples; i++)
            {
                Console.Write("Hello, ");
                var c = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("{0}", "World");
                Console.ForegroundColor = c;
                Console.WriteLine("!");
            }
            sw.Stop();
            var ms_sys = sw.ElapsedTicks / Samples;

            Console.SetOut(StandardOutput);

            SuperKonsole.WriteLine("String 'Hello, {{0}}!', Colored Part: '{{0}}', Format: 'World', Test 'Colored Formatted'\n SuperKonsole: %3;{}%*; ticks\n System.Console: %3;{}%*; ticks", ms_super, ms_sys);
        }
    }
}
