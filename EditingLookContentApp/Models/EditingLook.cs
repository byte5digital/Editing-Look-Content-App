using NPoco;
using System;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace byte5.EditingLookContentApp.Models
{
    [TableName("EditingLook")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class EditingLook
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("NodeUid")]
        public string NodeUid { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("SubscribeDate")]
        public DateTime SubscribeDate { get; set; }
    }
}