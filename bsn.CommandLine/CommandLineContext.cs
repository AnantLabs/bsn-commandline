using System;
using System.Diagnostics;
using System.IO;

using bsn.CommandLine.Context;

namespace bsn.CommandLine {
	public class CommandLineContext<TExecutionContext, T>: IExecutionContext<TExecutionContext> where T: RootContext<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly TextReader input;
		private readonly TextWriter output;
		private readonly T rootContext;
		private ContextBase<TExecutionContext> context;

		public CommandLineContext(T rootContext, TextReader input, TextWriter output) {
			if (rootContext == null) {
				throw new ArgumentNullException("rootContext");
			}
			if (input == null) {
				throw new ArgumentNullException("input");
			}
			if (output == null) {
				throw new ArgumentNullException("output");
			}
			this.rootContext = rootContext;
			this.input = input;
			this.output = output;
		}

		public T RootContext {
			get {
				return rootContext;
			}
		}

		RootContext<TExecutionContext> IExecutionContext<TExecutionContext>.RootContext {
			get {
				return RootContext;
			}
		}

		public ContextBase<TExecutionContext> Context {
			get {
				return context;
			}
			set {
				Debug.Assert((value == null) || ReferenceEquals(value.FindRootContext(), rootContext));
				context = value;
			}
		}

		public TextWriter Output {
			get {
				return output;
			}
		}

		public TextReader Input {
			get {
				return input;
			}
		}
	}
}