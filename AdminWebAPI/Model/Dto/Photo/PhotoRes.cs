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

//携带车牌
public class PhotoCarRes
{
    public int Code { get; set; }
    public bool Success { get; set; }
    public List<Data> Datas { get; set; }
    public string Image_Base64 { get; set; }
}

public class Data
{
    public Cargos Cargos { get; set; }
    public Trucks Trucks { get; set; }
}

public class Cargos
{
    public string name { get; set; }
    public float score { get; set; }
    public object bbox { get; set; }
}

public class Trucks
{
    public string plateNo { get; set; }
    public float score { get; set; }
}