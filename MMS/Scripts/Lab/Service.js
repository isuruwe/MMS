app.service("LabService", function ($http) {

    //get All Details
    this.getPors = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetAllPOR",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
     this.GetDrugtime = function () {
        return $http.get("/Patient_Detail/GetDrugtime");
    };
 
     this.Getspins = function (id) {

         var response = $http({
             method: "post",
             url: "/Drug_Prescription/Getspins",
             params: {
                 id: JSON.stringify(id)

             }
         });
         return response;


     };

     this.acctest = function (id, catid) {

         var response = $http({
             method: "post",
             url: "/Lab_Report/acctest",
             params: {
                 id: JSON.stringify(id),
                 catid: JSON.stringify(catid)
             }
         });
         return response;


     };
     this.Getperdose = function () {
         return $http.get("/Patient_Detail/Getperdose");
     };
    //Get POR
    this.getPOR = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPOR",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };
    //get POR details
    this.getPorDtls = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPorDtls",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    // get Employee By Id
    this.getrole = function (roleID) {
        var response = $http({
            method: "post",
            url: "/api/GetRole",
            params: {
                id: JSON.stringify(roleID)
            }
        });
        return response;
    }

    // Update Employee
    this.updateRole = function (role) {
        var response = $http({
            method: "post",
            url: "/api/PostRole",
            data: JSON.stringify(role),
            dataType: "json"
        });
        return response;
    }
   

    this.Getepasdrug = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/Getepasdrug",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;


    };
    this.Getepasrpcdrug = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/Getepasrpcdrug",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;


    };
  
    this.savepasdrug = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/savepasdrug",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;


    };
   
    this.savepasrpcdrug = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/savepasrpcdrug",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;


    };
    // Add Employee
    this.AddEpisode = function (por) {
        debugger;

        var response = $http({
            method: "post",
            url: "EpisodCreate",
            data: JSON.stringify(por),
            dataType: "json"
        });
        return response;
    }

    //Add POR Details
    this.AddDetails = function (porDetails) {
        debugger;

        var response = $http({
            method: "post",
            url: "PORDtlsCreate",
            data: JSON.stringify(porDetails),
            dataType: "json"
        });
        return response;
    }

    //Delete Employee
    this.DeletePorDtls = function (roleID) {
        debugger;
        var response = $http({
            method: "post",
            url: "DeletePorDtls",
            params: {
                id: JSON.stringify(roleID),
            }

        });
        return response;
    }
    this.GetHyperSubType1 = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetHyperSubType",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        
    };
    this.Searchlabsno = function (page, id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Lab_Report/Searchlabsno",
            params: {
                id: JSON.stringify(id),
                page: JSON.stringify(page)
            }
        });
        return response;


    };
    this.GetReactSubType = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetReactSubType",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;


    };
    this.loadlabtest = function (id,catid) {

        var response = $http({
            method: "post",
            url: "/Lab_Report/loadlabtest",
            params: {
                id: JSON.stringify(id),
                catid: JSON.stringify(catid)
            }
        });
        return response;


    };
    
    this.loaddruglist = function (id) {

        var response = $http({
            method: "post",
            url: "/Drug_Prescription/loaddruglist",
            params: {
                id: JSON.stringify(id),
               
            }
        });
        return response;


    };
    this.loadisudruglist = function (id) {

        var response = $http({
            method: "post",
            url: "/Drug_Prescription/loadisudruglist",
            params: {
                id: JSON.stringify(id),

            }
        });
        return response;


    };
    this.viewlabtest = function (id, catid) {

        var response = $http({
            method: "post",
            url: "/Lab_Report/viewlabtest",
            params: {
                id: JSON.stringify(id),
                catid: JSON.stringify(catid)
            }
        });
        return response;


    };
  
    this.Savereport = function (id) {

        var response = $http({
            method: "post",
            url: "/Lab_Report/Savereport",
            params: {
                id: JSON.stringify(id)
               
            }
        });
        return response;


    };
    this.Drugissued = function (id, psitems, ditems) {

        var response = $http({
            method: "post",
            url: "/Drug_Prescription/Drugissued",
            params: {
                id: JSON.stringify(id),
                ditems: JSON.stringify(ditems),
                psitems: JSON.stringify(psitems)
            }
        });
        return response;


    };
    this.Submitpatient = function (items, hitems, Present_Complain, PID, HDetail) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/Submitpatient",
            params: {
                items: JSON.stringify(items),
                hitems: JSON.stringify(hitems),
                Present_Complain: JSON.stringify(Present_Complain),
                PID: JSON.stringify(PID),
                HDetail: JSON.stringify(HDetail)
            }
        });
        return response;


    };
    this.Savepatient = function (sitems, ditems, Present_Complain, History_PresentComplain, Other_Complain, History_OtherComplain, OPD_Diagnosis, Alert, Confused, Drowsy, Unconscious, IsPain, PainScore, PainLoc, Lean, Average, Obese, Dyspnoea, Cyanosis, Pallor, Icterus, Arcus, Xanthomata, warm, Clammy, Oedema, Clubbing, Rashes, Ulcers, Wounds, Tattoos, Moles, Navi, Scars, SkinDetails, Carius, OralUlcers, Pulsevolume, RhythmReg, RhythmIreg, JVP, ApexBeat, HeartSounds, Murmurs, CardioOther, Carotidbruit, BrachialLeft, RadialLeft, FemoralLeft, DorsalisLeft, TibialLeft, BrachialRight, RadialRight, FemoralRight, DorsalisRight, TibialRight, SpO2, PFR, Trachea, Apex, ChestMovement, Percussion, Auscultation, OiNotIndicated, OiNotIndicatedtxt, NvAlert, NvConfused, NvDrowsy, NvUnconscious, NvRational, NvOriented, NvMentalConfused, NvNotIndicated, CranialNerves, MotorSystem, SensorySystem, NvReflexes, NvGcs, NvNormal, NvAphasia, NvDysarthria, Distension, FreeFluid, Liver, Livertxt, Spleen, Spleentxt, PulsatileLumps, OtherLumps, OtherLumpstxt, BowelAbsent, BowelSluggish, BowelNormal, BowelExaggereted, AbdOther, FBC, UFR, URINECulture, RBS, fullcheck, ESR, CReactiveProtien, TSH, T3, T4, LipidProfile, kidnyTest, bloodgroup, serumelectro, StressECG, LungFunction, BloodCulture, BloodPicture, CSF, LiverFunction, RenalProfile, glyhimo, microalb, pppg, ECG, CT, CTtxt, USScantxt, USScan, MRItxt, MRI, XRaytxt, XRay, InOthertxt, InOther, Echocardiogram, PDID) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/Savepatient",
            data:params,
            params: {
                sitems: JSON.stringify(sitems),
                ditems:JSON.stringify(ditems),
                Present_Complain:JSON.stringify(Present_Complain),
                History_PresentComplain:JSON.stringify(History_PresentComplain),
                Other_Complain: JSON.stringify(Other_Complain),
                History_OtherComplain: JSON.stringify(History_OtherComplain),
                OPD_Diagnosis: JSON.stringify(OPD_Diagnosis),
                Alert: JSON.stringify(Alert),
                Confused: JSON.stringify(Confused),
                Drowsy: JSON.stringify(Drowsy),
                Unconscious: JSON.stringify(Unconscious),
                IsPain: JSON.stringify(IsPain),
                PainScore: JSON.stringify(PainScore),
                PainLoc: JSON.stringify(PainLoc),
                Lean: JSON.stringify(Lean),
                Average: JSON.stringify(Average),
                Obese: JSON.stringify(Obese),
                Dyspnoea: JSON.stringify(Dyspnoea),
                Cyanosis: JSON.stringify(Cyanosis),
                Pallor: JSON.stringify(Pallor),
                Icterus: JSON.stringify(Icterus),
                Arcus: JSON.stringify(Arcus),
                Xanthomata: JSON.stringify(Xanthomata),
                warm: JSON.stringify(warm),
                Clammy: JSON.stringify(Clammy),
                Oedema: JSON.stringify(Oedema),
                Clubbing: JSON.stringify(Clubbing),
                Rashes: JSON.stringify(Rashes),
                Ulcers: JSON.stringify(Ulcers),
                Wounds: JSON.stringify(Wounds),
                Tattoos: JSON.stringify(Tattoos),
                Moles: JSON.stringify(Moles),
                Navi: JSON.stringify(Navi),
                Scars: JSON.stringify(Scars),
                SkinDetails: JSON.stringify(SkinDetails),
                Carius: JSON.stringify(Carius),
                OralUlcers: JSON.stringify(OralUlcers),
                Pulsevolume: JSON.stringify(Pulsevolume),
                RhythmReg: JSON.stringify(RhythmReg),
                RhythmIreg: JSON.stringify(RhythmIreg),
                JVP: JSON.stringify(JVP),
                ApexBeat: JSON.stringify(ApexBeat),
                HeartSounds: JSON.stringify(HeartSounds),
                Murmurs: JSON.stringify(Murmurs),
                CardioOther: JSON.stringify(CardioOther),
                Carotidbruit: JSON.stringify(Carotidbruit),
                BrachialLeft: JSON.stringify(BrachialLeft),
                RadialLeft: JSON.stringify(RadialLeft),
                FemoralLeft: JSON.stringify(FemoralLeft),
                DorsalisLeft: JSON.stringify(DorsalisLeft),
                TibialLeft: JSON.stringify(TibialLeft),
                BrachialRight: JSON.stringify(BrachialRight),
                RadialRight: JSON.stringify(RadialRight),
                FemoralRight: JSON.stringify(FemoralRight),
                DorsalisRight: JSON.stringify(DorsalisRight),
                TibialRight: JSON.stringify(TibialRight),
                SpO2: JSON.stringify(SpO2),
                PFR: JSON.stringify(PFR),
                Trachea: JSON.stringify(Trachea),
                Apex: JSON.stringify(Apex),
                ChestMovement: JSON.stringify(ChestMovement),
                Percussion: JSON.stringify(Percussion),
                Auscultation: JSON.stringify(Auscultation),
                OiNotIndicated: JSON.stringify(OiNotIndicated),
                OiNotIndicatedtxt: JSON.stringify(OiNotIndicatedtxt),
                NvAlert: JSON.stringify(NvAlert),
                NvConfused: JSON.stringify(NvConfused),
                NvDrowsy: JSON.stringify(NvDrowsy),
                NvUnconscious: JSON.stringify(NvUnconscious),
                NvRational: JSON.stringify(NvRational),
                NvOriented: JSON.stringify(NvOriented),
                NvMentalConfused: JSON.stringify(NvMentalConfused),
                NvNotIndicated: JSON.stringify(NvNotIndicated),
                CranialNerves: JSON.stringify(CranialNerves),
                MotorSystem: JSON.stringify(MotorSystem),
                SensorySystem: JSON.stringify(SensorySystem),
                NvReflexes: JSON.stringify(NvReflexes),
                NvGcs: JSON.stringify(NvGcs),
                NvNormal: JSON.stringify(NvNormal),
                NvAphasia: JSON.stringify(NvAphasia),
                NvDysarthria: JSON.stringify(NvDysarthria),
                Distension: JSON.stringify(Distension),
                FreeFluid: JSON.stringify(FreeFluid),
                Liver: JSON.stringify(Liver),
                Livertxt: JSON.stringify(Livertxt),
                Spleen: JSON.stringify(Spleen),
                Spleentxt: JSON.stringify(Spleentxt),
                PulsatileLumps: JSON.stringify(PulsatileLumps),
                OtherLumps: JSON.stringify(OtherLumps),
                OtherLumpstxt: JSON.stringify(OtherLumpstxt),
                BowelAbsent: JSON.stringify(BowelAbsent),
                BowelSluggish: JSON.stringify(BowelSluggish),
                BowelNormal: JSON.stringify(BowelNormal),
                BowelExaggereted: JSON.stringify(BowelExaggereted),
                AbdOther: JSON.stringify(AbdOther),
                FBC: JSON.stringify(FBC),
                UFR: JSON.stringify(UFR),
                URINECulture: JSON.stringify(URINECulture),
                RBS: JSON.stringify(RBS),
                fullcheck: JSON.stringify(fullcheck),  
                ESR: JSON.stringify(ESR),
                CReactiveProtien: JSON.stringify(CReactiveProtien),
                StressECG: JSON.stringify(StressECG),
                kidnyTest: JSON.stringify(kidnyTest),
                bloodgroup: JSON.stringify(bloodgroup),
                serumelectro: JSON.stringify(serumelectro),
                glyhimo: JSON.stringify(glyhimo),
                microalb: JSON.stringify(microalb),
                pppg: JSON.stringify(pppg),
                T3: JSON.stringify(T3),
                T4: JSON.stringify(T4),
                StressECG: JSON.stringify(StressECG),
                LungFunction: JSON.stringify(LungFunction),
                BloodCulture: JSON.stringify(BloodCulture),
                BloodPicture: JSON.stringify(BloodPicture),
                CSF: JSON.stringify(CSF),
                CT: JSON.stringify(CT),
                CTtxt: JSON.stringify(CTtxt),
                LiverFunction: JSON.stringify(LiverFunction),
                RenalProfile: JSON.stringify(RenalProfile),
                
                LipidProfile: JSON.stringify(LipidProfile),
                ECG: JSON.stringify(ECG),
                USScantxt: JSON.stringify(USScantxt),
                USScan: JSON.stringify(USScan),
                MRItxt: JSON.stringify(MRItxt),
                MRI: JSON.stringify(MRI),
                XRaytxt: JSON.stringify(XRaytxt),
                XRay: JSON.stringify(XRay),
                InOthertxt: JSON.stringify(InOthertxt),
                InOther: JSON.stringify(InOther),
                Echocardiogram: JSON.stringify(Echocardiogram),
                PDID: JSON.stringify(PDID)
            }
            
        });
        
        return response;


    };
    
    //Get Locations
    this.GetVitalTypes = function () {
        return $http.get("/Patient_Detail/GetVitalTypes");
    };
    this.loadcamp = function () {
        return $http.get("/Lab_Report/viewcamp");
    };
    this.GetSeverityTypes = function () {
        return $http.get("/Patient_Detail/GetSeverityTypes");
    };
    this.GetMethod = function () {
        return $http.get("/Patient_Detail/GetMethod");
    };
    this.GetRoute = function () {
        return $http.get("/Patient_Detail/GetRoute");
    };
    this.GetDrug = function () {
        return $http.get("/Patient_Detail/GetDrug");
    };
    this.GetSicktype = function () {
        return $http.get("/Patient_Detail/GetSicktype");
    };
    this.GetHyperReactType = function () {
        return $http.get("/Patient_Detail/GetHyperReactType");
    };
    this.GetHyperMainType = function () {
        return $http.get("/Patient_Detail/GetHyperTypes");
    };
    //Get Relationship types
    this.GetRelationships = function () {
        debugger;
        return $http.get("/Patient_Detail/GetRelationships");
    };

    //Get user location
    this.getUserLocation = function () {
        return $http.get("/POR_Header/GetUserLocation");
    };

    //Get Main Category
    this.getMainCategorys = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetMainCategory",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    //Get Sub Category
    this.getSubCategorys = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetSubCategory",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get gs division
    this.getDSDivision = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetDSDivision",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Town
    this.getTown = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetTown",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };

    //Get Police Station
    this.getPoliceStation = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPoliceStation",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };
    
     
    this.saveditem = function (itemname, qnty) {

        var response = $http({
            method: "post",
            url: "/DrugItems/saveditem",
            data: $.param({
               
                itemname: JSON.stringify(itemname),
                qnty: JSON.stringify(qnty)

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.GetPatients = function (id,relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatients",
            params: {
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)
            }
        });
        return response;

    };

    this.submitvital = function (ite) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/submitvitals",
            params: {
                id: JSON.stringify(ite)

            }
        });
        return response;

    };

    this.loadchildimg = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/loadchildimg",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;

    };

    this.getsp = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadServicePerson",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.getrnks = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadRanks",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.getrelation = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadRelationtype",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.getsex = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadSex",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    //Get Sub Category
    this.getFormation = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetFormation",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Service Personnel Details
    this.getServicePersonnel = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetServicePersonnel",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Promotion authority Details
    this.getAuthRef = function (authority, Subcat) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetAuthRef",
            params: {
                id: JSON.stringify(authority),
                Subcat: JSON.stringify(Subcat)
            }
        });
        return response;

    };

    //Get Ranks
    this.getRanks = function () {
        debugger;
        return $http.get("/POR_Header/GetRank");
    };

    //Get Trades
    this.getTrade = function () {
        debugger;
        return $http.get("/POR_Header/GetTrade");
    };

    //Get Districts
    this.getDistricts = function () {
        debugger;
        return $http.get("/POR_Header/GetDistricts");
    };

    //Get nature of occurances
    this.getNatureofOccurance = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetNatureofOccurance",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Max Occurance NO
    this.getMaxOccuranceNo = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetMaxOccuranceNo",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    // get Employee By Id
    this.getrole = function (roleID) {
        var response = $http({
            method: "post",
            url: "/api/GetRole",
            params: {
                id: JSON.stringify(roleID)
            }
        });
        return response;
    }

});