using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TrailsintheSky2ndChapter
{
	internal class ActionCommand(Action<object?> _action) : ICommand
	{
#pragma warning disable CS0067
		public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067
		public bool CanExecute(object? parameter) => true;
		public void Execute(object? parameter) => _action(parameter);
	}
}
