using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.photo;
using Model.Other;

namespace Service;


public class PhotoService:IPhotoService
{
    private readonly IMapper _mapper;
    private MyDbContext _context;

    public PhotoService(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PageInfo> PutPhoto(PhotoAdd po)
    {
        string base64 = po.photo.Substring(po.photo.IndexOf(',') + 1);
        byte[] data = Convert.FromBase64String(base64);
        ByteArrayContent bytes = new ByteArrayContent(data);
        
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:8000/detect");
        var content = new MultipartFormDataContent();
        content.Add(bytes, "file_list", "filename.png");
        content.Add(new StringContent("皮卡丘"), "model_name");
        content.Add(new StringContent("True"), "download_image");
        content.Add(new StringContent("640"), "img_size");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
       
        return new PageInfo();
    }
}