using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The EmailType model class.
    /// </summary>
    [Table("EmailType", Schema = "Mercury")]
    [Serializable]
    public partial class EmailType
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email type ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int EmailTypeID
        {
            get; set;
        }
        int _emailTypeID;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(1)]
        [DataMember]
        public string Code
        {
            get; set;
        }
        string _code;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [StringLength(30)]
        [DataMember]
        public string Name
        {
            get; set;
        }
        string _name;

        /// <summary>
        /// Gets or sets the sort key.
        /// </summary>
        [DataMember]
        public int? SortKey
        {
            get; set;
        }
        int? _sortKey;

        /// <summary>
        /// The collection of Emails associated with this Email type.
        /// </summary>
        [ForeignKey("EmailTypeID")]
        [DataMember]
        public virtual ICollection<Email> Emails
        { get; set; }

        #endregion
    }
}
