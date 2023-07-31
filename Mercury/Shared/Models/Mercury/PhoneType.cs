using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The PhoneType model class.
    /// </summary>
    [Table("PhoneType", Schema = "Mercury")]
    [Serializable]
    public partial class PhoneType
    {
        #region Properties

        /// <summary>
        /// Gets or sets the phone type ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PhoneTypeID
        {
            get; set;
        }
        int _phoneTypeID;

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Required]
        [StringLength(2)]
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
        /// The collection of Phones associated with this PhoneType.
        /// </summary>
        [ForeignKey("PhoneTypeID")]
        [DataMember]
        public virtual ICollection<Phone> Phones
        { get; set; }

        #endregion

    }
}
