using System.ComponentModel.DataAnnotations;
using Model.Common;

namespace Model.Entitys;

/// <summary>
/// 用户
/// </summary>
public class Users : IEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [Required]
    public string NickName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 用户类型（0=超级管理员，1=普通用户）
    /// </summary>
    [Required]
    public int UserType { get; set; }

    /// <summary>
    /// 是否启用（0=未启用，1=启用）
    /// </summary>
    [Required]
    public bool IsEnable { get; set; }
}