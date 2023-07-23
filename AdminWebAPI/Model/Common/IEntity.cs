using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common
{
    public class IEntity : IBase
    {
        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        [Required]
        public long CreateUserId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改人Id
        /// </summary>

        public long? ModifyUserId { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>

        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public int IsDeleted { get; set; }
    }
}