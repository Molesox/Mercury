using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Mercury.Shared.Models.AspNetUser;
namespace Mercury.Shared.Models.Mercury
{
    /// <summary>
    /// The Person model class.
    /// </summary>
    [Table("Person", Schema = "Mercury")]
    [Serializable]
    public partial class Person 
    {
        public Person() { }

        #region Properties

        /// <summary>
        /// Gets or sets the person ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int PersonID
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the AppUser ID.
        /// </summary>
        [DataMember]
        public string? AppUserID
        {
            get; set;
        } = null!;

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        [StringLength(20)]
        [DataMember]
        public string? Culture
        {
            get; set;
        }


        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string? Title
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string? LastName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [StringLength(80)]
        [DataMember]
        public string? FirstName
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the vat number.
        /// </summary>
        [StringLength(20)]
        public string? VatNumber
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        [StringLength(200)]
        [DataMember]
        public string? Remarks
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the annual revenue.
        /// </summary>
        [DataMember]
        public decimal? AnnualRevenue
        {
            get; set;
        } = null!;

        /// <summary>
        /// Gets the full name, 
        /// i.e, the concatenation of first name and last name.
        /// </summary>
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// Gets the inverted full name,
        /// i.e, the concatenation of last name and first name.
        /// </summary>
        [NotMapped]
        public string FullNameInv => $"{LastName} {FirstName}";

        /// <summary>
        /// The AppUser associated with this Person.
        /// </summary>
        [DataMember]
        public virtual AspNetUser.AspNetUser AppUser
        { get; set; }

        /// <summary>
        /// The collection of Addresses associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Address> Addresses { get; set; }= new List<Address>();

        /// <summary>
        /// The collection of Emails associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

        /// <summary>
        /// The collection of PersonProfiles associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<PersonProfile> PersonProfiles { get; set; }= new List<PersonProfile>();

        /// <summary>
        /// The collection of Phones associated with this Person.
        /// </summary>
        [ForeignKey("PersonID")]
        [DataMember]
        public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

        #endregion
    }
}