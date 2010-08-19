using System;
using System.IO;

namespace bsn.CommandLine.Context {
	public interface IExecutionContext {
		ContextBase Context {
			get;
			set;
		}

		TextReader Input {
			get;
		}

		TextWriter Output {
			get;
		}

		RootContext RootContext {
			get;
		}
	}
}
