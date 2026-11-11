using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace TrailsintheSky2ndChapter.ViewModel
{
	internal class MainViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public GeneralViewModel General { get; init; } = new();
		public ObservableCollection<NumberValue> Party { get; init; } = new();

		public ICommand LoadFileCommand { get; init; }
		public ICommand SaveFileCommand { get; init; }
		public ICommand ExportFileCommand { get; init; }
		public ICommand ImportFileCommand { get; init; }
		public ICommand ChoicePartyCommand { get; init; }

		private Info Info { get; init; } = Info.Instance();

		public SaveData.PlatForm[] Platforms => Enum.GetValues<SaveData.PlatForm>();
		public SaveData.PlatForm SelectPlatform { get; set; } = SaveData.PlatForm.Switch;

		public MainViewModel()
		{
			LoadFileCommand = new ActionCommand(LoadFile);
			SaveFileCommand = new ActionCommand(SaveFile);
			ExportFileCommand = new ActionCommand(ExportFile);
			ImportFileCommand = new ActionCommand(ImportFile);
			ChoicePartyCommand = new ActionCommand(ChoiceParty);
		}

		private void Initialize()
		{
			Party.Clear();
			for (uint index = 0; index < 64; index++)
			{
				Party.Add(new NumberValue(0x51454 + index * 2, 2));
			}

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(General)));
		}

		private void LoadFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Open(dlg.FileName, false, SelectPlatform);
			Initialize();
		}

		private void SaveFile(object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void ExportFile(object? parameter)
		{
			SaveFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}

		private void ImportFile(object? parameter)
		{
			OpenFileDialog dlg = new();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Import(dlg.FileName);
			Initialize();
		}

		private void ChoiceParty(object? parameter)
		{
			var number = parameter as NumberValue;
			if (number == null) return;

			var dlg = new ChoiceWindow();
			dlg.Type = ChoiceWindow.ChoiceType.Chara;
			dlg.ID = number.Value;
			dlg.ShowDialog();

			number.Value = dlg.ID;
		}
	}
}
