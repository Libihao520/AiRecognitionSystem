using Model.Dto.photo;
using Model.Other;

namespace Interface;

public interface IPhotoService
{
    Task<PageInfo> PutPhoto(PhotoAdd po);
}