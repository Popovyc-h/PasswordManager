using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class PasswordListViewModel : ViewModelBase
{
    private readonly IPasswordEntryRepository _repository;
    private readonly IEncryptionService _encryptionService;
    private readonly ISessionService _sessionService;

    public ObservableCollection<PasswordEntryItemViewModel> PasswordEntries { get; set; } = new();

    public PasswordListViewModel(IPasswordEntryRepository passwordEntryRepository, IEncryptionService encryptionService, ISessionService sessionService)
    {
        _repository = passwordEntryRepository;
        _encryptionService = encryptionService;
        _sessionService = sessionService;
    }
        
    public async Task InitializeAsync(int userId)
    {
        var entries = await _repository.GetAllByUserIdAsync(userId);
        PasswordEntries.Clear();

        foreach (var entry in entries)
            PasswordEntries.Add(new PasswordEntryItemViewModel(entry, _encryptionService, _sessionService));
    }

    [RelayCommand]
    public async Task DeleteEntry(PasswordEntryItemViewModel passwordEntryItemViewModel)
    {
        await _repository.DeleteAsync(passwordEntryItemViewModel.Entry.Id);
        PasswordEntries.Remove(passwordEntryItemViewModel);
    }
}