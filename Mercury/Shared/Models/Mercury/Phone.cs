using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The Phone model class.
    /// </summary>
    [Table("Phone", Schema = "Mercury")]
    [Serializable]
    public partial class Phone 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the phone ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PhoneID
        {
            get; set;
        }
        int _phoneID;

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
        /// Gets or sets the phone type ID.
        /// </summary>
        [DataMember]
        public int PhoneTypeID
        {
            get; set;
        }
        int _phoneTypeID;

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required]
        [StringLength(30)]
        [DataMember]
        public string PhoneNumber
        {
            get; set;
        }
        string _phoneNumber;

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
        /// The PhoneType associated with this Phone.
        /// </summary>
        [DataMember]
        public virtual PhoneType PhoneType
        { get; set; }

        /// <summary>
        /// The Person associated with this Phone.
        /// </summary>
        [DataMember]
        public virtual Person Person
        { get; set; }

        #endregion
    }
}