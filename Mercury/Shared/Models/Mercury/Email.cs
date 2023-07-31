using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The Email model class.
    /// </summary>
    [Table("Email", Schema = "Mercury")]
    [Serializable]
    public partial class Email
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int EmailID
        {
            get; set;
        }
        int _emailID;

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        [DataMember]
        public int PersonID
        {
            get; set;
        }
        int _personID;

        /// <summary>
        /// Gets or sets the email type ID.
        /// </summary>
        [DataMember]
        public int EmailTypeID
        {
            get; set;
        }
        int _emailTypeID;

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [StringLength(255)]
        [DataMember]
        public string EmailAddress
        {
            get; set;
        }
        string _emailAddress;

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
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        [DataMember]
        public string Remarks
        {
            get; set;
        }
        string _remarks;

        /// <summary>
        /// Gets or sets the is default flag.
        /// </summary>
        [DataMember]
        public bool IsDefault
        {
            get; set;
        }
        bool _isDefault;


        /// <summary>
        /// The EmailType associated with this Email.
        /// </summary>
        [DataMember]
        public virtual EmailType EmailType
        { get; set; }

        /// <summary>
        /// The Person associated with this Email.
        /// </summary>
        [DataMember]
        public virtual Person Person
        { get; set; }

        #endregion
    }
}
