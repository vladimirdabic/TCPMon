using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VD.BinarySchema;
using VD.Blaze.Interpreter;
using VD.Blaze.Interpreter.Environment;
using VD.Blaze.Interpreter.Types;
using VD.Blaze.Module;

namespace TCPMon.Blaze
{
    internal class Utils
    {
        public static void CreateLibraries(ModuleEnv env)
        {
            // console library
            Library console_lib = new Library("console");
            env.DefineVariable("console", VariableType.PUBLIC, console_lib);

            console_lib.DefineFunction("print", (VM vm, List<IValue> args) =>
            {
                string moduleName = vm.Module.Module.Name;

                foreach (var arg in args)
                    MainForm.PrintLine($"[{moduleName}] {arg.AsString()}");

                return null;
            });


            // parse library
            var parse_lib = new Library("parse");
            env.DefineVariable("parse", VariableType.PUBLIC, parse_lib);

            parse_lib.DefineFunction("num", (VM vm, List<IValue> args) =>
            {
                if (args.Count == 0 || !(args[0] is StringValue))
                    throw new InterpreterInternalException("Expected string value for function parse.num");

                var val = ((StringValue)args[0]).Value;
                double.TryParse(val, out double res);

                return new NumberValue(res);
            });

            parse_lib.DefineFunction("str", (VM vm, List<IValue> args) =>
            {
                if (args.Count == 0)
                    throw new InterpreterInternalException("Expected object value for function parse.str");

                return new StringValue(args[0].AsString());
            });

            parse_lib.DefineFunction("bool", (VM vm, List<IValue> args) =>
            {
                if (args.Count == 0)
                    throw new InterpreterInternalException("Expected object value for function parse.bool");

                return new BooleanValue(args[0].AsBoolean());
            });


            // Module library
            var module_lib = new Library("module");
            env.DefineVariable("module", VariableType.PUBLIC, module_lib);

            module_lib.DefineFunction("load", (VM vm, List<IValue> args) =>
            {
                if (args.Count == 0 || !(args[0] is StringValue))
                    throw new InterpreterInternalException("Expected module name for function module.load");

                var mod_name = ((StringValue)args[0]).Value;

                try
                {
                    Module module = new Module();
                    MemoryStream stream = new MemoryStream(File.ReadAllBytes(mod_name));
                    BinaryReader reader = new BinaryReader(stream);
                    module.FromBinary(reader);

                    // Load module file
                    ModuleEnv menv = VM.StaticLoadModule(module);

                    // Set its parent to be the current running module
                    menv.SetParent(vm.Module);

                    return new ModuleValue(menv);
                }
                catch (FileNotFoundException)
                {
                    return null;
                }
                catch (IOException)
                {
                    return null;
                }
            });

            // Schema library
            Library schema_library = new Library("schema");
            env.DefineVariable("schema", VariableType.PUBLIC, schema_library);

            schema_library.DefineFunction("load", (VM vm, List<IValue> args) =>
            {
                if (args.Count == 0 || !(args[0] is StringValue))
                    throw new InterpreterInternalException("Expected schema file name for function schema.load");

                var schema_path = Path.Combine("scripts", ((StringValue)args[0]).Value);

                try
                {
                    string schema = File.ReadAllText(schema_path);
                    string name = Path.GetFileName(schema_path);

                    Lexer lexer = new Lexer();
                    Parser parser = new Parser();

                    var tokens = lexer.Lex(schema, name);
                    var defs = parser.Parse(tokens);

                    return new SchemaValue(defs, name);
                }
                catch (LexerException ex)
                {
                    throw new InterpreterInternalException($"[{ex.Source}:{ex.Line}] {ex.Message}");
                }
                catch (ParserException ex)
                {
                    throw new InterpreterInternalException($"[{ex.Source}:{ex.Line}] {ex.Message}");
                }
                catch (FileNotFoundException ex)
                {
                    throw new InterpreterInternalException($"File not found: {ex.FileName}");
                }
            });
        }
    }
}
