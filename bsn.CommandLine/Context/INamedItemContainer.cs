using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface INamedItemContainer<T> where T: INamedItem {
		IEnumerable<T> GetItems();
	}
}