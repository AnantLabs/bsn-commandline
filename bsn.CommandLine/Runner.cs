using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using bsn.CommandLine.Context;
using bsn.CommandLine.Parser;

namespace bsn.CommandLine {
	public class Runner {
		private readonly IExecutionContext executionContext;

		public Runner(IExecutionContext executionContext) {
			if (executionContext == null) {
				throw new ArgumentNullException("executionContext");
			}
			this.executionContext = executionContext;
		}

		public void Run() {
			RootContext rootContext = executionContext.RootContext;
			TextWriter output = executionContext.Output;
			Debug.Assert(rootContext != null);
			executionContext.Context = rootContext;
			do {
				output.WriteLine();
				WriteContextName(output, executionContext.Context, '>');
				output.Write(' ');
				try {
					ParsedLine commandLine = CommandLineParser.Parse(executionContext.Input.ReadLine());
					CommandBase command = executionContext.Context;
					foreach (string s in commandLine.Unnamed) {
						List<CommandBase> availableCommands = new List<CommandBase>(CommandBase.Filter(command.AvailableCommands(), s));
						if (availableCommands.Count < 1) {
							if (command == executionContext.Context) {
								output.WriteLine("The following command was not found: {0}", s);
							}
							break;
						}
						if (availableCommands.Count > 1) {
							output.WriteLine("Ambiguous command name for '{0}':", s);
							foreach (CommandBase availableCommand in availableCommands) {
								output.WriteLine(availableCommand.Name);
							}
							command = executionContext.Context;
							break;
						}
						command = availableCommands[0];
					}
					if (command != executionContext.Context) {
						command.Execute(executionContext);
					}
				} catch (SystemException ex) {
					output.WriteLine("Error: {0}", ex.Message);
					Debug.WriteLine(ex);
				}
			} while (executionContext.Context != null);
		}

		private void WriteContextName(TextWriter output, ContextBase context, char separator) {
			if (context != null) {
				WriteContextName(output, context.ParentContext, ' ');
				output.Write(context.Name);
				output.Write(separator);
			}
		}
	}
}
