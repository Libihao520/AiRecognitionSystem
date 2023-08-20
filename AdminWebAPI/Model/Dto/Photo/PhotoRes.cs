namespace Model.Dto.photo;

public class PhotoRes
{
    public int Code { get; set; }
    public bool Success { get; set; }
    public List<Result> result { get; set; }
    public string Image_Base64 { get; set; }
}

public class Result
{
    public int Class { get; set; }
    public string ClassName { get; set; }
    public object bbox { get; set; }
    public double confidence { get; set; }
}