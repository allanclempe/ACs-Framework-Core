using FluentMigrator;

namespace ACs.Framework.Web.Migrations
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    [Migration(20160519125900)]
    public class InitialCreate : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FirstName").AsString(20).NotNullable()
                .WithColumn("LastName").AsString(20).NotNullable()
                .WithColumn("Password").AsString(100).NotNullable()
                .WithColumn("Token").AsString(36).NotNullable()
                .WithColumn("Status").AsInt16().NotNullable()
                ;

            Create.Table("UserEmail")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Address").AsString(255).NotNullable()
                .WithColumn("IsPrimary").AsBoolean().NotNullable()
                .WithColumn("User_Id").AsInt32().NotNullable()
                ;

            Create.ForeignKey().FromTable("UserEmail").ForeignColumn("User_Id")
                .ToTable("User").PrimaryColumn("Id");

        }

    }
}
