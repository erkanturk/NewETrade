using ETICARET.Business.Abstract;
using ETICARET.Business.Models;
using ETICARET.DataAccess.Abstract;
using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.Business.Concrete;

public class CommentManager(ICommentDal commentDal, IProductDal productDal) : ICommentService
{
    private const int MaxLength = 1000;
    public async Task<ServiceResult> AddAsync(int productId, string userId, string userName, string text)
    {
        var error=ValidateText(text);
        if (error is not null)
        {
            return ServiceResult.Fail(error);
        }
        if(!await productDal.AnyAsync(p=>p.Id==productId))
            return ServiceResult.Fail("Ürün bulunamadı.");
        await commentDal.AddAsync(new Comment
        {
            ProductId = productId,
            UserId = userId,
            UserName = userName,
            Text = text.Trim()
        });
        return ServiceResult.Ok("Yorum eklendi.");
    }

    public async Task<ServiceResult> DeleteAsync(int commentId, string userId, bool isAdmin)
    {
       var comment=await commentDal.GetByIdAsync(commentId);
        if(comment is null)
            return ServiceResult.Fail("Yorum bulunamadı.");
        if(comment.UserId!=userId && !isAdmin)
            return ServiceResult.Fail("Yorum silme yetkiniz yok.");
        await commentDal.DeleteAsync(comment);
        return ServiceResult.Ok("Yorum silindi.");
    }

    public Task<List<Comment>> GetAllAsync()
   => commentDal.GetAllWithProductAsync();

    public Task<List<Comment>> GetByProductAsync(int productId)
    => commentDal.GetByProductAsync(productId);

    public async Task<ServiceResult> UpdateAsync(int commentId, string userId, string text)
    {
        var error = ValidateText(text);
        if(error is not null) 
            return ServiceResult.Fail(error);
        var comment =await commentDal.GetByIdAsync(commentId);
        if(comment is null)
            return ServiceResult.Fail("Yorum bulunamadı.");
        if(comment.UserId!=userId) return ServiceResult.Fail("Yorum güncelleme yetkiniz yok.");

        comment.Text=text.Trim();
        await commentDal.UpdateAsync(comment);
        return ServiceResult.Ok("Yorum güncellendi.");

    }
    private static string? ValidateText(string? text) => text?.Trim() switch
    {
        null or "" => "Yorum boş olamaz.",
        { Length:>MaxLength } => $"Yorum en fazla {MaxLength} kadar olabilir",
        _ => null
    };

}
