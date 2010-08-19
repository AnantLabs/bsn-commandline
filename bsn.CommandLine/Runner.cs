using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

using bsn.CommandLine.Context;
using bsn.CommandLine.Parser;

namespace bsn.CommandLine {
	public class Runner {
		private static readonly Regex isHelp = new Regex(@"^(\?|help)$", RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture|RegexOptions.IgnoreCase);
		private readonly IExecutionContext executionContext;

		public Runner(IExecutionContext executionContext) {
			if (executionContext == null) {
				throw new ArgumentNullException("executionContext");
			}
			this.executionContext = executionContext;
			CommandLineParser.GetSemanticActions();
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
					do {
						string s = commandLine.Unnamed[0];
						List<CommandBase> availableCommands = new List<CommandBase>(CommandBase.Filter(command.GetAvailableCommands(), s));
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
						commandLine.Unnamed.RemoveAt(0);
					} while (commandLine.Unnamed.Count > 0);
					if (command != executionContext.Context) {
						if ((commandLine.Unnamed.Count == 1) && (isHelp.IsMatch(commandLine.Unnamed[0]))) {
							command.WriteCommandHelp(executionContext.Output);
						} else {
							command.ExecuteInternal(executionContext, commandLine.Named, commandLine.Unnamed);
						}
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
