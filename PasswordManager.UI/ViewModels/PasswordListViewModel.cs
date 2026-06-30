using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class PasswordListViewModel : ViewModelBase
{
    private readonly IPasswordEntryRepository _repository;

    public ObservableCollection<PasswordEntry> PasswordEntries { get; set; } = new();

    public PasswordListViewModel(IPasswordEntryRepository passwordEntryRepository)
    {
        _repository = passwordEntryRepository;
    }

    public async Task InitializeAsync(int userId)
    {
        var entries = await _repository.GetAllByUserIdAsync(userId);
        PasswordEntries.Clear();

        foreach (var entry in entries)
            PasswordEntries.Add(entry);
    }
}