using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PasswordManager.Core.Entities;
using PasswordManager.Core.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace PasswordManager.UI.ViewModels;

public partial class AddEntryViewModel : ViewModelBase
{
    private int? _editingEntryId;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _login = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private string _notes = string.Empty;

    [ObservableProperty]
    private int _categoryId = 1;

    [ObservableProperty]
    private int _length = 16;

    [ObservableProperty]
    private bool _useUppercase;

    [ObservableProperty]
    private bool _useLowercase;

    [ObservableProperty]
    private bool _useDigits;

    [ObservableProperty]
    private bool _useSpecial;

    private readonly IPasswordEntryRepository _passwordEntryRepository;
    private readonly IEncryptionService _encryptionService;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly ISessionService _sessionService;
    private readonly IRepository<PasswordHistory> _passwordHistoryRepository;
    private readonly IRepository<Category> _categoryRepository;

    public event Action? OnEntrySaved;
    public ObservableCollection<Category> Categories { get; } = new();

    public AddEntryViewModel(IPasswordEntryRepository passwordEntryRepository, IEncryptionService encryptionService, IPasswordGenerator passwordGenerator, ISessionService sessionService, IRepository<PasswordHistory> passwordHistoryRepository, IRepository<Category> categoryRepository)
    {
        _passwordEntryRepository = passwordEntryRepository;
        _encryptionService = encryptionService;
        _passwordGenerator = passwordGenerator;
        _sessionService = sessionService;
        _passwordHistoryRepository = passwordHistoryRepository;
        _categoryRepository = categoryRepository;
        _ = LoadCategoriesAsync(); // _ = символ (дискард/discard) для того щоб компілятор не сварився, він каже що йому не потрібен Task який повертає метод
    }

    [RelayCommand]
    private async Task Save()
    {
        if (_sessionService.AesKey == null)
            return;

        if (_editingEntryId.HasValue)
        {
            int id = _editingEntryId.Value;

            var existing = await _passwordEntryRepository.GetByIdAsync(id);

            if (existing == null) 
                return;

            var (encryptedPassword, iv) = _encryptionService.Encrypt(Password, _sessionService.AesKey);

            var passwordHistory = new PasswordHistory {
                EncryptedPassword = existing.EncryptedPassword,
                IV = existing.IV,
                PasswordEntryId = existing.Id,
                ChangedAt = DateTime.UtcNow
            };

            await _passwordHistoryRepository.AddAsync(passwordHistory);

            existing.Title = Title;
            existing.Login = Login;
            existing.EncryptedPassword = encryptedPassword;
            existing.IV = iv;
            existing.Url = Url;
            existing.Notes = Notes;
            existing.CategoryId = CategoryId;
            existing.UpdatedAt = DateTime.UtcNow;

            await _passwordEntryRepository.UpdateAsync(existing);
        }
        else
        {
            var (encryptedPassword, iv) = _encryptionService.Encrypt(Password, _sessionService.AesKey);

            var passwordEntry = new PasswordEntry
            {
                UserId = _sessionService.UserId,
                Title = Title,
                Login = Login,
                EncryptedPassword = encryptedPassword,
                IV = iv,
                Url = Url,
                Notes = Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CategoryId = CategoryId,
            };

            await _passwordEntryRepository.AddAsync(passwordEntry);
        }
        OnEntrySaved?.Invoke();
    }

    [RelayCommand]
    private void GeneratePassword()
    {
        Password = _passwordGenerator.Generate(Length, UseUppercase, UseLowercase, UseDigits, UseSpecial);
    }

    public void LoadForEdit(PasswordEntry passwordEntry, string decryptedPassword)
    {
        Url = passwordEntry.Url ?? string.Empty;
        Notes = passwordEntry.Notes ?? string.Empty;
        Title = passwordEntry.Title;
        Login = passwordEntry.Login;
        CategoryId = passwordEntry.CategoryId;
        _editingEntryId = passwordEntry.Id;
        Password = decryptedPassword;
    }

    public void ResetForNewEntry()
    {
        _editingEntryId = null;
        Title = string.Empty;
        Login = string.Empty;
        Password = string.Empty;
        Url = string.Empty;
        Notes = string.Empty;
        CategoryId = 1;
    }

    private async Task LoadCategoriesAsync()
    {
        var list = await _categoryRepository.GetAllAsync();
        Categories.Clear();

        foreach (var category in list)
            Categories.Add(category);
    }
}