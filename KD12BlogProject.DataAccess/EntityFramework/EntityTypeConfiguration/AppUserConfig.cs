using KD12BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KD12BlogProject.DataAccess.EntityFramework.EntityTypeConfiguration
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        //BaseEntity üzerinden devam ediyoruz.Çünkü oradaki sınıfımız  temel yapılandırmalarımızıda içeriyor bu yüzden tekrardan IEntityTypeConfiguration'dan kalıtım almamıza gerek kalmıyor. Welcome OOP!!
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NormalizedUserName).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(true);

            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
