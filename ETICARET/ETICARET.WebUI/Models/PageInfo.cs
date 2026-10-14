namespace ETICARET.WebUI.Models;

/// <summary>Sayfalama bilgisi. &lt;pagination page-info="..."&gt; tag helper'ı bunu kullanır.</summary>
public class PageInfo
{
    public int TotalItems { get; set; }

    public int PageSize { get; set; } = 12;

    // NOT (C# 14 "field" anahtar kelimesi): Ayrı bir private alan tanımlamadan
    // otomatik özelliğin arka planındaki alana erişiriz. Sayfa 1'den küçük olamaz.
    public int CurrentPage
    {
        get;
        set => field = Math.Max(1, value);
    } = 1;

    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;
}
