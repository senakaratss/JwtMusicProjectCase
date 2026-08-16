using JwtMusicProjectCase.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtMusicProjectCase.Business.Dtos.PackageDtos
{
    public class GetByIdPackageDto
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public int PackageLevel { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
