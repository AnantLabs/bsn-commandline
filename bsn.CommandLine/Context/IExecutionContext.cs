using System;
using System.IO;

namespace bsn.CommandLine.Context {
	public interface IExecutionContext<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		ContextBase<TExecutionContext> Context {
			get;
			set;
		}

		TextReader Input {
			get;
		}

		TextWriter Output {
			get;
		}

		RootContext<TExecutionContext> RootContext {
			get;
		}
	}
}