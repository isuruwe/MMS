namespace MMSEntity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MMSEntities : DbContext
    {
        public MMSEntities()
            : base("name=MMSEntities")
        {
        }

        public virtual DbSet<BatchStock> BatchStocks { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<CatDaignosi> CatDaignosis { get; set; }
        public virtual DbSet<Clinic_Alloc> Clinic_Alloc { get; set; }
        public virtual DbSet<Clinic_Master> Clinic_Master { get; set; }
        public virtual DbSet<Clinic_Schedule> Clinic_Schedule { get; set; }
        public virtual DbSet<Clinic_Type> Clinic_Type { get; set; }
        public virtual DbSet<Drug_Prescription> Drug_Prescription { get; set; }
        public virtual DbSet<DrugItem> DrugItems { get; set; }
        public virtual DbSet<DrugMethod> DrugMethods { get; set; }
        public virtual DbSet<DrugObject> DrugObjects { get; set; }
        public virtual DbSet<DrugOrderForm> DrugOrderForms { get; set; }
        public virtual DbSet<DrugRoute> DrugRoutes { get; set; }
        public virtual DbSet<DrugStockMaster> DrugStockMasters { get; set; }
        public virtual DbSet<DrugStockTransection> DrugStockTransections { get; set; }
        public virtual DbSet<DrugStockType> DrugStockTypes { get; set; }
        public virtual DbSet<DrugStore> DrugStores { get; set; }
        public virtual DbSet<ExamineAbdominal> ExamineAbdominals { get; set; }
        public virtual DbSet<ExamineCardiovascular> ExamineCardiovasculars { get; set; }
        public virtual DbSet<ExamineCentralNervou> ExamineCentralNervous { get; set; }
        public virtual DbSet<ExamineGeneral> ExamineGenerals { get; set; }
        public virtual DbSet<ExamineRespiratory> ExamineRespiratories { get; set; }
        public virtual DbSet<FamilyDetail> FamilyDetails { get; set; }
        public virtual DbSet<HypersenseReaction> HypersenseReactions { get; set; }
        public virtual DbSet<HypersenseSeverty> HypersenseSeverties { get; set; }
        public virtual DbSet<Hypersensivity> Hypersensivities { get; set; }
        public virtual DbSet<HypersensivityType> HypersensivityTypes { get; set; }
        public virtual DbSet<HypMainCategory> HypMainCategories { get; set; }
        public virtual DbSet<HypRMainCategory> HypRMainCategories { get; set; }
        public virtual DbSet<IssuedHeader> IssuedHeaders { get; set; }
        public virtual DbSet<Job_Category> Job_Category { get; set; }
        public virtual DbSet<Lab_MainCategory> Lab_MainCategory { get; set; }
        public virtual DbSet<Lab_Report> Lab_Report { get; set; }
        public virtual DbSet<Lab_sms> Lab_sms { get; set; }
        public virtual DbSet<Lab_sms_Cat> Lab_sms_Cat { get; set; }
        public virtual DbSet<Lab_SubCategory> Lab_SubCategory { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<MBP_Category> MBP_Category { get; set; }
        public virtual DbSet<MED_DEFN> MED_DEFN { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<MenuObject> MenuObjects { get; set; }
        public virtual DbSet<ModifiedUser> ModifiedUsers { get; set; }
        public virtual DbSet<OPD_Master> OPD_Master { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Patient_Detail> Patient_Detail { get; set; }
        public virtual DbSet<rank> ranks { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Service_Type> Service_Type { get; set; }
        public virtual DbSet<ServicePersonnelProfile> ServicePersonnelProfiles { get; set; }
        public virtual DbSet<Sex_Type> Sex_Type { get; set; }
        public virtual DbSet<Sick_Category> Sick_Category { get; set; }
        public virtual DbSet<Sick_CategoryType> Sick_CategoryType { get; set; }
        public virtual DbSet<Sick_Type> Sick_Type { get; set; }
        public virtual DbSet<Speciality_Type> Speciality_Type { get; set; }
        public virtual DbSet<Staff_Master> Staff_Master { get; set; }
        public virtual DbSet<STATION> STATIONS { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TranferDetail> TranferDetails { get; set; }
        public virtual DbSet<Usage> Usages { get; set; }
        public virtual DbSet<USER_TYPE> USER_TYPE { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole_Type> UserRole_Type { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VisualExamination> VisualExaminations { get; set; }
        public virtual DbSet<Vital_Type> Vital_Type { get; set; }
        public virtual DbSet<Vital> Vitals { get; set; }
        public virtual DbSet<Ward_Alloc> Ward_Alloc { get; set; }
        public virtual DbSet<Ward_Master> Ward_Master { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<DrugResever> DrugResevers { get; set; }
        public virtual DbSet<SideEffect> SideEffects { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BatchStock>()
                .Property(e => e.InQty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<BatchStock>()
                .Property(e => e.OutQty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<BatchStock>()
                .Property(e => e.Price)
                .HasPrecision(18, 3);

            modelBuilder.Entity<BloodGroup>()
                .Property(e => e.BloodType)
                .IsUnicode(false);

            modelBuilder.Entity<CatDaignosi>()
                .Property(e => e.dgid)
                .IsUnicode(false);

            modelBuilder.Entity<CatDaignosi>()
                .Property(e => e.dgdetail)
                .IsUnicode(false);

            modelBuilder.Entity<CatDaignosi>()
                .HasMany(e => e.Patient_Detail)
                .WithOptional(e => e.CatDaignosi)
                .HasForeignKey(e => e.DaignosisID);

            modelBuilder.Entity<Clinic_Alloc>()
                .Property(e => e.Clinic_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Alloc>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Alloc>()
                .Property(e => e.ClinicID)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Alloc>()
                .Property(e => e.Clinic_Diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.Clinic_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.CurrentNo)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.MaxPatients)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.Clinic_Detail)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.SID)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.ClinicDates)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .Property(e => e.ClinicTime)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Master>()
                .HasMany(e => e.Clinic_Alloc)
                .WithOptional(e => e.Clinic_Master)
                .HasForeignKey(e => e.ClinicID);

            modelBuilder.Entity<Clinic_Master>()
                .HasMany(e => e.Staff_Master)
                .WithOptional(e => e.Clinic_Master)
                .HasForeignKey(e => e.LOCID);

            modelBuilder.Entity<Clinic_Schedule>()
                .Property(e => e.title)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Schedule>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Type>()
                .Property(e => e.ClinicDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Clinic_Type>()
                .HasMany(e => e.Clinic_Master)
                .WithRequired(e => e.Clinic_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clinic_Type>()
                .HasMany(e => e.Staff_Master)
                .WithRequired(e => e.Clinic_Type)
                .HasForeignKey(e => e.SpecialityID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.Ps_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.Dose)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.Duration)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.PrescribeBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.GivenBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<DrugItem>()
                .Property(e => e.UOF)
                .IsUnicode(false);

            modelBuilder.Entity<DrugMethod>()
                .Property(e => e.MethodDetail)
                .IsUnicode(false);

            modelBuilder.Entity<DrugMethod>()
                .HasMany(e => e.Drug_Prescription)
                .WithOptional(e => e.DrugMethod)
                .HasForeignKey(e => e.Method);

            modelBuilder.Entity<DrugObject>()
                .Property(e => e.ObjDisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<DrugObject>()
                .Property(e => e.ObjFileName)
                .IsUnicode(false);

            modelBuilder.Entity<DrugRoute>()
                .Property(e => e.RouteDetail)
                .IsUnicode(false);

            modelBuilder.Entity<DrugRoute>()
                .HasMany(e => e.Drug_Prescription)
                .WithOptional(e => e.DrugRoute)
                .HasForeignKey(e => e.Route);

            modelBuilder.Entity<DrugStockMaster>()
                .Property(e => e.ItemIndex)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockMaster>()
                .Property(e => e.BatchID)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockMaster>()
                .Property(e => e.LOC)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockMaster>()
                .Property(e => e.DrugQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockTransection>()
                .Property(e => e.StockID)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockTransection>()
                .Property(e => e.IssuedTo)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockTransection>()
                .Property(e => e.TransectionQty)
                .HasPrecision(18, 3);

            modelBuilder.Entity<DrugStockType>()
                .Property(e => e.StockType)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Distension)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.FreeFluid)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Liver)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Livertxt)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Spleen)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Spleentxt)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.PulsatileLumps)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.OtherLumps)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.OtherLumpstxt)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.BowelSoundsAbsent)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.BowelSoundsSluggish)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.BowelSoundsNormal)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.BowelSoundsExaggerated)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineAbdominal>()
                .Property(e => e.Other)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PulseRhythmRegular)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PulseRhythmIregular)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PulseVolume)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.JVPcmH2O)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.ApexBeat)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.HeartSounds)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.Murmurs)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.Other)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.CarotidBruit)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.BrachialLeft)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.BrachialRight)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.RadialLeft)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.RadialRight)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.FemoralLeft)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.FemoralRight)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.DorsalisPedisLeft)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.DorsalisPedisRight)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PosteriorTibialLeft)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCardiovascular>()
                .Property(e => e.PosteriorTibialRight)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.ConciousnessAlert)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.ConciousnessConfused)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.ConciousnessDrowsy)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.ConciousnessUnconscious)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.SpeechNormal)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.SpeechAphasia)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.SpeechDysarthria)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.MentalStateRational)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.MentalStateOriented)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.MentalStateConfused)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.CranialNerves)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.MotorSystem)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.SensorySystem)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.Reflexes)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.GCS)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineCentralNervou>()
                .Property(e => e.NotClinicallyIndicated)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Alert)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Confused)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Drowsy)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Unconscius)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.IsPain)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.PainScore)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.PainLocation)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.BuidLean)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.BuildAverage)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.BuildObese)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Dyspnoea)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Cyanosis)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Pallor)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Icterus)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Arcus)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Xanthomata)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.ExtremitiesWarm)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.ColdandClammy)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.PedalOedema)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.Clubbing)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinRashes)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinUlcers)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinWounds)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinTattoos)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinMoles)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinNavei)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinScars)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.SkinDetails)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.CariousTeech)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.OralUlcers)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineGeneral>()
                .Property(e => e.OtherExamination)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.SpO2_FiO2)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.PFR_LMin)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.MediastinalShiftTrachea)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.MediastinalShiftApex)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.ChestMovement)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.PercussionNote)
                .IsUnicode(false);

            modelBuilder.Entity<ExamineRespiratory>()
                .Property(e => e.Auscultation)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyDetail>()
                .Property(e => e.RelativeNo)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyDetail>()
                .Property(e => e.MemberNo)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyDetail>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<HypersenseReaction>()
                .Property(e => e.RCategory)
                .IsUnicode(false);

            modelBuilder.Entity<HypersenseReaction>()
                .Property(e => e.RSubcategory)
                .IsUnicode(false);

            modelBuilder.Entity<HypersenseReaction>()
                .HasMany(e => e.Hypersensivities)
                .WithRequired(e => e.HypersenseReaction)
                .HasForeignKey(e => e.RSubID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HypersenseSeverty>()
                .Property(e => e.SevertyType)
                .IsUnicode(false);

            modelBuilder.Entity<HypersenseSeverty>()
                .HasMany(e => e.Hypersensivities)
                .WithRequired(e => e.HypersenseSeverty)
                .HasForeignKey(e => e.SeverityID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Hypersensivity>()
                .Property(e => e.HypersenseID)
                .IsUnicode(false);

            modelBuilder.Entity<Hypersensivity>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<Hypersensivity>()
                .Property(e => e.HyperTypeSubID)
                .IsUnicode(false);

            modelBuilder.Entity<Hypersensivity>()
                .Property(e => e.HypersenseDetail)
                .IsUnicode(false);

            modelBuilder.Entity<HypersensivityType>()
                .Property(e => e.HyperTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<HypersensivityType>()
                .Property(e => e.HyperType)
                .IsUnicode(false);

            modelBuilder.Entity<HypersensivityType>()
                .Property(e => e.HyperSubType)
                .IsUnicode(false);

            modelBuilder.Entity<HypersensivityType>()
                .HasMany(e => e.Hypersensivities)
                .WithOptional(e => e.HypersensivityType)
                .HasForeignKey(e => e.HyperTypeSubID);

            modelBuilder.Entity<HypMainCategory>()
                .Property(e => e.HypersenceMainCatID)
                .IsUnicode(false);

            modelBuilder.Entity<HypMainCategory>()
                .Property(e => e.HypersenceMainCategory)
                .IsUnicode(false);

            modelBuilder.Entity<HypMainCategory>()
                .HasMany(e => e.HypersensivityTypes)
                .WithOptional(e => e.HypMainCategory)
                .HasForeignKey(e => e.HyperType);

            modelBuilder.Entity<HypRMainCategory>()
                .Property(e => e.HypRMainID)
                .IsUnicode(false);

            modelBuilder.Entity<HypRMainCategory>()
                .Property(e => e.HypRMainDetail)
                .IsUnicode(false);

            modelBuilder.Entity<HypRMainCategory>()
                .HasMany(e => e.HypersenseReactions)
                .WithOptional(e => e.HypRMainCategory)
                .HasForeignKey(e => e.RCategory);

            modelBuilder.Entity<IssuedHeader>()
                .Property(e => e.IssuedNo)
                .IsUnicode(false);

            modelBuilder.Entity<IssuedHeader>()
                .Property(e => e.IssuedTo)
                .IsUnicode(false);

            modelBuilder.Entity<Job_Category>()
                .Property(e => e.Job_Category1)
                .IsUnicode(false);

            modelBuilder.Entity<Job_Category>()
                .HasMany(e => e.Staff_Master)
                .WithRequired(e => e.Job_Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lab_MainCategory>()
                .Property(e => e.CategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_MainCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.Lab_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.LabTestID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.Result)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.Issued)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_sms>()
                .Property(e => e.massegetext)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_sms>()
                .Property(e => e.phoneno)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_sms>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_sms>()
                .Property(e => e.pid)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_sms_Cat>()
                .Property(e => e.CatName)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.LabTestID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.CategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.SubCategoryID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.SubCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.ReferenceRange)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.ReferenceRangeUnit)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .HasMany(e => e.Lab_Report)
                .WithRequired(e => e.Lab_SubCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.DBName)
                .IsFixedLength();

            modelBuilder.Entity<Location>()
                .Property(e => e.SType)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.CmdOffName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.CmdOffRank)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.AADOName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.AADORank)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.CreatedUser)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Staff_Master)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MBP_Category>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<MED_DEFN>()
                .Property(e => e.MedDef)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.NavURL)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.ControllerName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.MethodName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.UserPermissions)
                .WithRequired(e => e.MenuItem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SysID)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.ObjID)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.ObjName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SubObjID)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SubObjName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SecondSubObjID)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SecondSubObjName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.ObjectFileName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.SmallIcon)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.LargeIcon)
                .IsUnicode(false);

            modelBuilder.Entity<MenuObject>()
                .Property(e => e.CreatedUser)
                .IsUnicode(false);

            modelBuilder.Entity<ModifiedUser>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<ModifiedUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<ModifiedUser>()
                .Property(e => e.DateTime)
                .IsUnicode(false);

            modelBuilder.Entity<OPD_Master>()
                .Property(e => e.OPD_ID)
                .IsUnicode(false);

            modelBuilder.Entity<OPD_Master>()
                .Property(e => e.CurrentNo)
                .IsUnicode(false);

            modelBuilder.Entity<OPD_Master>()
                .Property(e => e.MaxPatients)
                .IsUnicode(false);

            modelBuilder.Entity<OPD_Master>()
                .Property(e => e.OPD_Detail)
                .IsUnicode(false);

            modelBuilder.Entity<OPD_Master>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.SeqID)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Hypersensivities)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Patient_Detail)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Present_Complain)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.History_PresentComplain)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Other_Complain)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.History_OtherComplain)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Special_Sickness)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Hypersensivity)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Examination)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.OPD_Diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.OPDID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.DocSID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.Treatment)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .Property(e => e.DaignosisID)
                .IsUnicode(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Clinic_Alloc)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Drug_Prescription)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Lab_Report)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Sick_Category)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Vitals)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Detail>()
                .HasMany(e => e.Ward_Alloc)
                .WithRequired(e => e.Patient_Detail)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<rank>()
                .Property(e => e.RNK_NAME)
                .IsFixedLength();

            modelBuilder.Entity<rank>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.rank1)
                .HasForeignKey(e => e.RANK);

            modelBuilder.Entity<RelationshipType>()
                .Property(e => e.Relationship)
                .IsUnicode(false);

            modelBuilder.Entity<RelationshipType>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.RelationshipType1)
                .HasForeignKey(e => e.RelationshipType);

            modelBuilder.Entity<RolePermission>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<RolePermission>()
                .Property(e => e.SysID)
                .IsUnicode(false);

            modelBuilder.Entity<RolePermission>()
                .Property(e => e.ObjectCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.SchemaType)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service_Type>()
                .Property(e => e.ServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<Service_Type>()
                .HasMany(e => e.Patients)
                .WithRequired(e => e.Service_Type1)
                .HasForeignKey(e => e.Service_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service_Type>()
                .HasMany(e => e.Staff_Master)
                .WithRequired(e => e.Service_Type1)
                .HasForeignKey(e => e.Service_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.SvcID)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.FingerID)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.TANo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ActiveNo)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.F1250)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.RR_No)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Intake)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Trade_Group)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.OtherNames)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Remark)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Location)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Formation)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Current_Location)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Current_Formation)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Posted_Status)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.LivingIn_Out)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.NICNo_New)
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.Marriage_Status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<ServicePersonnelProfile>()
                .Property(e => e.NICNo)
                .IsUnicode(false);

            modelBuilder.Entity<Sex_Type>()
                .Property(e => e.SxDetail)
                .IsUnicode(false);

            modelBuilder.Entity<Sex_Type>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Sex_Type)
                .HasForeignKey(e => e.Sex);

            modelBuilder.Entity<Sick_Category>()
                .Property(e => e.CatIndex)
                .IsUnicode(false);

            modelBuilder.Entity<Sick_Category>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Sick_Category>()
                .Property(e => e.CatPeriod)
                .IsUnicode(false);

            modelBuilder.Entity<Sick_Category>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Sick_CategoryType>()
                .Property(e => e.Category_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Sick_CategoryType>()
                .HasMany(e => e.Sick_Category)
                .WithRequired(e => e.Sick_CategoryType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sick_Type>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Speciality_Type>()
                .Property(e => e.Speciality)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.SID)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Staff_Master>()
                .Property(e => e.LOCID)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.StatusDec)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Patients)
                .WithOptional(e => e.Status1)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Patients1)
                .WithOptional(e => e.Status2)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Patient_Detail)
                .WithRequired(e => e.Status1)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TranferDetail>()
                .Property(e => e.TransID)
                .IsUnicode(false);

            modelBuilder.Entity<TranferDetail>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<TranferDetail>()
                .Property(e => e.FromLoc)
                .IsUnicode(false);

            modelBuilder.Entity<TranferDetail>()
                .Property(e => e.ToLoc)
                .IsUnicode(false);

            modelBuilder.Entity<USER_TYPE>()
                .Property(e => e.User_Type_ID)
                .IsUnicode(false);

            modelBuilder.Entity<USER_TYPE>()
                .Property(e => e.User_Type1)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.SysID)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.DivisionID)
                .IsUnicode(false);

            modelBuilder.Entity<UserPermission>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.LocationID)
                .IsFixedLength();

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.DivisionID)
                .IsFixedLength();

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.SysID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole_Type>()
                .Property(e => e.UserRoleType_Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole_Type>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.UserRole_Type)
                .HasForeignKey(e => e.RoleType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.DivisionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.SysID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Pass)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Salutation)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Rank)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Trade)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Designation)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CompetentTrade)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DivisionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.NIC)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ContactNo)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Directorate)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserPermissions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasOptional(e => e.UserProfile)
                .WithRequired(e => e.User);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisualExamination>()
                .Property(e => e.VSID)
                .IsUnicode(false);

            modelBuilder.Entity<VisualExamination>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.VitalType)
                .IsUnicode(false);

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.ReadingUnit)
                .IsUnicode(false);

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Vital_Type>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Vital_Type>()
                .HasMany(e => e.Vitals)
                .WithRequired(e => e.Vital_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.VID)
                .IsUnicode(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.VitalValues)
                .IsUnicode(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Vital>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Vital>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Vital>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Vital>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Ward_Alloc>()
                .Property(e => e.Ward_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Alloc>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Alloc>()
                .Property(e => e.WardID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Alloc>()
                .Property(e => e.Ward_Diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.Ward_ID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.CurrentBedCount)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.MaxPatients)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.Bed_Count)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .Property(e => e.Ward_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Master>()
                .HasMany(e => e.Ward_Alloc)
                .WithOptional(e => e.Ward_Master)
                .HasForeignKey(e => e.WardID);

            modelBuilder.Entity<Ward_Master>()
                .HasMany(e => e.Ward_Alloc1)
                .WithOptional(e => e.Ward_Master1)
                .HasForeignKey(e => e.WardID);
        }
    }
}
