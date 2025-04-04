namespace violet.backend.Entities;

public sealed class TelegramInfo
{
    public TelegramInfo()
    {

    }
    public TelegramInfo(TelegramInfo info)
    {

        TelegramId = info.TelegramId;
        IsBot = info.IsBot;
        FirstName = info.FirstName;
        LastName = info.LastName;
        Username = info.Username;
        LanguageCode = info.LanguageCode;
        IsPremium = info.IsPremium;
        AddedToAttachmentMenu = info.AddedToAttachmentMenu;
        AllowsWriteToPm = info.AllowsWriteToPm;
        PhotoUrl = info.PhotoUrl;
    }
    public long TelegramId { get; set; }
    public bool IsBot { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string LanguageCode { get; set; }
    public bool IsPremium { get; set; }
    public bool AddedToAttachmentMenu { get; set; }
    public bool AllowsWriteToPm { get; set; }
    public string PhotoUrl { get; set; }
}