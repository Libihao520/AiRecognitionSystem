using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.photo;
using Model.Other;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

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
        MemoryStream stream = new MemoryStream(data);

        

        var client = new RestClient("http://127.0.0.1:8005/detect");
        client.UseNewtonsoftJson();
        var request = new RestRequest();
        request.Method = Method.Post;
        request.AddFile("file_list", stream.ToArray(), "filename.png");
        request.AddParameter("model_name", "皮卡丘");
        request.AddParameter("download_image", "True");
        var response = await client.ExecuteAsync<PhotoRes>(request);


        var pageInfo = new PageInfo();
        pageInfo.Total = 1;
        pageInfo.Data = response.Data.Image_Base64;
        return pageInfo;
    }
}