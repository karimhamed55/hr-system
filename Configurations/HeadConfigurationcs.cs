//using IEEE.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace IEEE.Configurations
//{
//    public class HeadConfigurations : IEntityTypeConfiguration<Head>
//    {
//        public void Configure(EntityTypeBuilder<Head> builder)
//        {
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.Id).ValueGeneratedOnAdd();

//            builder.HasOne(h => h.Committee)
//                .WithOne(c => c.Head)
//                .HasForeignKey<Head>(h => h.CommitteeId);

//        }
//    }
//}
