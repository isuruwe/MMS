namespace MMS
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MMSEntities : DbContext
    {
        public MMSEntities()
            : base("name=MMSDBContext")
        {
        }

        public virtual DbSet<BatchStock> BatchStocks { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<CatDaignosi> CatDaignosis { get; set; }
        public virtual DbSet<CatDiagList> CatDiagLists { get; set; }
        public virtual DbSet<CatDiagnosisPatient> CatDiagnosisPatients { get; set; }
        public virtual DbSet<CatReferal> CatReferals { get; set; }
        public virtual DbSet<claim_batch> claim_batch { get; set; }
        public virtual DbSet<claim_catagory> claim_catagory { get; set; }
        public virtual DbSet<claim_detail> claim_detail { get; set; }
        public virtual DbSet<Clinic_Alloc> Clinic_Alloc { get; set; }
        public virtual DbSet<Clinic_Master> Clinic_Master { get; set; }
        public virtual DbSet<Clinic_Schedule> Clinic_Schedule { get; set; }
        public virtual DbSet<Clinic_Type> Clinic_Type { get; set; }
        public virtual DbSet<Drug_Prescription> Drug_Prescription { get; set; }
        public virtual DbSet<Drug_Regular> Drug_Regular { get; set; }
        public virtual DbSet<DrugGroup> DrugGroups { get; set; }
        public virtual DbSet<DrugItem> DrugItems { get; set; }
        public virtual DbSet<DrugMethod> DrugMethods { get; set; }
        public virtual DbSet<DrugObject> DrugObjects { get; set; }
        public virtual DbSet<DrugOrderForm> DrugOrderForms { get; set; }
        public virtual DbSet<DrugRoute> DrugRoutes { get; set; }
        public virtual DbSet<DrugStockCheck> DrugStockChecks { get; set; }
        public virtual DbSet<DrugStockMaster> DrugStockMasters { get; set; }
        public virtual DbSet<DrugStockTransection> DrugStockTransections { get; set; }
        public virtual DbSet<DrugStockType> DrugStockTypes { get; set; }
        public virtual DbSet<DrugStore> DrugStores { get; set; }
        public virtual DbSet<EmailList> EmailLists { get; set; }
        public virtual DbSet<ExamineAbdominal> ExamineAbdominals { get; set; }
        public virtual DbSet<ExamineCardiovascular> ExamineCardiovasculars { get; set; }
        public virtual DbSet<ExamineCentralNervou> ExamineCentralNervous { get; set; }
        public virtual DbSet<ExamineGeneral> ExamineGenerals { get; set; }
        public virtual DbSet<ExamineOther> ExamineOthers { get; set; }
        public virtual DbSet<ExamineRespiratory> ExamineRespiratories { get; set; }
        public virtual DbSet<FamilyDetail> FamilyDetails { get; set; }
        public virtual DbSet<HypersenseReaction> HypersenseReactions { get; set; }
        public virtual DbSet<HypersenseSeverty> HypersenseSeverties { get; set; }
        public virtual DbSet<Hypersensivity> Hypersensivities { get; set; }
        public virtual DbSet<HypersensivityType> HypersensivityTypes { get; set; }
        public virtual DbSet<HypMainCategory> HypMainCategories { get; set; }
        public virtual DbSet<HypRMainCategory> HypRMainCategories { get; set; }
        public virtual DbSet<ImagePath> ImagePaths { get; set; }
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
        public virtual DbSet<MedicalScreen> MedicalScreens { get; set; }
        public virtual DbSet<MedKeyword> MedKeywords { get; set; }
        public virtual DbSet<medreport> medreports { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<MenuObject> MenuObjects { get; set; }
        public virtual DbSet<ModifiedUser> ModifiedUsers { get; set; }
        public virtual DbSet<OPD_Master> OPD_Master { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<PastMedHistory> PastMedHistories { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Patient_Category> Patient_Category { get; set; }
        public virtual DbSet<Patient_Detail> Patient_Detail { get; set; }
        public virtual DbSet<Patient_SubCategory> Patient_SubCategory { get; set; }
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
        public virtual DbSet<SickReport> SickReports { get; set; }
        public virtual DbSet<Sp_List> Sp_List { get; set; }
        public virtual DbSet<Speciality_Type> Speciality_Type { get; set; }
        public virtual DbSet<Staff_Master> Staff_Master { get; set; }
        public virtual DbSet<STATION> STATIONS { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SurgeeryMo> SurgeeryMoes { get; set; }
        public virtual DbSet<Surgeom> Surgeoms { get; set; }
        public virtual DbSet<SurgeryAP> SurgeryAPs { get; set; }
        public virtual DbSet<SurgeryAssist> SurgeryAssists { get; set; }
        public virtual DbSet<SurgeryClosure> SurgeryClosures { get; set; }
        public virtual DbSet<SurgeryFrequency> SurgeryFrequencies { get; set; }
        public virtual DbSet<SurgeryMaster> SurgeryMasters { get; set; }
        public virtual DbSet<SurgeryNProcedure> SurgeryNProcedures { get; set; }
        public virtual DbSet<SurgeryNutrition> SurgeryNutritions { get; set; }
        public virtual DbSet<SurgeryPDuration> SurgeryPDurations { get; set; }
        public virtual DbSet<SurgeryPom> SurgeryPoms { get; set; }
        public virtual DbSet<SurgeryPomDetail> SurgeryPomDetails { get; set; }
        public virtual DbSet<SurgerySutureM> SurgerySutureMs { get; set; }
        public virtual DbSet<SurgeryTechniq> SurgeryTechniqs { get; set; }
        public virtual DbSet<SurgeryType> SurgeryTypes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TranferDetail> TranferDetails { get; set; }
        public virtual DbSet<Usage> Usages { get; set; }
        public virtual DbSet<USER_TYPE> USER_TYPE { get; set; }
        public virtual DbSet<userfeedback> userfeedbacks { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole_Type> UserRole_Type { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VisualExamination> VisualExaminations { get; set; }
        public virtual DbSet<Vital_Type> Vital_Type { get; set; }
        public virtual DbSet<Vital> Vitals { get; set; }
        public virtual DbSet<Ward_Alloc> Ward_Alloc { get; set; }
        public virtual DbSet<Ward_Details> Ward_Details { get; set; }
        public virtual DbSet<ward_discharge> ward_discharge { get; set; }
        public virtual DbSet<Ward_display> Ward_display { get; set; }
        public virtual DbSet<Ward_Drug_Prescription> Ward_Drug_Prescription { get; set; }
        public virtual DbSet<Ward_Lab_Report> Ward_Lab_Report { get; set; }
        public virtual DbSet<Ward_Master> Ward_Master { get; set; }
        public virtual DbSet<Ward_Mgt_Plan> Ward_Mgt_Plan { get; set; }
        public virtual DbSet<Ward_Patient_Complain> Ward_Patient_Complain { get; set; }
        public virtual DbSet<Ward_Sick_Category> Ward_Sick_Category { get; set; }
        public virtual DbSet<Ward_Types> Ward_Types { get; set; }
        public virtual DbSet<Ward_Vitals> Ward_Vitals { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Drug_Prescription_test> Drug_Prescription_test { get; set; }
        public virtual DbSet<DrugResever> DrugResevers { get; set; }
        public virtual DbSet<SideEffect> SideEffects { get; set; }
        public virtual DbSet<Ward_Daignosis> Ward_Daignosis { get; set; }
        public virtual DbSet<BoardRequest> BoardRequests { get; set; }
        public virtual DbSet<Child> Children { get; set; }
        public virtual DbSet<EPASPharmacyItem> EPASPharmacyItems { get; set; }
        public virtual DbSet<EpasUser> EpasUsers { get; set; }
        public virtual DbSet<MBPS_View> MBPS_View { get; set; }
        public virtual DbSet<MedicalCategory> MedicalCategories { get; set; }
        public virtual DbSet<MedicalItemDetailSnMD> MedicalItemDetailSnMDs { get; set; }
        public virtual DbSet<MedicalStatu> MedicalStatus { get; set; }
        public virtual DbSet<parent> parents { get; set; }
        public virtual DbSet<PersonalDetail> PersonalDetails { get; set; }
        public virtual DbSet<SpouseDetail> SpouseDetails { get; set; }
        public virtual DbSet<Vw_Establishment> Vw_Establishment { get; set; }
        public virtual DbSet<Vw_Formation> Vw_Formation { get; set; }
        public virtual DbSet<Vw_MedicalBoard> Vw_MedicalBoard { get; set; }
        public virtual DbSet<Vw_MedicalBoardAuthority> Vw_MedicalBoardAuthority { get; set; }
        public virtual DbSet<Vw_PsnlImageP2> Vw_PsnlImageP2 { get; set; }
        public virtual DbSet<Vw_PsnlImageP3> Vw_PsnlImageP3 { get; set; }
        public virtual DbSet<VwAirCrew> VwAirCrews { get; set; }
        public virtual DbSet<VwAllPaidClaimGV> VwAllPaidClaimGVS { get; set; }
        public virtual DbSet<VwCWFMC> VwCWFMCS { get; set; }

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

            modelBuilder.Entity<CatDiagList>()
                .Property(e => e.dgid)
                .IsUnicode(false);

            modelBuilder.Entity<CatDiagList>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<CatDiagnosisPatient>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<CatDiagnosisPatient>()
                .Property(e => e.CDID)
                .IsUnicode(false);

            modelBuilder.Entity<CatReferal>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<CatReferal>()
                .Property(e => e.ReffNote)
                .IsUnicode(false);

            modelBuilder.Entity<CatReferal>()
                .Property(e => e.PlanofMgt)
                .IsUnicode(false);

            modelBuilder.Entity<CatReferal>()
                .Property(e => e.DrugHistory)
                .IsUnicode(false);

            modelBuilder.Entity<claim_batch>()
                .Property(e => e.batchid)
                .IsUnicode(false);

            modelBuilder.Entity<claim_batch>()
                .Property(e => e.claim_id)
                .IsUnicode(false);

            modelBuilder.Entity<claim_catagory>()
                .Property(e => e.mc_catid)
                .IsUnicode(false);

            modelBuilder.Entity<claim_catagory>()
                .Property(e => e.mc_catdetail)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.claim_id)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.pid)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.mcat_id)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.dgid)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.loc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.ClaimName)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.RegisterNo)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.ClaimReturn)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.Authority)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.Nurse)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.DHS)
                .IsUnicode(false);

            modelBuilder.Entity<claim_detail>()
                .Property(e => e.TSI)
                .IsUnicode(false);

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

            modelBuilder.Entity<Drug_Prescription>()
                .Property(e => e.issuedQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.Ps_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.Dose)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.Duration)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.PrescribeBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.GivenBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Regular>()
                .Property(e => e.issuedQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<DrugGroup>()
                .Property(e => e.DrugGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<DrugItem>()
                .Property(e => e.UOF)
                .IsUnicode(false);

            modelBuilder.Entity<DrugItem>()
                .Property(e => e.StockQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<DrugItem>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<DrugMethod>()
                .Property(e => e.MethodDetail)
                .IsUnicode(false);

            modelBuilder.Entity<DrugMethod>()
                .HasMany(e => e.Drug_Prescription)
                .WithOptional(e => e.DrugMethod)
                .HasForeignKey(e => e.Method);

            modelBuilder.Entity<DrugMethod>()
                .HasMany(e => e.SurgeryAPs)
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

            modelBuilder.Entity<DrugRoute>()
                .HasMany(e => e.SurgeryAPs)
                .WithOptional(e => e.DrugRoute)
                .HasForeignKey(e => e.Route);

            modelBuilder.Entity<DrugStockCheck>()
                .Property(e => e.grugindex)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockCheck>()
                .Property(e => e.drugqnty)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockCheck>()
                .Property(e => e.batchid)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockCheck>()
                .Property(e => e.SysQty)
                .IsUnicode(false);

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

            modelBuilder.Entity<DrugStockTransection>()
                .Property(e => e.InLoc)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockTransection>()
                .Property(e => e.OutLoc)
                .IsUnicode(false);

            modelBuilder.Entity<DrugStockType>()
                .Property(e => e.StockType)
                .IsUnicode(false);

            modelBuilder.Entity<EmailList>()
                .Property(e => e.SvcID)
                .IsUnicode(false);

            modelBuilder.Entity<EmailList>()
                .Property(e => e.Email)
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

            modelBuilder.Entity<HypersenseSeverty>()
                .Property(e => e.SevertyType)
                .IsUnicode(false);

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

            modelBuilder.Entity<HypMainCategory>()
                .Property(e => e.HypersenceMainCatID)
                .IsUnicode(false);

            modelBuilder.Entity<HypMainCategory>()
                .Property(e => e.HypersenceMainCategory)
                .IsUnicode(false);

            modelBuilder.Entity<HypMainCategory>()
                .HasMany(e => e.Hypersensivities)
                .WithOptional(e => e.HypMainCategory)
                .HasForeignKey(e => e.HyperTypeSubID);

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

            modelBuilder.Entity<Lab_MainCategory>()
                .Property(e => e.CategoryDes)
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
                .Property(e => e.teststatus)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.testResult)
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

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.TestSID)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_Report>()
                .Property(e => e.IsPrint)
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
                .Property(e => e.SubCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.ReferenceRange)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .Property(e => e.ReferenceRangeUnit)
                .IsUnicode(false);

            modelBuilder.Entity<Lab_SubCategory>()
                .HasMany(e => e.Ward_Lab_Report)
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
                .HasMany(e => e.Staff_Master)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MBP_Category>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<MED_DEFN>()
                .Property(e => e.MedDef)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.pdid)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msstation)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msage)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msheight)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msweight)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msbmi)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msbp)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msvision)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.mshbac)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msusugar)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msfbs)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.mstotalc)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msexecg)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msmes)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.createduser)
                .IsFixedLength();

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.modifieduser)
                .IsFixedLength();

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msldl)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.mstrigliceried)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msreason)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalScreen>()
                .Property(e => e.msspecs)
                .IsUnicode(false);

            modelBuilder.Entity<MedKeyword>()
                .Property(e => e.MKDetail)
                .IsUnicode(false);

            modelBuilder.Entity<medreport>()
                .Property(e => e.dname)
                .IsUnicode(false);

            modelBuilder.Entity<medreport>()
                .Property(e => e.pid)
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

            modelBuilder.Entity<PastMedHistory>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<PastMedHistory>()
                .Property(e => e.PMHDetail)
                .IsUnicode(false);

            modelBuilder.Entity<PastMedHistory>()
                .Property(e => e.Drughst)
                .IsUnicode(false);

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
                .HasMany(e => e.Patient_Detail)
                .WithRequired(e => e.Patient)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Patient_Category>()
                .Property(e => e.CategoryDescription)
                .IsUnicode(false);

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
                .Property(e => e.GeneralEntries)
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

            modelBuilder.Entity<Patient_SubCategory>()
                .Property(e => e.SubCategoryDescription)
                .IsUnicode(false);

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

            modelBuilder.Entity<Sick_Type>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.svcid)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.ismarried)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.isliveout)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.isduty)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.islow)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.OPDID)
                .IsUnicode(false);

            modelBuilder.Entity<SickReport>()
                .Property(e => e.ISDental)
                .IsUnicode(false);

            modelBuilder.Entity<Sp_List>()
                .Property(e => e.svcid)
                .IsUnicode(false);

            modelBuilder.Entity<Speciality_Type>()
                .Property(e => e.Speciality)
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
                .HasMany(e => e.Patient_Detail)
                .WithRequired(e => e.Status1)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SurgeeryMo>()
                .Property(e => e.modesc)
                .IsUnicode(false);

            modelBuilder.Entity<Surgeom>()
                .Property(e => e.modesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.Ps_Index)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.Dose)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.Duration)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.PrescribeBy)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.GivenBy)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAP>()
                .Property(e => e.issuedQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryAssist>()
                .Property(e => e.asidesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryClosure>()
                .Property(e => e.Technique)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryClosure>()
                .Property(e => e.Noofpkts)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryClosure>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryFrequency>()
                .Property(e => e.pomfdesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Indication)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.AntibioticP)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Surgeon)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.AssistedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Anesthetist)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.SurgeryStart)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.SurgeryEnd)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Findings)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.PrcedureDetail)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.DrainsInserted)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Specimens)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.SpecimensHistology)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.SpecimensMicrobiology)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.MonitoringInstruct)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Nutrition)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.SpecialIntruct)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryMaster>()
                .Property(e => e.Nurse)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryNProcedure>()
                .Property(e => e.nprDescription)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryNProcedure>()
                .Property(e => e.nprlDescription)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryNutrition>()
                .Property(e => e.NutDesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryPDuration>()
                .Property(e => e.pomduration)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryPom>()
                .Property(e => e.pomdesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryPomDetail>()
                .Property(e => e.pdid)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryPomDetail>()
                .Property(e => e.pcatid)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryPomDetail>()
                .Property(e => e.pdetail)
                .IsUnicode(false);

            modelBuilder.Entity<SurgerySutureM>()
                .Property(e => e.Suturematerials)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryTechniq>()
                .Property(e => e.tdesc)
                .IsUnicode(false);

            modelBuilder.Entity<SurgeryType>()
                .Property(e => e.TADescription)
                .IsUnicode(false);

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

            modelBuilder.Entity<userfeedback>()
                .Property(e => e.comment)
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

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Bed_No)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Ward_No)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Created_By)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Modify_By)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.BHTNo)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.IsLiveOut)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.Diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Details>()
                .Property(e => e.OPDID)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.presentingcomp)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.hispcomp)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.pastmedhis)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.pastsurghis)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.alergies)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.manageinhosp)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.dischargeins)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.followupins)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.BhtNo)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.ConsultantName)
                .IsUnicode(false);

            modelBuilder.Entity<ward_discharge>()
                .Property(e => e.SickCategory)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Pname)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Prank)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Pwardno)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Pbedno)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.PwardD)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Pdiagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.Padmitdate)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.PserviceNo)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.PdischargDate)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.PID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_display>()
                .Property(e => e.PStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.Ps_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.Dose)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.Duration)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.PrescribeBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.GivenBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Drug_Prescription>()
                .Property(e => e.issuedQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.Lab_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.LabTestID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.teststatus)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.testResult)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.Issued)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.TestSID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Lab_Report>()
                .Property(e => e.IsPrint)
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

            modelBuilder.Entity<Ward_Mgt_Plan>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Mgt_Plan>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Mgt_Plan>()
                .Property(e => e.InvestigateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Mgt_Plan>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Mgt_Plan>()
                .Property(e => e.RemarksBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Patient_Complain>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Patient_Complain>()
                .Property(e => e.InvestigateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Patient_Complain>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Patient_Complain>()
                .Property(e => e.RemarksBy)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Sick_Category>()
                .Property(e => e.CatIndex)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Sick_Category>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Sick_Category>()
                .Property(e => e.CatPeriod)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Sick_Category>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Types>()
                .Property(e => e.Ward_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.VID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.VitalValues)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.CreatedBy)
                .IsFixedLength();

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.CreatedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.ModifiedBy)
                .IsFixedLength();

            modelBuilder.Entity<Ward_Vitals>()
                .Property(e => e.ModifiedMachine)
                .IsFixedLength();

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.Ps_Index)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.Dose)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.Duration)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.LocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.PrescribeBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.GivenBy)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.IssuedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.RequestedLocID)
                .IsUnicode(false);

            modelBuilder.Entity<Drug_Prescription_test>()
                .Property(e => e.issuedQuantity)
                .IsUnicode(false);

            modelBuilder.Entity<Ward_Daignosis>()
                .Property(e => e.PDID)
                .IsUnicode(false);

            modelBuilder.Entity<Child>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<Child>()
                .Property(e => e.ChildName)
                .IsUnicode(false);

            modelBuilder.Entity<Child>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<EPASPharmacyItem>()
                .Property(e => e.itemclass)
                .IsFixedLength();

            modelBuilder.Entity<EpasUser>()
                .Property(e => e.userid)
                .IsUnicode(false);

            modelBuilder.Entity<EpasUser>()
                .Property(e => e.rankid)
                .IsUnicode(false);

            modelBuilder.Entity<EpasUser>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<EpasUser>()
                .Property(e => e.KitIssParentUnit)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.MED16_ID)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Ser_No)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Rank)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.OtherNames)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Loacation)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Branch_Trade)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.AuthOfBoard)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PlaceOfOrigin)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PrincipleDisability)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.OtherDisability)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PlaceOfBoard)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PresentCondition)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PES)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.UOM_Hight)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.CP1)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.UOM_Wight)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Hight)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Wight)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.BoardPES)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PESNEW)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Restrict_n_Emp)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.MedCat_13b)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.OtherDesc)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Instru_Individual)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.President)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member1)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member2)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member3)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.ConfirmingOfficer)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.ConRank)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.ConApp)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.CP2)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member1Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member2Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Member3Remark)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.PresidentRemark)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Is_Mem1_Auth)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Is_Mem2_Auth)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Is_Mem3_Auth)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Is_Presi_Auth)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.EnterDate)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.F551)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<MBPS_View>()
                .Property(e => e.Branch_Trade1)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalCategory>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalCategory>()
                .Property(e => e.MedicalCategory1)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.RequisitionAllocationNo)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.LocationID)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.DOQ)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.ItemClass)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.IssueDate)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalItemDetailSnMD>()
                .Property(e => e.Stocksonordernum)
                .IsUnicode(false);

            modelBuilder.Entity<MedicalStatu>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<parent>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<parent>()
                .Property(e => e.Relationship)
                .IsUnicode(false);

            modelBuilder.Entity<parent>()
                .Property(e => e.ParentName)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.Rank)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.OtherNames)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.Branch)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.BloodGroup)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.F1250No)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<PersonalDetail>()
                .Property(e => e.ServiceStatus)
                .IsUnicode(false);

            modelBuilder.Entity<SpouseDetail>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<SpouseDetail>()
                .Property(e => e.SpouseName)
                .IsUnicode(false);

            modelBuilder.Entity<SpouseDetail>()
                .Property(e => e.NICNo)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.DBName)
                .IsFixedLength();

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.SType)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.CmdOffName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.CmdOffRank)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.CmdControl)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.CommandBy)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.AADOName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.AADORank)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.CreatedUser)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.LocShortName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.HelpDeskStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Establishment>()
                .Property(e => e.PrintName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.LocationID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.DivisionID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.DivisionType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.DivisionName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.OfficeCommand)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.RptDivisionName)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_Formation>()
                .Property(e => e.Commanding)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_MedicalBoard>()
                .Property(e => e.findingRemarks)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_PsnlImageP2>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_PsnlImageP2>()
                .Property(e => e.SerNo)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_PsnlImageP3>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<Vw_PsnlImageP3>()
                .Property(e => e.SerNo)
                .IsUnicode(false);

            modelBuilder.Entity<VwAirCrew>()
                .Property(e => e.MES)
                .IsUnicode(false);

            modelBuilder.Entity<VwAirCrew>()
                .Property(e => e.SvcNo)
                .IsUnicode(false);

            modelBuilder.Entity<VwAirCrew>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.ServiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.Rank)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.Date)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.ChequeNumber)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.BeneficiaryName)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.SNo)
                .IsUnicode(false);

            modelBuilder.Entity<VwAllPaidClaimGV>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.claRegNo)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.claSno)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.claCatCode)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.claAuthorityForCDD)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.claAuthorityForOverBillAmount)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.userName)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.RegistrationId)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.Diagnosis)
                .IsUnicode(false);

            modelBuilder.Entity<VwCWFMC>()
                .Property(e => e.Beneficiary)
                .IsUnicode(false);
        }
    }
}
