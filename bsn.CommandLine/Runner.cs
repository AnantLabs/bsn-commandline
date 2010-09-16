using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

using bsn.CommandLine.Context;
using bsn.CommandLine.Parser;

namespace bsn.CommandLine {
	public class Runner<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private static readonly Regex isHelp = new Regex(@"^(\?|help)$", RegexOptions.CultureInvariant|RegexOptions.ExplicitCapture|RegexOptions.IgnoreCase);
		private readonly TExecutionContext executionContext;

		public Runner(TExecutionContext executionContext) {
			if (executionContext == null) {
				throw new ArgumentNullException("executionContext");
			}
			this.executionContext = executionContext;
			CommandLineParser.GetSemanticActions();
		}

		public void Run() {
			RootContext<TExecutionContext> rootContext = executionContext.RootContext;
			TextWriter output = executionContext.Output;
			Debug.Assert(rootContext != null);
			executionContext.Context = rootContext;
			do {
				output.WriteLine();
				WriteContextName(output, executionContext.Context, '>');
				output.Write(' ');
				try {
					ParsedLine commandLine = CommandLineParser.Parse(executionContext.Input.ReadLine());
					CommandBase<TExecutionContext> command = executionContext.Context;
					while (commandLine.Unnamed.Count > 0) {
						string s = commandLine.Unnamed[0];
						List<CommandBase<TExecutionContext>> availableCommands = new List<CommandBase<TExecutionContext>>(CommandBase<TExecutionContext>.Filter(command.GetAvailableCommands(), s));
						if (availableCommands.Count < 1) {
							if (command == executionContext.Context) {
								output.WriteLine("The following command was not found: {0}", s);
							}
							break;
						}
						if (availableCommands.Count > 1) {
							output.WriteLine("Ambiguous command name for '{0}':", s);
							foreach (CommandBase<TExecutionContext> availableCommand in availableCommands) {
								output.Write(' ');
								output.WriteLine(availableCommand.Name);
							}
							command = executionContext.Context;
							break;
						}
						command = availableCommands[0];
						commandLine.Unnamed.RemoveAt(0);
					}
					if (command != executionContext.Context) {
						if ((commandLine.Unnamed.Count == 1) && (isHelp.IsMatch(commandLine.Unnamed[0]))) {
							command.WriteItemHelp(executionContext.Output, executionContext);
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

		private void WriteContextName(TextWriter output, ContextBase<TExecutionContext> context, char separator) {
			if (context != null) {
				WriteContextName(output, context.ParentContext, ' ');
				output.Write(context.Name);
				output.Write(separator);
			}
		}
	}
}