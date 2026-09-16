using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TrailsintheSky2ndChapter
{
	internal class ActionCommand : ICommand
	{
		private readonly Action<object?> mAction;
#pragma warning disable CS0067
		public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067
		public bool CanExecute(object? parameter) => true;
		public ActionCommand(Action<object?> action) => mAction = action;
		public void Execute(object? parameter) => mAction(parameter);
	}
}
