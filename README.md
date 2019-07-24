## What is SuperKonsole?
SuperKonsole is a .NET library written in C# to provide developers easier console formatting  
SuperKonsole is able to color message on-the-go and fix formatting issues

## SuperKonsole Message Specification
SuperKonsole is mostly compatible with System.Console so:  
	`using Console = SuperKonsole.SuperKonsole`  
Would probably work just fine.

But I left one big difference between `System.Console` and `SuperKonsole`: ForegroundColor and BackgroundColor  
These two are respectively changed to TextColor and BackColor, this will make your code not compile  
Don't worry tho, SuperKonsole provides easier color formatting.  
This difference was left with the thought of you changing your code to use SuperKonsole instead

# Formatting in SuperKonsole
SuperKonsole formatting is same as string.Format with a few extra features

1. Auto Formatting  
	SuperKonsole will automatically fix format numbers.

	Ex: `SuperKonsole.WriteLine("{}, {*}!", "Hello", "World");`  
	Output: Hello, World!

	`SuperKonsole.WriteLine` internally calls `SuperKonsole.FixFormats` to re-create all formats

2. Easy Coloring  
	SuperKonsole defines an easy-to-use color formatting system for console

	Ex: `SuperKonsole.WriteLine("Hello, &cyan;World&back;!")`  
	Output: Hello, World! (but 'World' is in cyan color obviously)

	`SuperKonsole.WriteLine` internally uses a method to split message into smaller parts and write them indepently  
	SuperKonsole is currently capable of using all `ConsoleColor` colors.

	Formatting for color:  
	* Name based formatting  
	This method uses color names to determine colors  

	Semantics: *%color or 'back';Message* 
	Values:
		- black
		- darkblue
		- darkgreen
		- darkcyan
		- darkred
		- darkmagenta
		- darkyellow
		- gray
		- darkgray
		- blue
		- green
		- cyan
		- red
		- magenta
		- yellow
		- white
		- back (used to revert back to color before)

	* Code based formatting  
	This method uses color codes to determine colors. Since name based formatting uses such verbose names, message can get quite big quite quick.  
	This method instead uses color codes to determine colors, which is not only compact, but a little bit faster too.

	Semantics: *%code or '**';Message*  
	Values: 0 to 15, 0 being the 'black' and 15 being 'white'. Also a '*' used for reverting back to color before.  
	
	Every color change pushes the color before it to stack. And every one '*' char in '&*;' will revert by one color.  
	This means: '&***;' will revert back three times.  
	Ex: `SuperKonsole.WriteLine("Hello, &1;W&2;o&*;rl&*;d!")`  
	Output: Hello, World!